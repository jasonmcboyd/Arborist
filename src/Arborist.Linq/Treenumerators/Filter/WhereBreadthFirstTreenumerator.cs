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

    // Track the position of the last skipped node that was removed, for sibling index calculation
    private NodePosition? _LastRemovedSkippedNodePosition = null;

    // Track if we need to emit a parent visit before the next inner event
    private bool _PendingParentVisit = false;

    // Track how many extra parent visits we've emitted via _PendingParentVisit
    // These correspond to inner parent visits that we should skip
    private int _ExtraParentVisitsEmitted = 0;

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


    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
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

      // If a promoted node (outer depth differs from inner depth due to filtered ancestors)
      // has SkipSiblings, we may need to strip it from the strategy passed to the inner
      // treenumerator. The inner BFT's SkipSiblings disposes the queue front's child
      // enumerator, which would damage unrelated subtrees already in the inner queue.
      //
      // However, when the inner queue is empty (no other accepted nodes), SkipSiblings
      // safely sets _RootsEnumeratorFinished = true, correctly terminating the traversal.
      // This is needed when the entire ancestor chain was SkipNode'd/filtered — the inner
      // BFT's stack still holds those ancestors, but nothing is queued for visiting.
      //
      // We approximate "inner queue is empty" by checking our own queue: if it only contains
      // the sentinel and the current node (Count <= 2), there are no other accepted nodes.
      if (Position != new NodePosition(0, -1)
        && InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipSiblings))
      {
        var outerDepth = _NodePositionAndVisitCounts.GetLast().Position.Depth;
        var innerDepth = InnerTreenumerator.Position.Depth;
        if (outerDepth != innerDepth && _NodePositionAndVisitCounts.Count > 2)
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
          _ExtraParentVisitsEmitted++;

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
                _LastRemovedSkippedNodePosition = _NodePositionAndVisitCounts.GetLast().Position;
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

            var innerPathOffset = AppendQueuePath(innerDepth);
            _NodePositionAndVisitCounts.AddLast(new NodeTraversalStatus(InnerTreenumerator.Node, effectivePosition, 0, innerPathOffset, innerDepth));
          }
        }
        else // VisitingNode
        {
          // When visiting a node, clear saved positions - we're entering a new subtree
          _LastRemovedSkippedNodePosition = null;

          var innerDepth = InnerTreenumerator.Position.Depth;
          var innerSiblingIndex = InnerTreenumerator.Position.SiblingIndex;

          // When visiting a node at depth D, pop skipped nodes at depth D (siblings that were skipped)
          while (_SkippedStack.Count > 0 && _SkippedStack.GetLast().Position.Depth == innerDepth)
          {
            _SkippedPathDataTop = _SkippedStack.GetLast().InnerPathOffset;
            _SkippedStack.RemoveLast();
          }

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

          // Check if this is a parent visit that we've already emitted via _PendingParentVisit.
          // Use full path prefix comparison to verify actual parent-of-filtered relationship.
          // Single-level comparison is insufficient when nodes from different subtrees share
          // the same inner sibling index at the same depth.
          if (_ExtraParentVisitsEmitted > 0 && _SkippedStack.Count > 0)
          {
            var isActualParentOfFilteredNode = false;
            for (int i = 0; i < _SkippedStack.Count; i++)
            {
              var skippedInfo = _SkippedStack.GetFromBack(i);
              var skippedPathLength = skippedInfo.Position.Depth + 1;
              if (innerDepth == skippedInfo.Position.Depth - 1
                && visitingNodePathLength > 0
                && skippedPathLength > innerDepth)
              {
                var pathPrefixMatches = true;
                for (int j = 0; j <= innerDepth && j < visitingNodePathLength; j++)
                {
                  if (_SkippedPathData[skippedInfo.InnerPathOffset + j] != _QueuePathData[visitingNodePathOffset + j])
                  {
                    pathPrefixMatches = false;
                    break;
                  }
                }
                if (pathPrefixMatches)
                {
                  isActualParentOfFilteredNode = true;
                  break;
                }
              }
            }

            if (isActualParentOfFilteredNode)
            {
              _ExtraParentVisitsEmitted--;
              continue;
            }
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
          // If previous output was a schedule and there's a node at the same depth, it's a sibling
          else if (previousNodePosition.Depth == effectiveDepth)
          {
            var baseIndex = previousNodePosition.SiblingIndex;

            // Also check if a recently removed consumer-skipped node had a higher sibling index.
            // This happens when a consumer-skipped node (e.g. g) was between accepted nodes (e.g. f, h):
            // f is in the queue at (0,2), g was removed at (1,2), h should be (2,2).
            if (_LastRemovedSkippedNodePosition.HasValue
              && _LastRemovedSkippedNodePosition.Value.Depth == effectiveDepth
              && _LastRemovedSkippedNodePosition.Value.SiblingIndex > baseIndex)
            {
              baseIndex = _LastRemovedSkippedNodePosition.Value.SiblingIndex;
            }

            effectiveSiblingIndex = baseIndex + 1;
          }
          // Check saved position from a recently removed skipped node
          else if (_LastRemovedSkippedNodePosition.HasValue
            && _LastRemovedSkippedNodePosition.Value.Depth == effectiveDepth)
          {
            effectiveSiblingIndex = _LastRemovedSkippedNodePosition.Value.SiblingIndex + 1;
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
