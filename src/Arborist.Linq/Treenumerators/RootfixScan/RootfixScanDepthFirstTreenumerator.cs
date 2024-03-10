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

      _SeedVisit =
        new NodeVisit<TAccumulate>(
          TreenumeratorState.VisitingNode,
          seed,
          1,
          (0, -1),
          (0, -1),
          SchedulingStrategy.TraverseSubtree);
    }

    private readonly Func<NodeVisit<TAccumulate>, NodeVisit<TNode>, TAccumulate> _Accumulator;

    private readonly NodeVisit<TAccumulate> _SeedVisit;

    private readonly Stack<NodeVisit<TAccumulate>> _Stack = new Stack<NodeVisit<TAccumulate>>();

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (!InnerTreenumerator.MoveNext(schedulingStrategy))
        return false;

      State = InnerTreenumerator.State;

      if (InnerTreenumerator.OriginalPosition.Depth == 0
        && InnerTreenumerator.State == TreenumeratorState.SchedulingNode)
        return OnRootNode();

      var previousDepth = _Stack.Peek().OriginalPosition.Depth;
      var currentDepth = InnerTreenumerator.OriginalPosition.Depth;

      if (currentDepth < previousDepth)
        _Stack.Pop();

      //if (currentDepth <= previousDepth)
      //{
      //  if (_Stack.Count == 0)
      //    return false;

      //  var visit = _Stack.Pop().IncrementVisitCount();

      //  UpdateStateFromNodeVisit(visit);

      //  _Stack.Push(visit);

      //  return true;
      //}
     
      var node =
        currentDepth <= previousDepth
        ? _Stack.Pop().Node
        : _Accumulator(_Stack.Peek(), InnerTreenumerator.ToNodeVisit());

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

    private bool OnRootNode()
    {
      var node = _Accumulator(_SeedVisit, InnerTreenumerator.ToNodeVisit());

      var visit =
        new NodeVisit<TAccumulate>(
          InnerTreenumerator.State,
          node,
          InnerTreenumerator.VisitCount,
          InnerTreenumerator.OriginalPosition,
          InnerTreenumerator.Position,
          InnerTreenumerator.SchedulingStrategy);

      _Stack.Push(visit);

      UpdateStateFromNodeVisit(visit);

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
