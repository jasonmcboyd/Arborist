using Arborist.Core;
using Arborist.Linq.Extensions;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class RootfixScanDepthFirstTreenumerator<TNode, TAccumulate>
    : TreenumeratorWrapper<TNode, TAccumulate>
  {
    public RootfixScanDepthFirstTreenumerator(
      ITreenumerator<TNode> InnerTreenumerator,
      Func<NodeVisit<TAccumulate>, NodeVisit<TNode>, TAccumulate> accumulator,
      TAccumulate seed) : base(InnerTreenumerator)
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

      _Stack.Push(seedVisit);
    }

    private readonly Func<NodeVisit<TAccumulate>, NodeVisit<TNode>, TAccumulate> _Accumulator;

    private readonly Stack<NodeVisit<TAccumulate>> _Stack = new Stack<NodeVisit<TAccumulate>>();

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (!InnerTreenumerator.MoveNext(schedulingStrategy))
        return false;

      var previousDepth = _Stack.Peek().OriginalPosition.Depth;
      var currentDepth = InnerTreenumerator.OriginalPosition.Depth;

      var schedulingNewRootNode =
        currentDepth == 0
        && InnerTreenumerator.VisitCount == 0
        && InnerTreenumerator.OriginalPosition.SiblingIndex > 0;

      if (currentDepth < previousDepth || schedulingNewRootNode)
        _Stack.Pop();

      var node =
        currentDepth > previousDepth || schedulingNewRootNode
        ? _Accumulator(_Stack.Peek(), InnerTreenumerator.ToNodeVisit())
        : _Stack.Pop().Node;

      var newVisit =
        new NodeVisit<TAccumulate>(
          InnerTreenumerator.State,
          node,
          InnerTreenumerator.VisitCount,
          InnerTreenumerator.OriginalPosition,
          InnerTreenumerator.Position,
          InnerTreenumerator.SchedulingStrategy);

      _Stack.Push(newVisit);

      UpdateStateFromNodeVisit(newVisit);

      return true;
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
