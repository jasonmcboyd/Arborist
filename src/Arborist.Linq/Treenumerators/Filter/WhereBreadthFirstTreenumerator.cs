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
      _NodePositionAndVisitCounts.AddLast(new NodeTraversalStatus(InnerTreenumerator.Node, InnerTreenumerator.Position, 0, -1));
    }

    private readonly Func<NodeContext<TNode>, bool> _Predicate;
    private readonly NodeTraversalStrategies _NodeTraversalStrategy;

    private readonly RefSemiDeque<NodeTraversalStatus> _NodePositionAndVisitCounts = new RefSemiDeque<NodeTraversalStatus>();

    private int _SeenRootNodesCount = 0;

    // Incremental predicate-skipped-ancestor prefix: _PredSkipPrefix[d] = number of
    // PREDICATE-skipped nodes among inner depths 0..d on the current path. (Consumer-
    // SkipNode'd nodes are predicate-accepted, so they do NOT count here -- they are
    // handled by _RemovedSkippedChildCounts.) This gives an O(1) skipped-ancestor lookup
    // with no per-node stack scan. It is maintained incrementally: each scheduled node sets
    // its own depth (live), and when the queue front advances the prefix is re-anchored at
    // the new front's depth in O(1) (RestoreSkipPrefixAtFront). Because the inner schedules
    // depths contiguously from the front downward, deeper entries are always rewritten before
    // they are read, so no full-path copy/restore is needed.
    private readonly List<int> _PredSkipPrefix = new List<int>();

    private int PrefixSkippedAncestorCount(out bool immediateParentIsSkipped)
    {
      var depth = InnerTreenumerator.Position.Depth;
      if (depth == 0)
      {
        immediateParentIsSkipped = false;
        return 0;
      }
      var parentPrefix = _PredSkipPrefix[depth - 1];
      var grandparentPrefix = depth > 1 ? _PredSkipPrefix[depth - 2] : 0;
      immediateParentIsSkipped = parentPrefix > grandparentPrefix;
      return parentPrefix;
    }

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
          while (_PredSkipPrefix.Count <= innerDepth)
            _PredSkipPrefix.Add(0);

          var skipped = _Predicate(InnerTreenumerator.ToNodeContext());

          _PredSkipPrefix[innerDepth] = (innerDepth > 0 ? _PredSkipPrefix[innerDepth - 1] : 0) + (skipped ? 1 : 0);

          if (skipped)
          {
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

              _NodePositionAndVisitCounts.AddLast(new NodeTraversalStatus(InnerTreenumerator.Node, effectivePosition, 0, innerDepth));
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

          _NodePositionAndVisitCounts.AddLast(new NodeTraversalStatus(InnerTreenumerator.Node, effectivePosition, 0, innerDepth));
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
              RestoreSkipPrefixAtFront(visitedEntry.InnerDepth, visitedEntry.InnerDepth - visitedEntry.Position.Depth);
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

    // Re-anchor the live skipped-ancestor prefix at the new queue front in O(1). The front
    // is an accepted (predicate-kept) node, so it contributes 0 to the prefix; hence
    // prefix[frontDepth] == prefix[frontDepth-1] == frontSkipPrefix. Both are set so the
    // front's first-scheduled child can read its grandparent slot; deeper slots are rewritten
    // live as the inner schedules the front's descendants contiguously downward.
    private void RestoreSkipPrefixAtFront(int frontInnerDepth, int frontSkipPrefix)
    {
      while (_PredSkipPrefix.Count <= frontInnerDepth)
        _PredSkipPrefix.Add(0);
      _PredSkipPrefix[frontInnerDepth] = frontSkipPrefix;
      if (frontInnerDepth > 0)
        _PredSkipPrefix[frontInnerDepth - 1] = frontSkipPrefix;
    }

    private NodePosition GetEffectivePosition(out bool immediateParentIsSkipped)
    {
      var skippedAncestorCount = PrefixSkippedAncestorCount(out immediateParentIsSkipped);
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
        int innerDepth,
        NodeTraversalStrategies nodeTraversalStrategies = NodeTraversalStrategies.TraverseAll)
      {
        Node = node;
        Position = position;
        VisitCount = visitCount;
        InnerDepth = innerDepth;
        TraversalStrategy = nodeTraversalStrategies;
        AcceptedChildCount = 0;
      }

      public TNode Node { get; }
      public NodePosition Position { get; }
      public int VisitCount { get; set; }
      public int InnerDepth { get; }
      public NodeTraversalStrategies TraversalStrategy { get; set; }
      public bool Skipped => TraversalStrategy != NodeTraversalStrategies.TraverseAll;
      public int AcceptedChildCount { get; set; }

      public override string ToString() => $"({Position}), {VisitCount}, {TraversalStrategy}";
    }
  }
}
