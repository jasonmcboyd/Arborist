using Arborist.Core;
using Arborist.Linq.Extensions;
using Nito.Collections;
using System;
using System.Linq;

namespace Arborist.Linq.Treenumerators
{
  internal class RootfixScanBreadthFirstTreenumerator<TNode, TAccumulate>
    : TreenumeratorWrapper<TNode, TAccumulate>
  {
    public RootfixScanBreadthFirstTreenumerator(
      ITreenumerator<TNode> innerTreenumerator,
      Func<NodeVisit<TAccumulate>, NodeVisit<TNode>, TAccumulate> accumulator,
      TAccumulate seed) : base(innerTreenumerator)
    {
      _Accumulator = accumulator;

      var seedVisit =
        new NodeVisit<TAccumulate>(
          TreenumeratorState.VisitingNode,
          seed,
          1,
          (0, -1),
          (0, -1),
          SchedulingStrategy.TraverseSubtree);

      _Queue.AddToBack(seedVisit);
    }

    private readonly Func<NodeVisit<TAccumulate>, NodeVisit<TNode>, TAccumulate> _Accumulator;

    private Deque<NodeVisit<TAccumulate>> _Queue = new Deque<NodeVisit<TAccumulate>>();

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (State == TreenumeratorState.SchedulingNode)
      {
        if (schedulingStrategy == SchedulingStrategy.SkipNode
          || schedulingStrategy == SchedulingStrategy.SkipSubtree)
          _Queue.RemoveFromBack();
      }

      if (!InnerTreenumerator.MoveNext(schedulingStrategy))
        return false;

      if (InnerTreenumerator.State == TreenumeratorState.SchedulingNode)
      {
        OnSchedulingNode();

        return true;
      }

      if (InnerTreenumerator.VisitCount == 1)
        _Queue.RemoveFromFront();

      var visit = _Queue[0];

      var newVisit =
        new NodeVisit<TAccumulate>(
          InnerTreenumerator.State,
          visit.Node,
          InnerTreenumerator.VisitCount,
          InnerTreenumerator.OriginalPosition,
          InnerTreenumerator.Position,
          InnerTreenumerator.SchedulingStrategy);

      _Queue[0] = newVisit;

      UpdateStateFromNodeVisit(newVisit);

      return true;
    }

    private void OnSchedulingNode()
    {
      var visit = InnerTreenumerator.ToNodeVisit();

      var node = _Accumulator(_Queue[0], visit);

      _Queue.AddToBack(visit.WithNode(node));

      UpdateStateFromNodeVisit(_Queue.Last());
    }

    private void UpdateStateFromNodeVisit(NodeVisit<TAccumulate> nodeVisit)
    {
      State = nodeVisit.TreenumeratorState;
      Node = nodeVisit.Node;
      VisitCount = nodeVisit.VisitCount;
      OriginalPosition = nodeVisit.OriginalPosition;
      Position = nodeVisit.Position;
      SchedulingStrategy = nodeVisit.SchedulingStrategy;
    }
  }
}
