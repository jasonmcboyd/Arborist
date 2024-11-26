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
      NodeTraversalStrategies nodeTraversalStrategies)
      : base(innerTreenumeratorFactory)
    {
      _Predicate = predicate;
      _TraversalStrategy = nodeTraversalStrategies;
      _NodePositionAndVisitCounts.AddLast(new NodeTraversalStatus(InnerTreenumerator.Position, 0));
    }

    private readonly Func<NodeContext<TNode>, bool> _Predicate;
    private readonly NodeTraversalStrategies _TraversalStrategy;

    private readonly RefSemiDeque<NodeTraversalStatus> _NodePositionAndVisitCounts = new RefSemiDeque<NodeTraversalStatus>();
    private readonly RefSemiDeque<NodeVisit<TNode>> _SkippedStack = new RefSemiDeque<NodeVisit<TNode>>();

    private int _SeenRootNodesCount = 0;

    private bool _EnumerationFinished = false;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (_EnumerationFinished)
        return false;

      if (Mode == TreenumeratorMode.VisitingNode)
        nodeTraversalStrategies = NodeTraversalStrategies.TraverseAll;

      var previouslySeenNodeWasScheduledAndSkipped =
        Position != new NodePosition(0, -1)
        && InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && (nodeTraversalStrategies == NodeTraversalStrategies.SkipNode || nodeTraversalStrategies == NodeTraversalStrategies.SkipNodeAndDescendants);

      if (previouslySeenNodeWasScheduledAndSkipped)
        _NodePositionAndVisitCounts.GetLast().TraversalStrategy = nodeTraversalStrategies;

      var previousModeWasVisitingNode = Mode == TreenumeratorMode.VisitingNode;

      while (InnerTreenumerator.MoveNext(nodeTraversalStrategies))
      {
        while (_SkippedStack.Count > 0 && _SkippedStack.GetLast().Position.Depth >= InnerTreenumerator.Position.Depth)
          _SkippedStack.RemoveLast();

        var effectiveDepth = InnerTreenumerator.Position.Depth - _SkippedStack.Count;

        if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        {
          var skipped = _Predicate(InnerTreenumerator.ToNodeContext());

          if (skipped)
          {
            _SkippedStack.AddLast(InnerTreenumerator.ToNodeVisit());

            nodeTraversalStrategies = _TraversalStrategy;

            continue;
          }
          else
          {
            var effectivePosition = GetEffectivePosition();

            var lastScheduleNodeVisitWasSkipped = _NodePositionAndVisitCounts.GetLast().Skipped;

            if (lastScheduleNodeVisitWasSkipped)
              _NodePositionAndVisitCounts.RemoveLast();

            _NodePositionAndVisitCounts.AddLast(new NodeTraversalStatus(effectivePosition, 0));
          }
        }
        else
        {
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

      _EnumerationFinished = true;

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
        // TODO: I am not sure this block is 100% correct. I would like to add
        // more tests to try and uncover more edge cases here.
        var previousNodePosition = _NodePositionAndVisitCounts.GetLast().Position;

        effectiveSiblingIndex =
          previousNodePosition.Depth == effectiveDepth
          ? previousNodePosition.SiblingIndex + 1
          : 0;
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

      if (!_EnumerationFinished)
      {
        ref var nodePositionAndVisitCount = ref GetNodeTraversalStatusToUpdateState();

        if (nodePositionAndVisitCount.Position.Depth == 0
          && InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        {
          _SeenRootNodesCount++;
        }

        Node = InnerTreenumerator.Node;
        VisitCount = nodePositionAndVisitCount.VisitCount;
        Position = nodePositionAndVisitCount.Position;
      }
    }

    private struct NodeTraversalStatus
    {
      public NodeTraversalStatus(
        NodePosition position,
        int visitCount,
        NodeTraversalStrategies nodeTraversalStrategies = NodeTraversalStrategies.TraverseAll)
      {
        Position = position;
        VisitCount = visitCount;
        TraversalStrategy = nodeTraversalStrategies;
      }

      public NodePosition Position { get; set; }
      public int VisitCount { get; set; }
      public NodeTraversalStrategies TraversalStrategy { get; set; }
      public bool Skipped => TraversalStrategy != NodeTraversalStrategies.TraverseAll;

      public override string ToString() => $"({Position}), {VisitCount}, {TraversalStrategy}";
    }
  }
}
