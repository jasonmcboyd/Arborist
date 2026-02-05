using Arborist.Core;
using Arborist.Linq.Extensions;
using System;

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
      _NodePositionAndVisitCounts.AddLast(new NodeTraversalStatus(InnerTreenumerator.Node, InnerTreenumerator.Position, InnerTreenumerator.Position, 0));
    }

    private readonly Func<NodeContext<TNode>, bool> _Predicate;
    private readonly NodeTraversalStrategies _NodeTraversalStrategy;

    private readonly RefSemiDeque<NodeTraversalStatus> _NodePositionAndVisitCounts = new RefSemiDeque<NodeTraversalStatus>();
    private readonly RefSemiDeque<NodeVisit<TNode>> _SkippedStack = new RefSemiDeque<NodeVisit<TNode>>();

    private int _SeenRootNodesCount = 0;

    // Track the position of the last skipped node that was removed, for sibling index calculation
    private NodePosition? _LastRemovedSkippedNodePosition = null;

    // Track if we need to emit a parent visit before the next inner event
    private bool _PendingParentVisit = false;

    // Track how many extra parent visits we've emitted via _PendingParentVisit
    // These correspond to inner parent visits that we should skip
    private int _ExtraParentVisitsEmitted = 0;

    // Track a pending child schedule when we need to emit a parent visit first
    private bool _HasPendingSchedule = false;
    private NodeTraversalStatus _PendingSchedule;

    // Track how many children of the current parent (queue front) were caller-skipped.
    // This is needed because when a child is skipped, the inner BFT bypasses the parent
    // visit, so the parent's VisitCount doesn't reflect the skipped child.
    private int _SkippedChildrenOfCurrentParent = 0;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      // If we have a pending schedule from a previous parent visit injection, emit it now
      if (_HasPendingSchedule)
      {
        _HasPendingSchedule = false;
        _NodePositionAndVisitCounts.AddLast(_PendingSchedule);
        UpdateState();
        return true;
      }

      if (Mode == TreenumeratorMode.VisitingNode)
        nodeTraversalStrategies = NodeTraversalStrategies.TraverseAll;

      var previouslySeenNodeWasScheduledAndSkipped =
        Position != new NodePosition(0, -1)
        && InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode);

      if (previouslySeenNodeWasScheduledAndSkipped)
        _NodePositionAndVisitCounts.GetLast().TraversalStrategy = nodeTraversalStrategies;

      // If applying SkipSiblings to a promoted root node (effective depth 0), we need to check
      // if there are other non-skipped nodes at the same depth that would be incorrectly affected.
      // The inner BFT's SkipSiblings implementation disposes the queue front's child enumerator,
      // which is correct when the queue front is the parent, but incorrect when the queue has
      // siblings that were already processed.
      // This check applies regardless of whether SkipNode is also set.
      if (Position != new NodePosition(0, -1)
        && InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && _NodePositionAndVisitCounts.GetLast().Position.Depth == 0
        && nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipSiblings))
      {
        // Check if there are other non-skipped nodes at depth 0 that would be affected
        var hasOtherNonSkippedRoots = false;
        for (int i = 1; i < _NodePositionAndVisitCounts.Count; i++) // exclude current node (at index 0 from back)
        {
          ref var nodeStatus = ref _NodePositionAndVisitCounts.GetFromBack(i);
          if (nodeStatus.Position.Depth == 0 && !nodeStatus.Skipped)
          {
            hasOtherNonSkippedRoots = true;
            break;
          }
        }

        if (hasOtherNonSkippedRoots)
          nodeTraversalStrategies &= ~NodeTraversalStrategies.SkipSiblings;
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
          parentStatus.VisitCount++;
          _ExtraParentVisitsEmitted++;

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
          // Only pop skipped nodes when scheduling at the same or shallower depth
          // This indicates we've moved past the skipped node's subtree
          while (_SkippedStack.Count > 0 && _SkippedStack.GetLast().Position.Depth >= InnerTreenumerator.Position.Depth)
            _SkippedStack.RemoveLast();

          var skipped = _Predicate(InnerTreenumerator.ToNodeContext());

          if (skipped)
          {
            _SkippedStack.AddLast(InnerTreenumerator.ToNodeVisit());

            nodeTraversalStrategies = _NodeTraversalStrategy;

            continue;
          }
          else
          {
            var effectivePosition = GetEffectivePosition();

            var lastScheduleNodeVisitWasSkipped = _NodePositionAndVisitCounts.GetLast().Skipped;

            if (lastScheduleNodeVisitWasSkipped)
            {
              // Save the position of the skipped node for sibling index calculation
              var skippedPosition = _NodePositionAndVisitCounts.GetLast().Position;
              _LastRemovedSkippedNodePosition = skippedPosition;
              _NodePositionAndVisitCounts.RemoveLast();

              // Track skipped children of the current parent for sibling index calculation.
              // A skipped node is a child of the current parent if its depth equals the
              // parent's depth + 1 (parent is at queue front after removal).
              if (_NodePositionAndVisitCounts.Count > 0)
              {
                var frontDepth = _NodePositionAndVisitCounts.GetFirst().Position.Depth;
                if (skippedPosition.Depth == frontDepth + 1)
                  _SkippedChildrenOfCurrentParent++;
              }

              // After removing a skipped node, check if we need to emit a parent visit
              // before scheduling this child. This happens when the inner BFT's skip handling
              // jumps directly to scheduling a child without the normal parent visit sequence.
              // This fix only applies when there are filtered nodes in _SkippedStack, because
              // that's when the normal parent visit sequence is disrupted. Without filtered nodes,
              // the inner BFT handles parent visits correctly.
              if (effectivePosition.Depth > 0 && _SkippedStack.Count > 0)
              {
                // Search for a parent at the right depth that hasn't been visited yet
                // The parent should be at effectiveDepth - 1
                var parentDepth = effectivePosition.Depth - 1;
                for (int i = 0; i < _NodePositionAndVisitCounts.Count; i++)
                {
                  ref var potentialParent = ref _NodePositionAndVisitCounts.GetFromBack(i);
                  if (potentialParent.Position.Depth == parentDepth
                    && potentialParent.VisitCount == 0
                    && !potentialParent.Skipped)
                  {
                    // Need to visit parent first, cache the child schedule
                    _PendingSchedule = new NodeTraversalStatus(InnerTreenumerator.Node, effectivePosition, InnerTreenumerator.Position, 0);
                    _HasPendingSchedule = true;

                    potentialParent.VisitCount++;
                    Mode = TreenumeratorMode.VisitingNode;
                    Node = potentialParent.Node;
                    Position = potentialParent.Position;
                    VisitCount = potentialParent.VisitCount;
                    return true;
                  }
                }
              }
            }

            // Check if this is a promoted child (child of a filtered node)
            // If so, set pending parent visit flag - but only if the promoted child
            // is NOT becoming a root (effectiveDepth > 0)
            if (_SkippedStack.Count > 0
              && InnerTreenumerator.Position.Depth == _SkippedStack.GetLast().Position.Depth + 1
              && effectivePosition.Depth > 0)
            {
              _PendingParentVisit = true;
            }

            _NodePositionAndVisitCounts.AddLast(new NodeTraversalStatus(InnerTreenumerator.Node, effectivePosition, InnerTreenumerator.Position, 0));
          }
        }
        else // VisitingNode
        {
          // Check if we're visiting a filtered node
          var isVisitingFilteredNode = _SkippedStack.Count > 0
            && _SkippedStack.GetLast().Position == InnerTreenumerator.Position;

          if (isVisitingFilteredNode)
          {
            // Skip all visits to filtered nodes - the parent visits are handled via _PendingParentVisit
            continue;
          }

          // Check if this is a parent visit that we've already emitted via _PendingParentVisit
          // This happens when visiting the parent of a filtered node
          // We need to check if the inner position is the parent of ANY filtered node in the stack
          if (_ExtraParentVisitsEmitted > 0 && _SkippedStack.Count > 0)
          {
            var isParentOfAnyFilteredNode = false;
            for (int i = 0; i < _SkippedStack.Count; i++)
            {
              if (InnerTreenumerator.Position.Depth == _SkippedStack.GetFromBack(i).Position.Depth - 1)
              {
                isParentOfAnyFilteredNode = true;
                break;
              }
            }

            if (isParentOfAnyFilteredNode)
            {
              _ExtraParentVisitsEmitted--;
              continue;
            }
          }

          // Normal visiting logic
          if (InnerTreenumerator.VisitCount == 1)
          {
            _NodePositionAndVisitCounts.RemoveFirst();
            _SkippedChildrenOfCurrentParent = 0; // Reset for new parent
          }
          else if (previousModeWasVisitingNode)
            continue;

          _NodePositionAndVisitCounts.GetFirst().VisitCount++;
        }

        UpdateState();

        return true;
      }

      UpdateState();

      return false;
    }

    private NodePosition GetEffectivePosition()
    {
      // Count only skipped nodes that are actual ancestors of the current node, not siblings.
      // A skipped node at inner depth D is an ancestor if there's no non-skipped, visited node
      // at the same INNER depth (which would indicate the skipped node is a sibling).
      // We use OriginalPosition.Depth to track the inner depth of nodes.
      var ancestorSkippedCount = 0;
      for (int i = 0; i < _SkippedStack.Count; i++)
      {
        var skippedInnerDepth = _SkippedStack.GetFromBack(i).Position.Depth;

        // Check if there's a non-skipped, visited node at the same inner depth.
        // We compare against OriginalPosition.Depth which is the inner depth.
        var hasNonSkippedVisitedAtSameInnerDepth = false;
        for (int j = 0; j < _NodePositionAndVisitCounts.Count; j++)
        {
          ref var node = ref _NodePositionAndVisitCounts.GetFromBack(j);
          if (node.OriginalPosition.Depth == skippedInnerDepth && node.VisitCount > 0 && !node.Skipped)
          {
            hasNonSkippedVisitedAtSameInnerDepth = true;
            break;
          }
        }

        if (!hasNonSkippedVisitedAtSameInnerDepth)
          ancestorSkippedCount++;
      }

      var effectiveDepth = InnerTreenumerator.Position.Depth - ancestorSkippedCount;

      int effectiveSiblingIndex;

      if (effectiveDepth == 0)
      {
        effectiveSiblingIndex = _SeenRootNodesCount;
      }
      else if (Mode == TreenumeratorMode.VisitingNode)
      {
        // Use the parent's visit count, plus any children that were caller-skipped.
        // When a child is caller-skipped, the inner BFT bypasses the parent visit,
        // so VisitCount doesn't include the skipped child. We track these separately.
        effectiveSiblingIndex = _NodePositionAndVisitCounts.GetFirst().VisitCount - 1 + _SkippedChildrenOfCurrentParent;
      }
      else
      {
        // Check the queue for the last node at this depth
        var previousNodePosition = _NodePositionAndVisitCounts.GetLast().Position;

        if (previousNodePosition.Depth == effectiveDepth)
        {
          effectiveSiblingIndex = previousNodePosition.SiblingIndex + 1;
        }
        // Also check the saved position from a recently removed skipped node
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
        NodePosition originalPosition,
        int visitCount,
        NodeTraversalStrategies nodeTraversalStrategies = NodeTraversalStrategies.TraverseAll)
      {
        Node = node;
        Position = position;
        OriginalPosition = originalPosition;
        VisitCount = visitCount;
        TraversalStrategy = nodeTraversalStrategies;
      }

      public TNode Node { get; set; }
      public NodePosition Position { get; set; }
      public NodePosition OriginalPosition { get; set; }
      public int VisitCount { get; set; }
      public NodeTraversalStrategies TraversalStrategy { get; set; }
      public bool Skipped => TraversalStrategy != NodeTraversalStrategies.TraverseAll;

      public override string ToString() => $"({Position}), {VisitCount}, {TraversalStrategy}";
    }
  }
}
