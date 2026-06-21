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

    // Flat shared buffers for inner path data (avoids per-node int[] allocations).
    private readonly List<int> _QueuePathData = new List<int>();
    private readonly List<int> _SkippedPathData = new List<int>();
    private int _SkippedPathDataTop = 0;

    // Track if we need to emit a parent visit before the next inner event
    private bool _PendingParentVisit = false;

    // When a _PendingParentVisit is cancelled by consumer SkipNode, the inner BFT
    // may produce a redundant parent visit. Cleared when an accepted child is scheduled.
    private bool _ConsumeNextInnerParentVisit = false;

    // When consumer-SkipNode'd nodes are removed from the queue, track child counts
    // per depth for sibling index calculation. Index = effective depth of the removed
    // parent; value = number of accepted children scheduled so far. -1 = no removed
    // parent at that depth. Supports nested consumer-SkipNode'd parents (e.g., both
    // a at depth 0 and b at depth 1 are SkipNode'd).
    private readonly List<int> _RemovedSkippedChildCounts = new List<int>();

    // Deferred consumer strategy when a pending parent visit is emitted first.
    private NodeTraversalStrategies? _DeferredNodeTraversalStrategies = null;

    // Effective depth of a consumer-SkipNode'd node whose removal requires the wrapper
    // to generate a parent visit when scheduling exits the subtree.
    // -1 when inactive, DeferredSchedulePending when a deferred schedule is queued.
    private int _ConsumerSkippedParentEffectiveDepth = -1;
    private const int DeferredSchedulePending = int.MinValue;

    // Whether the last child in a consumer-SkipNode'd parent's subtree was itself
    // consumer-SkipNode'd. Suppresses the deferred parent visit when true.
    private bool _ConsumerSkippedChildAfterLastAccepted = false;


    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      // Output the deferred schedule from a consumer-SkipNode'd parent transition.
      if (_ConsumerSkippedParentEffectiveDepth == DeferredSchedulePending)
      {
        _ConsumerSkippedParentEffectiveDepth = -1;

        ref var deferredEntry = ref _NodePositionAndVisitCounts.GetLast();

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

      // SkipSiblings now passes straight through to the base engine, which handles it
      // correctly under promotion (including effective-root SkipSiblings ending the root
      // stream). The wrapper previously stripped and re-emulated it -- that was
      // over-compensation for the then-broken base engine and produced wrong results.

      // Emit pending parent visit (unless consumer SkipNode'd or sentinel).
      if (_PendingParentVisit && !previouslySeenNodeWasScheduledAndSkipped)
      {
        ref var parentStatus = ref _NodePositionAndVisitCounts.GetFirst();

        if (parentStatus.Position.Depth >= 0)
        {
          _PendingParentVisit = false;
          _DeferredNodeTraversalStrategies = nodeTraversalStrategies;
          parentStatus.VisitCount++;

          Mode = TreenumeratorMode.VisitingNode;
          Node = parentStatus.Node;
          Position = parentStatus.Position;
          VisitCount = parentStatus.VisitCount;
          return true;
        }
      }

      if (previouslySeenNodeWasScheduledAndSkipped)
      {
        // Only a PROMOTED child (one whose immediate inner parent was predicate-filtered, so a
        // pending parent visit was set then cancelled just above) produces a redundant inner
        // parent visit: its earlier promoted siblings' manufactured visits already advanced the
        // parent past the inner's between-children visit. A consumer-SkipNode'd node that is NOT
        // a promoted child (its inner parent is itself consumer-skipped, e.g. the last child of a
        // promoted subtree) does NOT inflate the parent's count, so the inner's owed final parent
        // visit must pass through. Gating on _PendingParentVisit (still set here; cleared below)
        // distinguishes the two and fixes the swallowed promoted-parent visit.
        if (_PendingParentVisit)
          _ConsumeNextInnerParentVisit = true;

        if (_ConsumerSkippedParentEffectiveDepth >= 0)
          _ConsumerSkippedChildAfterLastAccepted = true;
      }

      _PendingParentVisit = false;

      while (InnerTreenumerator.MoveNext(nodeTraversalStrategies))
      {
        if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        {
          var innerDepth = InnerTreenumerator.Position.Depth;
          while (_CurrentInnerPath.Count <= innerDepth)
            _CurrentInnerPath.Add(0);
          _CurrentInnerPath[innerDepth] = InnerTreenumerator.Position.SiblingIndex;

          // Pop skipped nodes at strictly deeper depths (same-depth nodes may be
          // from different subtrees; path-based counting handles correctness).
          while (_SkippedStack.Count > 0 && _SkippedStack.GetLast().Position.Depth > InnerTreenumerator.Position.Depth)
          {
            _SkippedPathDataTop = _SkippedStack.GetLast().InnerPathOffset;
            _SkippedStack.RemoveLast();
          }

          var skipped = _Predicate(InnerTreenumerator.ToNodeContext());

          if (skipped)
          {
            var skippedPathOffset = AppendSkippedPath(innerDepth);
            _SkippedStack.AddLast(new SkippedNodeInfo(InnerTreenumerator.Position, skippedPathOffset));

            nodeTraversalStrategies = _NodeTraversalStrategy;

            continue;
          }

          // --- Accepted node processing ---

          // Remove consumer-SkipNode'd nodes before calculating effective position.
          var lastScheduleNodeVisitWasSkipped = _NodePositionAndVisitCounts.GetLast().Skipped;
          var removedSkippedDepth = lastScheduleNodeVisitWasSkipped
            ? _NodePositionAndVisitCounts.GetLast().Position.Depth
            : -1;

          if (lastScheduleNodeVisitWasSkipped)
          {
            // Record the removed parent at its effective depth so its children and
            // subsequent siblings at the same depth get correct sibling indices.
            SetRemovedSkippedChildCount(removedSkippedDepth, 0);

            _NodePositionAndVisitCounts.RemoveLast();
          }

          var effectivePosition = GetEffectivePosition(out var immediateParentIsSkipped);

          if (effectivePosition.Depth > 0)
          {
            var parentDepth = effectivePosition.Depth - 1;
            if (GetRemovedSkippedChildCount(parentDepth) >= 0)
            {
              IncrementRemovedSkippedChildCount(parentDepth);
            }
            else
            {
              _NodePositionAndVisitCounts.GetFirst().AcceptedChildCount++;
            }
          }

          // If the immediate inner parent is filtered, this is a promoted child.
          // Set pending parent visit flag (unless promoted child is becoming a root).
          if (immediateParentIsSkipped && effectivePosition.Depth > 0)
            _PendingParentVisit = true;

          _ConsumeNextInnerParentVisit = false;

          // An accepted child within the SkipNode'd subtree clears the flag.
          if (_ConsumerSkippedParentEffectiveDepth >= 0
            && effectivePosition.Depth > _ConsumerSkippedParentEffectiveDepth)
            _ConsumerSkippedChildAfterLastAccepted = false;

          // Track entry into a consumer-SkipNode'd subtree.
          if (lastScheduleNodeVisitWasSkipped && removedSkippedDepth >= 0
            && effectivePosition.Depth > removedSkippedDepth)
          {
            _ConsumerSkippedParentEffectiveDepth = removedSkippedDepth;
          }
          else if (_ConsumerSkippedParentEffectiveDepth >= 0
            && effectivePosition.Depth <= _ConsumerSkippedParentEffectiveDepth
            && effectivePosition.Depth > 0
            && Mode != TreenumeratorMode.VisitingNode
            && _NodePositionAndVisitCounts.GetFirst().Position.Depth >= 0)
          {
            if (!_ConsumerSkippedChildAfterLastAccepted)
            {
              _ConsumerSkippedChildAfterLastAccepted = false;

              var deferredInnerPathOffset = AppendQueuePath(innerDepth);
              _NodePositionAndVisitCounts.AddLast(new NodeTraversalStatus(InnerTreenumerator.Node, effectivePosition, 0, deferredInnerPathOffset, innerDepth));
              _ConsumerSkippedParentEffectiveDepth = DeferredSchedulePending;

              ref var parentStatus = ref _NodePositionAndVisitCounts.GetFirst();
              parentStatus.VisitCount++;

              Mode = TreenumeratorMode.VisitingNode;
              Node = parentStatus.Node;
              Position = parentStatus.Position;
              VisitCount = parentStatus.VisitCount;

              return true;
            }

            // The last child was consumer-SkipNode'd, so no deferred V parent needed.
            _ConsumerSkippedParentEffectiveDepth = -1;
            _ConsumerSkippedChildAfterLastAccepted = false;
          }

          var innerPathOffset = AppendQueuePath(innerDepth);
          _NodePositionAndVisitCounts.AddLast(new NodeTraversalStatus(InnerTreenumerator.Node, effectivePosition, 0, innerPathOffset, innerDepth));
        }
        else // VisitingNode
        {
          // Skip redundant inner parent visit after consumer SkipNode.
          if (_ConsumeNextInnerParentVisit
            && InnerTreenumerator.VisitCount > 1)
          {
            _ConsumeNextInnerParentVisit = false;

            if (_NodePositionAndVisitCounts.GetFirst().VisitCount >= InnerTreenumerator.VisitCount)
              continue;
          }

          if (InnerTreenumerator.VisitCount == 1)
          {
            _NodePositionAndVisitCounts.RemoveFirst();
            ClearAllRemovedSkippedChildCounts();

            ref var visitedEntry = ref _NodePositionAndVisitCounts.GetFirst();
            if (visitedEntry.InnerDepth >= 0)
              RestoreInnerPathFromQueue(visitedEntry.InnerPathOffset, visitedEntry.InnerDepth + 1);
          }
          else if (Mode == TreenumeratorMode.VisitingNode)
            continue;

          _NodePositionAndVisitCounts.GetFirst().VisitCount++;
        }

        UpdateState();

        return true;
      }

      UpdateState();

      return false;
    }

    private int AppendQueuePath(int innerDepth)
    {
      var offset = _QueuePathData.Count;
      for (int i = 0; i <= innerDepth && i < _CurrentInnerPath.Count; i++)
        _QueuePathData.Add(_CurrentInnerPath[i]);
      return offset;
    }

    private int GetRemovedSkippedChildCount(int depth)
    {
      if (depth < 0 || depth >= _RemovedSkippedChildCounts.Count)
        return -1;
      return _RemovedSkippedChildCounts[depth];
    }

    private void SetRemovedSkippedChildCount(int depth, int count)
    {
      while (_RemovedSkippedChildCounts.Count <= depth)
        _RemovedSkippedChildCounts.Add(-1);
      _RemovedSkippedChildCounts[depth] = count;
    }

    private void IncrementRemovedSkippedChildCount(int depth)
    {
      _RemovedSkippedChildCounts[depth]++;
    }

    private void ClearAllRemovedSkippedChildCounts()
    {
      for (int i = 0; i < _RemovedSkippedChildCounts.Count; i++)
        _RemovedSkippedChildCounts[i] = -1;
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
      if (_CurrentInnerPath.Count > pathLength)
        _CurrentInnerPath.RemoveRange(pathLength, _CurrentInnerPath.Count - pathLength);
      while (_CurrentInnerPath.Count < pathLength)
        _CurrentInnerPath.Add(0);
      for (int i = 0; i < pathLength; i++)
        _CurrentInnerPath[i] = _QueuePathData[offset + i];
    }

    private int CountSkippedAncestors(out bool immediateParentIsSkipped)
    {
      var count = 0;
      immediateParentIsSkipped = false;
      var parentDepth = InnerTreenumerator.Position.Depth - 1;

      for (int i = 0; i < _SkippedStack.Count; i++)
      {
        var skippedInfo = _SkippedStack.GetFromBack(i);

        if (skippedInfo.Position.Depth >= InnerTreenumerator.Position.Depth)
          continue;

        var pathLength = skippedInfo.Position.Depth + 1;
        var isAncestor = true;
        for (int d = 0; d < pathLength; d++)
        {
          if (_CurrentInnerPath[d] != _SkippedPathData[skippedInfo.InnerPathOffset + d])
          {
            isAncestor = false;
            break;
          }
        }

        if (isAncestor)
        {
          count++;
          if (skippedInfo.Position.Depth == parentDepth)
            immediateParentIsSkipped = true;
        }
      }
      return count;
    }

    private NodePosition GetEffectivePosition(out bool immediateParentIsSkipped)
    {
      var skippedAncestorCount = CountSkippedAncestors(out immediateParentIsSkipped);
      var effectiveDepth = InnerTreenumerator.Position.Depth - skippedAncestorCount;

      int effectiveSiblingIndex;

      if (effectiveDepth == 0)
      {
        effectiveSiblingIndex = _SeenRootNodesCount;
      }
      else
      {
        // Check if this is a child of a recently removed consumer-SkipNode'd parent
        var parentDepth = effectiveDepth - 1;
        var removedChildCount = GetRemovedSkippedChildCount(parentDepth);
        if (removedChildCount >= 0)
        {
          effectiveSiblingIndex = removedChildCount;
        }
        else
        {
          effectiveSiblingIndex = _NodePositionAndVisitCounts.GetFirst().AcceptedChildCount;
        }
      }

      return new NodePosition(effectiveSiblingIndex, effectiveDepth);
    }

    private void UpdateState()
    {
      var isScheduling = InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode;

      ref var entry = ref isScheduling
        ? ref _NodePositionAndVisitCounts.GetLast()
        : ref _NodePositionAndVisitCounts.GetFirst();

      Mode = InnerTreenumerator.Mode;
      Node = entry.Node;
      VisitCount = entry.VisitCount;
      Position = entry.Position;

      if (isScheduling && entry.Position.Depth == 0)
        _SeenRootNodesCount++;
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
        AcceptedChildCount = 0;
      }

      public TNode Node { get; }
      public NodePosition Position { get; }
      public int VisitCount { get; set; }
      public int InnerPathOffset { get; }
      public int InnerDepth { get; }
      public NodeTraversalStrategies TraversalStrategy { get; set; }
      public bool Skipped => TraversalStrategy != NodeTraversalStrategies.TraverseAll;
      public int AcceptedChildCount { get; set; }

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
