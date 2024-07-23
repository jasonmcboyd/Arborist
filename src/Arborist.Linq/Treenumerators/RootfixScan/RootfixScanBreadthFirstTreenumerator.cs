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
      Func<NodeContext<TAccumulate>, NodeContext<TNode>, TAccumulate> accumulator,
      TAccumulate seed) : base(innerTreenumerator)
    {
      _Accumulator = accumulator;

      var seedVisit =
        new NodeVisit<TAccumulate>(
          TreenumeratorMode.VisitingNode,
          seed,
          1,
          (0, -1));

      _Queue.AddToBack(seedVisit);
    }

    private readonly Func<NodeContext<TAccumulate>, NodeContext<TNode>, TAccumulate> _Accumulator;

    private readonly Deque<NodeVisit<TAccumulate>> _Queue = new Deque<NodeVisit<TAccumulate>>();

    protected override bool OnMoveNext(NodeTraversalStrategy nodeTraversalStrategy)
    {
      if (Mode == TreenumeratorMode.SchedulingNode)
      {
        if (nodeTraversalStrategy == NodeTraversalStrategy.SkipNode
          || nodeTraversalStrategy == NodeTraversalStrategy.SkipSubtree)
          _Queue.RemoveFromBack();
      }

      if (!InnerTreenumerator.MoveNext(nodeTraversalStrategy))
        return false;

      if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
      {
        OnSchedulingNode();

        return true;
      }

      if (InnerTreenumerator.VisitCount == 1)
        _Queue.RemoveFromFront();

      var visit = _Queue[0];

      var newVisit =
        new NodeVisit<TAccumulate>(
          InnerTreenumerator.Mode,
          visit.Node,
          InnerTreenumerator.VisitCount,
          InnerTreenumerator.Position);

      _Queue[0] = newVisit;

      UpdateStateFromNodeVisit(newVisit);

      return true;
    }

    private void OnSchedulingNode()
    {
      var visit = InnerTreenumerator.ToNodeVisit();

      var node = _Accumulator(_Queue[0].ToNodeContext(), visit.ToNodeContext());

      _Queue.AddToBack(visit.WithNode(node));

      UpdateStateFromNodeVisit(_Queue.Last());
    }

    private void UpdateStateFromNodeVisit(NodeVisit<TAccumulate> nodeVisit)
    {
      Mode = nodeVisit.Mode;
      Node = nodeVisit.Node;
      VisitCount = nodeVisit.VisitCount;
      Position = nodeVisit.Position;
    }
  }
}
