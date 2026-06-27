using Arborist.Core;
using Arborist.Linq.Extensions;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  /// <summary>
  /// Breadth-first <c>Where</c>: filters the inner stream, promoting a predicate-skipped node's
  /// children into its slot, and re-emits the SAME visit multiset the base engine would for the
  /// filtered tree -- in level order.
  ///
  /// <para>State splits into three orthogonal axes:</para>
  /// <list type="bullet">
  /// <item><b>The accepted queue</b> (<see cref="_AcceptedQueue"/>): the base engine's visit-queue
  /// analogue; the front is the active effective parent.</item>
  /// <item><b>The predicate-skipped-ancestor prefix carry</b> (regioned, off-limits): an O(1)
  /// effective-depth lookup with O(stored-skip-depth) memory.</item>
  /// <item><b>The consumer-SkipNode axis</b> (regioned, off-limits): orthogonal to predicate-skip;
  /// the consumer drops a predicate-accepted node.</item>
  /// </list>
  ///
  /// <para>The front parent's return-visit cadence against the inner stream is a single token
  /// (<see cref="_FrontReturnVisit"/>) -- the wrapper's analogue of the base engine's one-bit
  /// <c>FrontSlotEnqueuedNode</c>, widened to three states because the wrapper must both MANUFACTURE
  /// a visit the inner never emits and SUPPRESS one it did.</para>
  /// </summary>
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
      _AcceptedQueue.AddLast(new AcceptedFrame(InnerTreenumerator.Node, InnerTreenumerator.Position, 0, -1));
    }

    private readonly Func<NodeContext<TNode>, bool> _Predicate;
    private readonly NodeTraversalStrategies _NodeTraversalStrategy;

    private readonly RefSemiDeque<AcceptedFrame> _AcceptedQueue = new RefSemiDeque<AcceptedFrame>();

    private int _RootNodesSeen = 0;

    #region Off-limits: predicate-skipped-ancestor prefix carry (f7eae61 O(N) time, ca567e0 O(1)-depth memory)

    // Incremental predicate-skipped-ancestor prefix: PrefixRead(d) = number of
    // PREDICATE-skipped nodes among inner depths 0..d on the current path. (Consumer-
    // SkipNode'd nodes are predicate-accepted, so they do NOT count here -- they are
    // handled by _RemovedSkippedChildCounts.) This gives an O(1) skipped-ancestor lookup
    // with no per-node stack scan. It is maintained incrementally: each scheduled node sets
    // its own depth (live), and when the queue front advances the prefix is re-anchored at
    // the new front's depth in O(1) (PrefixAnchor). Because the inner schedules depths
    // contiguously from the front downward, deeper entries are always rewritten before they
    // are read, so no full-path copy/restore is needed.
    //
    // STORAGE (tail-carry): the prefix is monotonic non-decreasing in depth and CONSTANT
    // beyond the deepest predicate-skipped ancestor on the current path (accepted nodes add 0).
    // So we store explicit per-depth entries only up to the deepest depth that differs from the
    // running tail value; reads past _PrefixStoredCount return _PrefixTail. With no predicate
    // skips (WhereAll) the stored region stays empty -- memory is O(stored skip depth), not
    // O(inner depth). This is the only thing that differs from a flat List<int> indexed by
    // absolute depth; the logical values are identical (verified value-by-value against the old
    // flat-list logic across the full Where2InProcessScan via a temporary validate-alongside shadow).
    private readonly List<int> _PrefixStored = new List<int>();
    private int _PrefixStoredCount = 0;
    private int _PrefixTail = 0;

    private int PrefixRead(int depth)
    {
      if (depth < 0)
        return 0;
      if (depth < _PrefixStoredCount)
        return _PrefixStored[depth];
      return _PrefixTail;
    }

    // Set the prefix at inner depth `depth` to `value` for the current scheduled node.
    // Only materializes stored entries when `value` exceeds the constant tail (i.e. a
    // predicate skip pushed the running count above the tail); equal-to-tail writes in the
    // accepted region are no-ops and allocate nothing.
    private void PrefixWriteScheduled(int depth, int value)
    {
      if (depth < _PrefixStoredCount)
      {
        _PrefixStored[depth] = value;
        return;
      }

      if (value == _PrefixTail)
        return; // still on the constant tail -- nothing to store.

      // A skip raised the count above the tail. Materialize [_PrefixStoredCount .. depth]:
      // the gap entries are still on the (old) tail, then this depth gets `value`.
      while (_PrefixStoredCount < depth)
      {
        if (_PrefixStoredCount < _PrefixStored.Count)
          _PrefixStored[_PrefixStoredCount] = _PrefixTail;
        else
          _PrefixStored.Add(_PrefixTail);
        _PrefixStoredCount++;
      }
      if (_PrefixStoredCount < _PrefixStored.Count)
        _PrefixStored[_PrefixStoredCount] = value;
      else
        _PrefixStored.Add(value);
      _PrefixStoredCount++;
    }

    private int PrefixSkippedAncestorCount(out bool immediateParentIsSkipped)
    {
      var depth = InnerTreenumerator.Position.Depth;
      if (depth == 0)
      {
        immediateParentIsSkipped = false;
        return 0;
      }
      var parentPrefix = PrefixRead(depth - 1);
      var grandparentPrefix = depth > 1 ? PrefixRead(depth - 2) : 0;
      immediateParentIsSkipped = parentPrefix > grandparentPrefix;
      return parentPrefix;
    }

    // Re-anchor the live skipped-ancestor prefix at the new queue front in O(1). The front
    // is an accepted (predicate-kept) node, so it contributes 0 to the prefix; hence
    // prefix[frontDepth] == prefix[frontDepth-1] == frontSkipPrefix, and EVERYTHING below the
    // front on the go-forward path is the constant frontSkipPrefix until the next skip. So we
    // make frontSkipPrefix the tail and drop stored entries at/above frontDepth-1: deeper slots
    // are re-materialized live (PrefixWriteScheduled) as the inner schedules the front's
    // descendants contiguously downward. Ancestor entries above the front (depths <
    // frontDepth-1, whose counts may be smaller) stay stored. This is what reclaims the memory
    // the old absolute-depth List<int> never released.
    private void PrefixAnchor(int frontInnerDepth, int frontSkipPrefix)
    {
      var keep = frontInnerDepth > 0 ? frontInnerDepth - 1 : 0;
      if (_PrefixStoredCount > keep)
        _PrefixStoredCount = keep;
      _PrefixTail = frontSkipPrefix;
    }

    #endregion Off-limits: predicate-skipped-ancestor prefix carry

    // The front (active effective parent)'s return-visit cadence against the inner stream.
    // The wrapper must do TWO things the base engine never does: MANUFACTURE a visit the inner
    // never emits (Owed), and SUPPRESS a visit the inner DID emit (SuppressNextInner). Distinct,
    // not one bit.
    private enum FrontReturnVisit
    {
      None,               // Front owes nothing; pass inner parent visits through untouched.
      Owed,               // A promoted child was scheduled; manufacture the front's return visit next.
      SuppressNextInner,  // An Owed return visit was CANCELLED by a consumer SkipNode (earlier promoted
                          // siblings already advanced the front past the inner's between-children visit);
                          // swallow that now-redundant inner parent visit ONCE. Armed ONLY by the
                          // consumer-skip-cancel gate -- the normal emit path does NOT arm it (it relies
                          // on the Mode==Visiting / VisitCount comparison instead).
    }
    private FrontReturnVisit _FrontReturnVisit = FrontReturnVisit.None;

    // The consumer strategy held over a manufactured (Owed) parent visit, to be applied to the
    // child's own visiting turn that follows it.
    private NodeTraversalStrategies? _DeferredStrategy = null;

    #region Off-limits: consumer-SkipNode axis (orthogonal to predicate-skip promotion)

    // When consumer-SkipNode'd nodes are removed from the queue, track child counts
    // per depth for sibling index calculation. Index = effective depth of the removed
    // parent; value = number of accepted children scheduled so far. -1 = no removed
    // parent at that depth. Supports nested consumer-SkipNode'd parents (e.g., both
    // a at depth 0 and b at depth 1 are SkipNode'd).
    private readonly List<int> _RemovedSkippedChildCounts = new List<int>();

    // Effective depth of a consumer-SkipNode'd node whose removal requires the wrapper
    // to generate a parent visit when scheduling exits the subtree.
    // -1 when inactive, DeferredSchedulePending when a deferred schedule is queued.
    private int _ConsumerSkippedSubtreeDepth = -1;
    private const int DeferredSchedulePending = int.MinValue;

    // Whether the last child in a consumer-SkipNode'd parent's subtree was itself
    // consumer-SkipNode'd. Suppresses the deferred parent visit when true.
    private bool _ConsumerSkippedChildAfterLastAccepted = false;

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

    #endregion Off-limits: consumer-SkipNode axis

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      // Output the deferred schedule from a consumer-SkipNode'd parent transition.
      if (_ConsumerSkippedSubtreeDepth == DeferredSchedulePending)
      {
        _ConsumerSkippedSubtreeDepth = -1;

        ref var deferredEntry = ref _AcceptedQueue.GetLast();

        Mode = TreenumeratorMode.SchedulingNode;
        Node = deferredEntry.Node;
        Position = deferredEntry.Position;
        VisitCount = deferredEntry.VisitCount;

        return true;
      }

      if (Mode == TreenumeratorMode.VisitingNode)
      {
        if (_DeferredStrategy.HasValue)
        {
          nodeTraversalStrategies = _DeferredStrategy.Value;
          _DeferredStrategy = null;
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
        _AcceptedQueue.GetLast().TraversalStrategy = nodeTraversalStrategies;

      // SkipSiblings now passes straight through to the base engine, which handles it
      // correctly under promotion (including effective-root SkipSiblings ending the root
      // stream). The wrapper previously stripped and re-emulated it -- that was
      // over-compensation for the then-broken base engine and produced wrong results.

      // Manufacture the front's owed return visit (unless this turn consumer-SkipNode's the child,
      // which cancels the owing, or the front is the sentinel).
      if (_FrontReturnVisit == FrontReturnVisit.Owed && !previouslySeenNodeWasScheduledAndSkipped)
      {
        ref var parentStatus = ref _AcceptedQueue.GetFirst();

        if (parentStatus.Position.Depth >= 0)
        {
          // The manufactured visit is emitted here. The inner's redundant between-children parent
          // visit is suppressed downstream by the Mode==Visiting / VisitCount comparison, not by
          // SuppressNextInner (that token is reserved for the consumer-skip-cancel 32b1f8f gate).
          _FrontReturnVisit = FrontReturnVisit.None;
          _DeferredStrategy = nodeTraversalStrategies;
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
        // THE 32b1f8f GATE. A consumer-skip that cancels an Owed return visit means we already
        // manufactured the front's between-children visit for this slot's earlier promoted siblings,
        // so the inner's matching parent visit is now redundant: arm its one-shot suppression. The
        // transition is Owed -> SuppressNextInner ONLY -- a not-Owed front leaves the inner's owed
        // visit to pass through, so a None-origin consumer-skip can never arm suppression and the
        // 32b1f8f swallowed-visit bug is structurally impossible.
        if (_FrontReturnVisit == FrontReturnVisit.Owed)
          _FrontReturnVisit = FrontReturnVisit.SuppressNextInner;

        if (_ConsumerSkippedSubtreeDepth >= 0)
          _ConsumerSkippedChildAfterLastAccepted = true;
      }

      // Owed but never emitted (the emit block fell through on the sentinel front): drop it.
      if (_FrontReturnVisit == FrontReturnVisit.Owed)
        _FrontReturnVisit = FrontReturnVisit.None;

      while (InnerTreenumerator.MoveNext(nodeTraversalStrategies))
      {
        if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        {
          var innerDepth = InnerTreenumerator.Position.Depth;

          var skipped = _Predicate(InnerTreenumerator.ToNodeContext());

          PrefixWriteScheduled(innerDepth, (innerDepth > 0 ? PrefixRead(innerDepth - 1) : 0) + (skipped ? 1 : 0));

          if (skipped)
          {
            nodeTraversalStrategies = _NodeTraversalStrategy;

            continue;
          }

          // --- Accepted node processing ---

          // Remove consumer-SkipNode'd nodes before calculating effective position.
          var lastScheduleNodeVisitWasSkipped = _AcceptedQueue.GetLast().Skipped;
          var removedSkippedDepth = lastScheduleNodeVisitWasSkipped
            ? _AcceptedQueue.GetLast().Position.Depth
            : -1;

          if (lastScheduleNodeVisitWasSkipped)
          {
            // Record the removed parent at its effective depth so its children and
            // subsequent siblings at the same depth get correct sibling indices.
            SetRemovedSkippedChildCount(removedSkippedDepth, 0);

            _AcceptedQueue.RemoveLast();
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
              _AcceptedQueue.GetFirst().AcceptedChildCount++;
            }
          }

          // Reset the front's cadence for this freshly accepted child. A PROMOTED child (its
          // immediate inner parent was predicate-filtered, and it is not itself an effective root)
          // owes the front a manufactured return visit; anything else clears the token, dropping any
          // prior suppression.
          _FrontReturnVisit = immediateParentIsSkipped && effectivePosition.Depth > 0
            ? FrontReturnVisit.Owed
            : FrontReturnVisit.None;

          // An accepted child within the SkipNode'd subtree clears the flag.
          if (_ConsumerSkippedSubtreeDepth >= 0
            && effectivePosition.Depth > _ConsumerSkippedSubtreeDepth)
            _ConsumerSkippedChildAfterLastAccepted = false;

          // Track entry into a consumer-SkipNode'd subtree.
          if (lastScheduleNodeVisitWasSkipped && removedSkippedDepth >= 0
            && effectivePosition.Depth > removedSkippedDepth)
          {
            _ConsumerSkippedSubtreeDepth = removedSkippedDepth;
          }
          else if (_ConsumerSkippedSubtreeDepth >= 0
            && effectivePosition.Depth <= _ConsumerSkippedSubtreeDepth
            && effectivePosition.Depth > 0
            && Mode != TreenumeratorMode.VisitingNode
            && _AcceptedQueue.GetFirst().Position.Depth >= 0)
          {
            if (!_ConsumerSkippedChildAfterLastAccepted)
            {
              _ConsumerSkippedChildAfterLastAccepted = false;

              _AcceptedQueue.AddLast(new AcceptedFrame(InnerTreenumerator.Node, effectivePosition, 0, innerDepth));
              _ConsumerSkippedSubtreeDepth = DeferredSchedulePending;

              ref var parentStatus = ref _AcceptedQueue.GetFirst();
              parentStatus.VisitCount++;

              Mode = TreenumeratorMode.VisitingNode;
              Node = parentStatus.Node;
              Position = parentStatus.Position;
              VisitCount = parentStatus.VisitCount;

              return true;
            }

            // The last child was consumer-SkipNode'd, so no deferred V parent needed.
            _ConsumerSkippedSubtreeDepth = -1;
            _ConsumerSkippedChildAfterLastAccepted = false;
          }

          _AcceptedQueue.AddLast(new AcceptedFrame(InnerTreenumerator.Node, effectivePosition, 0, innerDepth));
        }
        else // VisitingNode
        {
          // Swallow the inner's now-redundant between-children parent visit ONCE.
          if (_FrontReturnVisit == FrontReturnVisit.SuppressNextInner
            && InnerTreenumerator.VisitCount > 1)
          {
            _FrontReturnVisit = FrontReturnVisit.None;

            if (_AcceptedQueue.GetFirst().VisitCount >= InnerTreenumerator.VisitCount)
              continue;
          }

          if (InnerTreenumerator.VisitCount == 1)
          {
            _AcceptedQueue.RemoveFirst();
            ClearAllRemovedSkippedChildCounts();

            ref var visitedEntry = ref _AcceptedQueue.GetFirst();
            if (visitedEntry.InnerDepth >= 0)
              PrefixAnchor(visitedEntry.InnerDepth, visitedEntry.InnerDepth - visitedEntry.Position.Depth);
          }
          else if (Mode == TreenumeratorMode.VisitingNode)
            continue;

          _AcceptedQueue.GetFirst().VisitCount++;
        }

        UpdateState();

        return true;
      }

      UpdateState();

      return false;
    }

    private NodePosition GetEffectivePosition(out bool immediateParentIsSkipped)
    {
      var skippedAncestorCount = PrefixSkippedAncestorCount(out immediateParentIsSkipped);
      var effectiveDepth = InnerTreenumerator.Position.Depth - skippedAncestorCount;

      int effectiveSiblingIndex;

      if (effectiveDepth == 0)
      {
        effectiveSiblingIndex = _RootNodesSeen;
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
          effectiveSiblingIndex = _AcceptedQueue.GetFirst().AcceptedChildCount;
        }
      }

      return new NodePosition(effectiveSiblingIndex, effectiveDepth);
    }

    private void UpdateState()
    {
      var isScheduling = InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode;

      ref var entry = ref isScheduling
        ? ref _AcceptedQueue.GetLast()
        : ref _AcceptedQueue.GetFirst();

      Mode = InnerTreenumerator.Mode;
      Node = entry.Node;
      VisitCount = entry.VisitCount;
      Position = entry.Position;

      if (isScheduling && entry.Position.Depth == 0)
        _RootNodesSeen++;
    }

    private struct AcceptedFrame
    {
      public AcceptedFrame(
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
