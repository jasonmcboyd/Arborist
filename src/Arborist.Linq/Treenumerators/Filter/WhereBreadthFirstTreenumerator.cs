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
      _NodePositionAndVisitCounts.AddLast(new NodeTraversalStatus(InnerTreenumerator.Node, InnerTreenumerator.Position, 0));
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

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (Mode == TreenumeratorMode.VisitingNode)
        nodeTraversalStrategies = NodeTraversalStrategies.TraverseAll;

      var previouslySeenNodeWasScheduledAndSkipped =
        Position != new NodePosition(0, -1)
        && InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode);

      if (previouslySeenNodeWasScheduledAndSkipped)
        _NodePositionAndVisitCounts.GetLast().TraversalStrategy = nodeTraversalStrategies;

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
              _LastRemovedSkippedNodePosition = _NodePositionAndVisitCounts.GetLast().Position;
              _NodePositionAndVisitCounts.RemoveLast();
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

            _NodePositionAndVisitCounts.AddLast(new NodeTraversalStatus(InnerTreenumerator.Node, effectivePosition, 0));
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
            _NodePositionAndVisitCounts.RemoveFirst();
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
      var effectiveDepth = InnerTreenumerator.Position.Depth - _SkippedStack.Count;

      int effectiveSiblingIndex;

      if (effectiveDepth == 0)
      {
        effectiveSiblingIndex = _SeenRootNodesCount;
      }
      else if (Mode == TreenumeratorMode.VisitingNode)
      {
        effectiveSiblingIndex = _NodePositionAndVisitCounts.GetFirst().VisitCount - 1;
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
        int visitCount,
        NodeTraversalStrategies nodeTraversalStrategies = NodeTraversalStrategies.TraverseAll)
      {
        Node = node;
        Position = position;
        VisitCount = visitCount;
        TraversalStrategy = nodeTraversalStrategies;
      }

      public TNode Node { get; set; }
      public NodePosition Position { get; set; }
      public int VisitCount { get; set; }
      public NodeTraversalStrategies TraversalStrategy { get; set; }
      public bool Skipped => TraversalStrategy != NodeTraversalStrategies.TraverseAll;

      public override string ToString() => $"({Position}), {VisitCount}, {TraversalStrategy}";
    }
  }
}
