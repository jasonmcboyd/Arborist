using Arborist.Core;
using Arborist.Linq.Extensions;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class WhereBreadthFirstTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public WhereBreadthFirstTreenumerator(
      Func<ITreenumerator<TNode>> innerTreenumeratorFactory,
      Func<NodeContext<TNode>, bool> predicate,
      NodeTraversalStrategies nodeTraversalStrategy)
      : base(innerTreenumeratorFactory)
    {
      _Predicate = predicate;
      _NodeTraversalStrategy = nodeTraversalStrategy;
      _NodePositionAndVisitCounts.AddLast(new NodeTraversalStatus(InnerTreenumerator.Node, InnerTreenumerator.Position, 0, 0, -1));
    }

    private readonly Func<NodeContext<TNode>, bool> _Predicate;
    private readonly NodeTraversalStrategies _NodeTraversalStrategy;

    private readonly RefSemiDeque<NodeTraversalStatus> _NodePositionAndVisitCounts = new RefSemiDeque<NodeTraversalStatus>();
    private readonly RefSemiDeque<SkippedNodeInfo> _SkippedStack = new RefSemiDeque<SkippedNodeInfo>();

    private int _SeenRootNodesCount = 0;

    // Track the inner sibling index at each depth for the current path
    private readonly List<int> _CurrentInnerPath = new List<int>();

    // Flat shared buffers for inner path data, replacing per-node int[] allocations.
    // Queue buffer is append-only (grows monotonically over the traversal).
    // Skipped buffer is LIFO with memory reuse: on push, write at top; on pop, reset top.
    private readonly List<int> _QueuePathData = new List<int>();
    private readonly List<int> _SkippedPathData = new List<int>();
    private int _SkippedPathDataTop = 0;

    // Track the sibling index of the last consumer-skipped node removed at each depth.
    // When consecutive consumer-skipped nodes are removed at different depths (e.g. b at
    // depth 1 then d at depth 2), we need to remember both positions. A single variable
    // would be overwritten by the deeper removal, losing the shallower sibling info.
    // Value of -1 means no removed node at that depth.
    private readonly List<int> _RemovedSkippedSiblingIndices = new List<int>();

    // Track if we need to emit a parent visit before the next inner event
    private bool _PendingParentVisit = false;

    // When a _PendingParentVisit is cancelled by consumer SkipNode (because
    // previouslySeenNodeWasScheduledAndSkipped prevents emission), the inner BFT
    // MAY produce a redundant parent visit. This is only redundant if no new accepted
    // child was scheduled between the cancellation and the inner parent visit.
    // If a new child WAS scheduled (e.g. an inner sibling like c), the inner parent
    // visit is legitimate and should be output.
    private bool _ConsumeNextInnerParentVisit = false;

    // Track the depth of the last removed skipped parent, for sibling index calculation
    // When we remove a skipped parent and schedule its child, the sibling index should reset to 0
    private int? _DepthOfLastRemovedSkippedParent = null;

    // Track the last visited node's depth and visit count for sibling index calculation
    // In BFT, after visiting a node at depth D, scheduled nodes at D+1 are its children
    private int _LastVisitedNodeDepth = -1;
    private int _LastVisitedNodeVisitCount = 0;

    // When a pending parent visit is emitted, the consumer's strategy for the previously
    // scheduled node is deferred until the next MoveNext call.
    private NodeTraversalStrategies? _DeferredNodeTraversalStrategies = null;

    // When SkipSiblings is stripped from the inner strategy (to avoid damaging the inner
    // BFT's queue), the wrapper handles sibling skipping itself. It tracks:
    // - The inner depth and parent path for fast inner-sibling matching
    // - The effective depth and queue front position for catching promoted siblings
    //   from different subtrees (slow path)
    private bool _SkipRemainingSiblings = false;
    private int _SkipSiblingsInnerDepth = -1;
    private int[] _SkipSiblingsInnerParentPath = null;
    private int _SkipSiblingsEffectiveDepth = -1;
    private NodePosition _SkipSiblingsQueueFrontPosition;

    // When a consumer-SkipNode'd node is removed from the queue, its effective depth
    // is saved here. The inner BFT's SkipNode processes the node's children directly,
    // then jumps to the queue front's next child WITHOUT producing a parent visit.
    // When the next accepted node is scheduled at a depth <= this saved depth, the
    // wrapper must generate a parent visit. Set to -1 when not applicable.
    private int _ConsumerSkippedParentEffectiveDepth = -1;

    // When a parent visit is generated due to a consumer-SkipNode'd parent transition
    // (see _ConsumerSkippedParentEffectiveDepth), the scheduling of the new accepted
    // node is deferred until the next MoveNext call.
    private bool _HasDeferredSchedule = false;

    // Tracks whether the last child action in a consumer-SkipNode'd parent's subtree
    // was a consumer SkipNode. When transitioning out of the subtree, the deferred
    // schedule should only fire if the last action was NOT a consumer SkipNode.
    // In a clean BFT, when the last child is SkipNode'd, SkipNode calls TryPushNextChild
    // on the queue front which succeeds without incrementing VisitCount -- no V parent
    // is produced. When the last child is TraverseAll'd (or Where-filtered only),
    // the terminal path DOES produce V parent via VisitCount++.
    private bool _ConsumerSkippedChildAfterLastAccepted = false;


    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      // When a parent visit was emitted due to a consumer-SkipNode'd parent transition,
      // the scheduling of the accepted node was deferred. Output it now.
      if (_HasDeferredSchedule)
      {
        _HasDeferredSchedule = false;

        ref var deferredEntry = ref _NodePositionAndVisitCounts.GetLast();

        if (deferredEntry.Position.Depth == 0)
          _SeenRootNodesCount++;

        Mode = TreenumeratorMode.SchedulingNode;
        Node = deferredEntry.Node;
        Position = deferredEntry.Position;
        VisitCount = deferredEntry.VisitCount;

        return true;
      }

      if (Mode == TreenumeratorMode.VisitingNode)
      {
        if (_DeferredNodeTraversalStrategies.HasValue)
        {
          nodeTraversalStrategies = _DeferredNodeTraversalStrategies.Value;
          _DeferredNodeTraversalStrategies = null;
        }
        else
        {
          nodeTraversalStrategies = NodeTraversalStrategies.TraverseAll;
        }
      }

      var previouslySeenNodeWasScheduledAndSkipped =
        Position != new NodePosition(0, -1)
        && InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode);

      if (previouslySeenNodeWasScheduledAndSkipped)
        _NodePositionAndVisitCounts.GetLast().TraversalStrategy = nodeTraversalStrategies;

      // When a promoted node has SkipSiblings and its effective depth is 0 (root)
      // but inner depth > 0, strip SkipSiblings. The clean BFT would set
      // _RootsEnumeratorFinished (no CE disposal), but the inner BFT at depth > 0
      // would dispose the queue front's CE (damaging unrelated subtrees).
      //
      // When effective depth > 0 (even if != inner depth), DON'T strip. The inner
      // BFT's SkipSiblings disposes the queue front's CE, matching what the clean
      // BFT would do at the same depth > 0. This correctly prevents the queue
      // front's children from being scheduled.
      if (Position != new NodePosition(0, -1)
        && InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipSiblings))
      {
        var outerDepth = _NodePositionAndVisitCounts.GetLast().Position.Depth;
        var innerDepth = InnerTreenumerator.Position.Depth;
        if (outerDepth == 0 && innerDepth > 0)
        {
          nodeTraversalStrategies = nodeTraversalStrategies & ~NodeTraversalStrategies.SkipSiblings;
          _SkipRemainingSiblings = true;
          _SkipSiblingsInnerDepth = InnerTreenumerator.Position.Depth;
          _SkipSiblingsInnerParentPath = BuildInnerPath(InnerTreenumerator.Position.Depth - 1);
          _SkipSiblingsEffectiveDepth = outerDepth;
          _SkipSiblingsQueueFrontPosition = _NodePositionAndVisitCounts.GetFirst().Position;
        }
      }

      // If we have a pending parent visit, emit it now
      // But only if:
      // 1. The previously scheduled node was NOT skipped by the caller
      // 2. The parent is not the sentinel (position != (0,-1))
      if (_PendingParentVisit && !previouslySeenNodeWasScheduledAndSkipped)
      {
        ref var parentStatus = ref _NodePositionAndVisitCounts.GetFirst();

        // Only emit if parent is not the sentinel
        if (parentStatus.Position.Depth >= 0)
        {
          _PendingParentVisit = false;
          _DeferredNodeTraversalStrategies = nodeTraversalStrategies;
          parentStatus.VisitCount++;

          // Track the visited node's depth and visit count for sibling index calculation
          _LastVisitedNodeDepth = parentStatus.Position.Depth;
          _LastVisitedNodeVisitCount = parentStatus.VisitCount;

          Mode = TreenumeratorMode.VisitingNode;
          Node = parentStatus.Node;
          Position = parentStatus.Position;
          VisitCount = parentStatus.VisitCount;
          return true;
        }
      }

      // When the consumer SkipNode'd the previously scheduled child, the inner BFT
      // may produce a redundant parent visit. This happens when the wrapper has already
      // emitted parent visits (via _PendingParentVisit) that the inner BFT hasn't
      // accounted for. The actual redundancy is verified in the consumption check by
      // comparing the wrapper's VisitCount with the inner BFT's VisitCount.
      // The flag is cleared when any accepted child is scheduled (see scheduling section).
      if (previouslySeenNodeWasScheduledAndSkipped)
      {
        _ConsumeNextInnerParentVisit = true;
      }

      // When a consumer-SkipNode'd child is within the consumer-SkipNode'd parent's subtree,
      // track that the last child action was a consumer SkipNode. This determines whether
      // the deferred schedule should fire when transitioning out of the subtree.
      if (previouslySeenNodeWasScheduledAndSkipped
        && _ConsumerSkippedParentEffectiveDepth >= 0)
      {
        _ConsumerSkippedChildAfterLastAccepted = true;
      }

      // Clear pending flag if we're not emitting
      _PendingParentVisit = false;

      var previousModeWasVisitingNode = Mode == TreenumeratorMode.VisitingNode;

      while (InnerTreenumerator.MoveNext(nodeTraversalStrategies))
      {
        if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        {
          // Update the current inner path when scheduling
          var innerDepth = InnerTreenumerator.Position.Depth;
          while (_CurrentInnerPath.Count <= innerDepth)
            _CurrentInnerPath.Add(0);
          _CurrentInnerPath[innerDepth] = InnerTreenumerator.Position.SiblingIndex;

          // Only pop skipped nodes when scheduling at a shallower depth
          // In BFT, siblings at the same depth might be from different subtrees,
          // so we must not pop them (path-based counting handles correctness)
          while (_SkippedStack.Count > 0 && _SkippedStack.GetLast().Position.Depth > InnerTreenumerator.Position.Depth)
          {
            _SkippedPathDataTop = _SkippedStack.GetLast().InnerPathOffset;
            _SkippedStack.RemoveLast();
          }

          // When SkipSiblings was stripped from the inner strategy, skip remaining
          // siblings by passing SkipNodeAndDescendants to the inner BFT.
          // Two paths: (1) fast path for true inner siblings (same inner parent),
          // (2) slow path using effective depth for promoted siblings from different subtrees.
          if (_SkipRemainingSiblings)
          {
            var shouldSkip = false;

            // Fast path: same inner depth and same inner parent
            if (innerDepth == _SkipSiblingsInnerDepth
              && _SkipSiblingsInnerParentPath != null
              && _SkipSiblingsInnerParentPath.Length <= _CurrentInnerPath.Count)
            {
              shouldSkip = true;
              for (int i = 0; i < _SkipSiblingsInnerParentPath.Length; i++)
              {
                if (_CurrentInnerPath[i] != _SkipSiblingsInnerParentPath[i])
                {
                  shouldSkip = false;
                  break;
                }
              }
            }

            // Slow path: check effective depth for promoted siblings at different inner depths.
            // Also verify the queue front hasn't changed, which ensures we're still scheduling
            // children of the same effective parent. If the queue front has changed (e.g., due
            // to visiting events moving to a different parent), nodes at the same effective depth
            // are under a different parent and should not be skipped.
            if (!shouldSkip)
            {
              var skippedAncestorCount = CountSkippedAncestors();
              var effectiveDepth = innerDepth - skippedAncestorCount;

              if (effectiveDepth == _SkipSiblingsEffectiveDepth
                && _NodePositionAndVisitCounts.GetFirst().Position == _SkipSiblingsQueueFrontPosition)
                shouldSkip = true;
              else
                _SkipRemainingSiblings = false;
            }

            if (shouldSkip)
            {
              nodeTraversalStrategies = NodeTraversalStrategies.SkipNodeAndDescendants;
              continue;
            }
          }

          var skipped = _Predicate(InnerTreenumerator.ToNodeContext());

          if (skipped)
          {
            var skippedPathOffset = AppendSkippedPath(innerDepth);
            _SkippedStack.AddLast(new SkippedNodeInfo(InnerTreenumerator.Position, skippedPathOffset));

            nodeTraversalStrategies = _NodeTraversalStrategy;

            continue;
          }
          else
          {
            // Remove skipped nodes from the queue BEFORE calculating the effective position
            // This ensures we don't use positions from a different parent's skipped children
            var lastScheduleNodeVisitWasSkipped = _NodePositionAndVisitCounts.GetLast().Skipped;

            // Save the depth before removal for post-effectivePosition checks
            var removedSkippedParentDepth = lastScheduleNodeVisitWasSkipped
              ? _NodePositionAndVisitCounts.GetLast().Position.Depth
              : -1;

            if (lastScheduleNodeVisitWasSkipped)
            {
              // Track the depth of the skipped parent for sibling index calculation
              _DepthOfLastRemovedSkippedParent = _NodePositionAndVisitCounts.GetLast().Position.Depth;

              // Save the position of the skipped node for sibling index calculation
              // Save if:
              // 1. Previous output was not a visit (consecutive schedules), OR
              // 2. Parent's VC > 1 (we've scheduled siblings before, so this is a sibling being removed)
              // Don't save if we just visited a NEW parent (VC == 1) - those are from different parent
              if (!previousModeWasVisitingNode || _LastVisitedNodeVisitCount > 1)
              {
                var removedPos = _NodePositionAndVisitCounts.GetLast().Position;
                SetRemovedSkippedSiblingIndex(removedPos.Depth, removedPos.SiblingIndex);
              }

              _NodePositionAndVisitCounts.RemoveLast();
            }
            else
            {
              _DepthOfLastRemovedSkippedParent = null;
            }

            var effectivePosition = GetEffectivePosition();

            // Check if this is a promoted child (child of a filtered node)
            // If so, set pending parent visit flag - but only if the promoted child
            // is NOT becoming a root (effectiveDepth > 0)
            // Use path-based matching to find actual filtered parent
            if (_SkippedStack.Count > 0
              && InnerTreenumerator.Position.Depth > 0
              && effectivePosition.Depth > 0)
            {
              var parentDepth = InnerTreenumerator.Position.Depth - 1;
              bool parentIsSkipped = false;
              for (int i = 0; i < _SkippedStack.Count; i++)
              {
                var skippedInfo = _SkippedStack.GetFromBack(i);
                if (skippedInfo.Position.Depth == parentDepth
                  && IsSkippedPathPrefix(skippedInfo.InnerPathOffset, skippedInfo.Position.Depth + 1, parentDepth + 1))
                {
                  parentIsSkipped = true;
                  break;
                }
              }

              if (parentIsSkipped)
              {
                _PendingParentVisit = true;
              }
            }

            // An accepted child was scheduled. If we were waiting to consume a
            // redundant inner parent visit (from a cancelled _PendingParentVisit),
            // the scheduling of this child means the next inner parent visit is
            // legitimate, not redundant. Clear the consumption flag.
            _ConsumeNextInnerParentVisit = false;

            // An accepted child is being scheduled. If it's WITHIN the consumer-
            // SkipNode'd parent's subtree (deeper than the saved depth), this means
            // the last action in the subtree is NOT a consumer SkipNode. Clear the
            // flag so the deferred schedule can fire when transitioning out.
            // Don't clear for nodes AT or ABOVE the saved depth - those are siblings,
            // not children of the consumer-SkipNode'd node.
            if (_ConsumerSkippedParentEffectiveDepth >= 0
              && effectivePosition.Depth > _ConsumerSkippedParentEffectiveDepth)
              _ConsumerSkippedChildAfterLastAccepted = false;

            // Track transitions from consumer-SkipNode'd node subtrees.
            // When a consumer-SkipNode'd node is removed and the new accepted node is
            // at a DEEPER effective depth, it's a child of the SkipNode'd node. Save the
            // depth so we can detect when we leave the subtree later.
            // When a previously-tracked consumer-SkipNode'd subtree is exited (new node
            // at depth <= saved depth), emit a parent visit for the queue front.
            if (lastScheduleNodeVisitWasSkipped && removedSkippedParentDepth >= 0
              && effectivePosition.Depth > removedSkippedParentDepth)
            {
              // New node is a child of the consumer-SkipNode'd parent.
              // Track the depth for transition detection.
              _ConsumerSkippedParentEffectiveDepth = removedSkippedParentDepth;
            }

            if (_ConsumerSkippedParentEffectiveDepth >= 0
              && effectivePosition.Depth <= _ConsumerSkippedParentEffectiveDepth
              && effectivePosition.Depth > 0
              && !previousModeWasVisitingNode
              && _NodePositionAndVisitCounts.GetFirst().Position.Depth >= 0)
            {
              if (!_ConsumerSkippedChildAfterLastAccepted)
              {
                _ConsumerSkippedParentEffectiveDepth = -1;
                _ConsumerSkippedChildAfterLastAccepted = false;

                // Add the node to queue (its output will be deferred)
                var deferredInnerPathOffset = AppendQueuePath(innerDepth);
                _NodePositionAndVisitCounts.AddLast(new NodeTraversalStatus(InnerTreenumerator.Node, effectivePosition, 0, deferredInnerPathOffset, innerDepth));
                _HasDeferredSchedule = true;

                // Emit parent visit for the queue front
                ref var parentStatus = ref _NodePositionAndVisitCounts.GetFirst();
                parentStatus.VisitCount++;

                _LastVisitedNodeDepth = parentStatus.Position.Depth;
                _LastVisitedNodeVisitCount = parentStatus.VisitCount;

                Mode = TreenumeratorMode.VisitingNode;
                Node = parentStatus.Node;
                Position = parentStatus.Position;
                VisitCount = parentStatus.VisitCount;

                return true;
              }
              else
              {
                // The last child was consumer-SkipNode'd, so no deferred V parent needed.
                // Clear tracking and fall through to normal scheduling.
                _ConsumerSkippedParentEffectiveDepth = -1;
                _ConsumerSkippedChildAfterLastAccepted = false;
              }
            }

            var innerPathOffset = AppendQueuePath(innerDepth);
            _NodePositionAndVisitCounts.AddLast(new NodeTraversalStatus(InnerTreenumerator.Node, effectivePosition, 0, innerPathOffset, innerDepth));
          }
        }
        else // VisitingNode
        {
          // When visiting a node, clear saved positions - we're entering a new subtree
          ClearRemovedSkippedSiblingIndices();

          var innerDepth = InnerTreenumerator.Position.Depth;
          var innerSiblingIndex = InnerTreenumerator.Position.SiblingIndex;

          // In BFT, do NOT pop skipped nodes during visiting. Unlike DFT where
          // visiting means the subtree is complete, in BFT visiting happens BEFORE
          // children at deeper levels are scheduled. Popping here would lose skipped
          // ancestor information needed for later depth calculations.
          //
          // The scheduling section handles cleanup: it pops strictly-deeper nodes
          // when scheduling at a shallower depth. Path-based CountSkippedAncestors
          // safely ignores stale entries from unrelated subtrees.

          // Get the visiting node's path info from our queue for full path comparison.
          // For VC==1: the visiting node is at queue index 1 (index 0 is the previous front to be removed)
          // For VC>1: the visiting node is at queue index 0 (the front)
          int visitingNodePathOffset = 0;
          int visitingNodePathLength = 0;
          if (InnerTreenumerator.VisitCount == 1 && _NodePositionAndVisitCounts.Count >= 2)
          {
            var indexFromBack = _NodePositionAndVisitCounts.Count - 2;
            var entry = _NodePositionAndVisitCounts.GetFromBack(indexFromBack);
            visitingNodePathOffset = entry.InnerPathOffset;
            visitingNodePathLength = entry.InnerDepth + 1;
          }
          else if (_NodePositionAndVisitCounts.Count >= 1)
          {
            var entry = _NodePositionAndVisitCounts.GetFirst();
            visitingNodePathOffset = entry.InnerPathOffset;
            visitingNodePathLength = entry.InnerDepth + 1;
          }

          // Check if we're visiting a filtered node using full path comparison.
          // Nodes from different subtrees can share the same inner position (e.g. d at (0,1)
          // under a and e at (0,1) under c). Full path comparison distinguishes them.
          var isVisitingFilteredNode = false;
          for (int i = 0; i < _SkippedStack.Count; i++)
          {
            var skippedInfo = _SkippedStack.GetFromBack(i);
            var skippedPathLength = skippedInfo.Position.Depth + 1;
            if (skippedInfo.Position == InnerTreenumerator.Position
              && visitingNodePathLength > 0
              && skippedPathLength == visitingNodePathLength)
            {
              var pathsMatch = true;
              for (int j = 0; j < skippedPathLength; j++)
              {
                if (_SkippedPathData[skippedInfo.InnerPathOffset + j] != _QueuePathData[visitingNodePathOffset + j])
                {
                  pathsMatch = false;
                  break;
                }
              }
              if (pathsMatch)
              {
                isVisitingFilteredNode = true;
                break;
              }
            }
          }

          if (isVisitingFilteredNode)
          {
            // Skip all visits to filtered nodes - the parent visits are handled via _PendingParentVisit
            continue;
          }

          // Check if this inner parent visit should be consumed.
          // When the consumer SkipNode'd a previously scheduled node, the wrapper may have
          // already emitted the corresponding parent visit (via deferred schedule or
          // _PendingParentVisit). If the wrapper's queue front VisitCount is already >= the
          // inner's VisitCount, the visit is redundant and should be skipped.
          // If the wrapper is behind, check the slow path (parent-of-filtered relationship).
          if (_ConsumeNextInnerParentVisit
            && InnerTreenumerator.VisitCount > 1)
          {
            // Fast path: if the wrapper's queue front VisitCount is already >= the inner's
            // VisitCount, the wrapper has already emitted this visit (via deferred schedule
            // or _PendingParentVisit). The inner V parent is definitely redundant.
            if (_NodePositionAndVisitCounts.GetFirst().VisitCount >= InnerTreenumerator.VisitCount)
            {
              _ConsumeNextInnerParentVisit = false;
              continue;
            }

            // Neither path consumed -- clear the flag.
            _ConsumeNextInnerParentVisit = false;
          }

          // Normal visiting logic
          if (InnerTreenumerator.VisitCount == 1)
          {
            _NodePositionAndVisitCounts.RemoveFirst();

            // Restore inner path from the visited node's stored path
            ref var visitedEntry = ref _NodePositionAndVisitCounts.GetFirst();
            if (visitedEntry.InnerDepth >= 0)
              RestoreInnerPathFromQueue(visitedEntry.InnerPathOffset, visitedEntry.InnerDepth + 1);
          }
          else if (previousModeWasVisitingNode)
            continue;

          _NodePositionAndVisitCounts.GetFirst().VisitCount++;

          // Track the visited node's effective depth and visit count for sibling index calculation
          _LastVisitedNodeDepth = _NodePositionAndVisitCounts.GetFirst().Position.Depth;
          _LastVisitedNodeVisitCount = _NodePositionAndVisitCounts.GetFirst().VisitCount;

          // Note: do NOT clear _ConsumerSkippedParentEffectiveDepth here.
          // The deferred schedule mechanism needs to persist through visiting events.
          // When the last child of the consumer-SkipNode'd parent's subtree ends,
          // the deferred schedule fires on the NEXT scheduling event (at or above
          // the saved depth). Clearing here would prevent Pattern B cases (no c)
          // from getting their required V parent.
        }

        UpdateState();

        return true;
      }

      UpdateState();

      return false;
    }

    private int[] BuildInnerPath(int innerDepth)
    {
      var path = new int[innerDepth + 1];
      for (int i = 0; i <= innerDepth && i < _CurrentInnerPath.Count; i++)
        path[i] = _CurrentInnerPath[i];
      return path;
    }

    private int AppendQueuePath(int innerDepth)
    {
      var offset = _QueuePathData.Count;
      for (int i = 0; i <= innerDepth && i < _CurrentInnerPath.Count; i++)
        _QueuePathData.Add(_CurrentInnerPath[i]);
      return offset;
    }

    private int AppendSkippedPath(int innerDepth)
    {
      var offset = _SkippedPathDataTop;
      for (int i = 0; i <= innerDepth && i < _CurrentInnerPath.Count; i++)
      {
        if (_SkippedPathDataTop < _SkippedPathData.Count)
          _SkippedPathData[_SkippedPathDataTop] = _CurrentInnerPath[i];
        else
          _SkippedPathData.Add(_CurrentInnerPath[i]);
        _SkippedPathDataTop++;
      }
      return offset;
    }

    private void RestoreInnerPathFromQueue(int offset, int pathLength)
    {
      while (_CurrentInnerPath.Count > pathLength)
        _CurrentInnerPath.RemoveAt(_CurrentInnerPath.Count - 1);
      while (_CurrentInnerPath.Count < pathLength)
        _CurrentInnerPath.Add(0);
      for (int i = 0; i < pathLength; i++)
        _CurrentInnerPath[i] = _QueuePathData[offset + i];
    }

    /// <summary>
    /// Checks if a skipped node's path (up to prefixLength elements) is a prefix of _CurrentInnerPath.
    /// Reads from the shared _SkippedPathData buffer.
    /// </summary>
    private bool IsSkippedPathPrefix(int skippedPathOffset, int skippedPathLength, int prefixLength)
    {
      if (skippedPathLength < prefixLength)
        return false;
      for (int d = 0; d < prefixLength && d < _CurrentInnerPath.Count; d++)
      {
        if (_CurrentInnerPath[d] != _SkippedPathData[skippedPathOffset + d])
          return false;
      }
      return true;
    }

    /// <summary>
    /// Counts skipped nodes that are actual ancestors of the current node being scheduled.
    /// A skipped node is an ancestor if its stored path is a prefix of _CurrentInnerPath.
    /// Reads from the shared _SkippedPathData buffer.
    /// </summary>
    private int CountSkippedAncestors()
    {
      var count = 0;
      for (int i = 0; i < _SkippedStack.Count; i++)
      {
        var skippedInfo = _SkippedStack.GetFromBack(i);

        // A skipped node can only be an ancestor if it's at a strictly shallower depth.
        // Same-depth nodes are siblings, not ancestors.
        if (skippedInfo.Position.Depth >= InnerTreenumerator.Position.Depth)
          continue;

        var skippedPathLength = skippedInfo.Position.Depth + 1;
        if (skippedPathLength <= _CurrentInnerPath.Count)
        {
          var isPrefix = true;
          for (int d = 0; d < skippedPathLength; d++)
          {
            if (d >= _CurrentInnerPath.Count || _CurrentInnerPath[d] != _SkippedPathData[skippedInfo.InnerPathOffset + d])
            {
              isPrefix = false;
              break;
            }
          }
          if (isPrefix)
            count++;
        }
      }
      return count;
    }

    private void SetRemovedSkippedSiblingIndex(int depth, int siblingIndex)
    {
      while (_RemovedSkippedSiblingIndices.Count <= depth)
        _RemovedSkippedSiblingIndices.Add(-1);
      _RemovedSkippedSiblingIndices[depth] = siblingIndex;
    }

    private int GetRemovedSkippedSiblingIndex(int depth)
    {
      if (depth < _RemovedSkippedSiblingIndices.Count)
        return _RemovedSkippedSiblingIndices[depth];
      return -1;
    }

    private void ClearRemovedSkippedSiblingIndices()
    {
      for (int i = 0; i < _RemovedSkippedSiblingIndices.Count; i++)
        _RemovedSkippedSiblingIndices[i] = -1;
    }

    /// <summary>
    /// Checks if the current node's parent (at inner depth - 1) is on the skipped stack.
    /// Used to determine whether a filtered node's parent is accepted or also filtered.
    /// </summary>
    private bool IsParentOnSkippedStack()
    {
      var parentDepth = InnerTreenumerator.Position.Depth - 1;
      if (parentDepth < 0)
        return false;

      for (int i = 0; i < _SkippedStack.Count; i++)
      {
        var skippedInfo = _SkippedStack.GetFromBack(i);
        if (skippedInfo.Position.Depth == parentDepth
          && IsSkippedPathPrefix(skippedInfo.InnerPathOffset, skippedInfo.Position.Depth + 1, parentDepth + 1))
        {
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Checks if a queue entry shares the same inner parent as the current node being scheduled.
    /// Two nodes at the same inner depth are siblings if their inner path prefixes (depth 0..D-2) match.
    /// Nodes at different inner depths cannot share a parent (they have different filtered ancestor counts).
    /// </summary>
    private bool HasSameInnerParent(ref NodeTraversalStatus queueEntry)
    {
      var currentInnerDepth = InnerTreenumerator.Position.Depth;

      // Different inner depths means different subtree contexts
      if (queueEntry.InnerDepth != currentInnerDepth)
        return false;

      // Root-level nodes (inner depth 0) share the same parent (the virtual root)
      if (currentInnerDepth == 0)
        return true;

      // Compare inner parent path (depth 0 to D-2) from queue entry's stored path
      // against _CurrentInnerPath
      var parentPathLength = currentInnerDepth; // depths 0..D-1, excluding D itself = D elements, i.e. indices 0..D-2 + depth D-1?
      // Actually: parent is at depth D-1. The parent's identity is the path from root to depth D-1.
      // That means comparing indices 0..D-2 (the path TO the parent) plus the parent's own sibling index at D-1.
      // Wait, the parent path is the full path up to and including the parent, which is depths 0..D-1 = D elements.
      // No: the inner path stores the sibling index at each depth. For a node at inner depth D, its parent
      // is uniquely identified by the path [0..D-1], which is D elements (indices 0 through D-1).
      // But we want to check if they share the same PARENT, so we compare indices 0..D-2 (the path to
      // the grandparent) and index D-1 (the parent's sibling index).
      // Actually it's simpler: the path [0..D-1] identifies the parent. Compare all D elements.
      // That IS the parent's path. If both nodes have the same path at indices 0..D-1, they share a parent.

      for (int d = 0; d < currentInnerDepth; d++)
      {
        if (d >= _CurrentInnerPath.Count)
          return false;
        var queuePathValue = _QueuePathData[queueEntry.InnerPathOffset + d];
        if (queuePathValue != _CurrentInnerPath[d])
          return false;
      }
      return true;
    }

    private NodePosition GetEffectivePosition()
    {
      // Count only skipped nodes that are actual ancestors
      var skippedAncestorCount = CountSkippedAncestors();
      var effectiveDepth = InnerTreenumerator.Position.Depth - skippedAncestorCount;

      int effectiveSiblingIndex;

      if (effectiveDepth == 0)
      {
        effectiveSiblingIndex = _SeenRootNodesCount;
      }
      else
      {
        // If we just removed a skipped parent and are scheduling its child,
        // this is the first child of that parent, so sibling index is 0
        if (_DepthOfLastRemovedSkippedParent.HasValue
          && _DepthOfLastRemovedSkippedParent.Value + 1 == effectiveDepth)
        {
          effectiveSiblingIndex = 0;
          _DepthOfLastRemovedSkippedParent = null;  // Clear after use - only affects first child
        }
        else
        {
          var previousNodePosition = _NodePositionAndVisitCounts.GetLast().Position;

          // If previous output was a visit at depth-1, we need to determine the sibling index
          if (Mode == TreenumeratorMode.VisitingNode
            && _LastVisitedNodeDepth + 1 == effectiveDepth)
          {
            var siblingIndexFromVC = _LastVisitedNodeVisitCount - 1;

            // Only check for existing siblings if this is not the first child (VC > 1)
            // For the first child (VC == 1), any existing nodes at this depth are from a previous parent
            if (_LastVisitedNodeVisitCount > 1
              && previousNodePosition.Depth == effectiveDepth
              && previousNodePosition.SiblingIndex >= siblingIndexFromVC)
            {
              // Continue from the previous sibling's position
              effectiveSiblingIndex = previousNodePosition.SiblingIndex + 1;
            }
            else
            {
              // No sibling at this depth yet (after this visit), use parent's VisitCount
              effectiveSiblingIndex = siblingIndexFromVC;
            }
          }
          // If previous output was a schedule and there's a node at the same depth, it may be a sibling.
          // However, in BFT the queue may contain nodes from different parents at the same depth.
          // We verify they share the same inner parent before treating them as siblings.
          // When HasSameInnerParent fails, the nodes might still be effective siblings if one
          // was promoted (inner depth != effective depth). This happens when a filtered node's
          // children are promoted to be siblings of the filtered node's own siblings:
          // e.g., a(b(d,e),c) with b filtered -> a(d,e,c), where d,e have innerDepth=2
          // and c has innerDepth=1 but all are effective siblings at depth 1.
          else if (previousNodePosition.Depth == effectiveDepth
            && (HasSameInnerParent(ref _NodePositionAndVisitCounts.GetLast())
                || _NodePositionAndVisitCounts.GetLast().InnerDepth != previousNodePosition.Depth
                || InnerTreenumerator.Position.Depth != effectiveDepth))
          {
            var baseIndex = previousNodePosition.SiblingIndex;

            // Also check if a recently removed consumer-skipped node had a higher sibling index.
            // This happens when a consumer-skipped node (e.g. g) was between accepted nodes (e.g. f, h):
            // f is in the queue at (0,2), g was removed at (1,2), h should be (2,2).
            var removedSiblingIndex = GetRemovedSkippedSiblingIndex(effectiveDepth);
            if (removedSiblingIndex >= 0 && removedSiblingIndex > baseIndex)
            {
              baseIndex = removedSiblingIndex;
            }

            effectiveSiblingIndex = baseIndex + 1;
          }
          // Check saved position from a recently removed skipped node
          else if (GetRemovedSkippedSiblingIndex(effectiveDepth) >= 0)
          {
            effectiveSiblingIndex = GetRemovedSkippedSiblingIndex(effectiveDepth) + 1;
          }
          else
          {
            effectiveSiblingIndex = 0;
          }
        }
      }

      return new NodePosition(effectiveSiblingIndex, effectiveDepth);
    }

    private ref NodeTraversalStatus GetNodeTraversalStatusToUpdateState()
    {
      if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        return ref _NodePositionAndVisitCounts.GetLast();

      return ref _NodePositionAndVisitCounts.GetFirst();
    }

    private void UpdateState()
    {
      Mode = InnerTreenumerator.Mode;

      ref var nodePositionAndVisitCount = ref GetNodeTraversalStatusToUpdateState();

      if (nodePositionAndVisitCount.Position.Depth == 0
        && InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
      {
        _SeenRootNodesCount++;
      }

      Node = nodePositionAndVisitCount.Node;
      VisitCount = nodePositionAndVisitCount.VisitCount;
      Position = nodePositionAndVisitCount.Position;
    }

    private struct NodeTraversalStatus
    {
      public NodeTraversalStatus(
        TNode node,
        NodePosition position,
        int visitCount,
        int innerPathOffset,
        int innerDepth,
        NodeTraversalStrategies nodeTraversalStrategies = NodeTraversalStrategies.TraverseAll)
      {
        Node = node;
        Position = position;
        VisitCount = visitCount;
        InnerPathOffset = innerPathOffset;
        InnerDepth = innerDepth;
        TraversalStrategy = nodeTraversalStrategies;
      }

      public TNode Node { get; set; }
      public NodePosition Position { get; set; }
      public int VisitCount { get; set; }
      public int InnerPathOffset { get; set; }
      public int InnerDepth { get; set; }
      public NodeTraversalStrategies TraversalStrategy { get; set; }
      public bool Skipped => TraversalStrategy != NodeTraversalStrategies.TraverseAll;

      public override string ToString() => $"({Position}), {VisitCount}, {TraversalStrategy}";
    }

    private struct SkippedNodeInfo
    {
      public SkippedNodeInfo(NodePosition position, int innerPathOffset)
      {
        Position = position;
        InnerPathOffset = innerPathOffset;
      }

      public NodePosition Position { get; }
      public int InnerPathOffset { get; }

      public override string ToString() => $"({Position})";
    }
  }
}
