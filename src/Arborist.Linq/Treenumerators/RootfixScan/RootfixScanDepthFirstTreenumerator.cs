﻿using Arborist.Core;
using Arborist.Linq.Extensions;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class RootfixScanDepthFirstTreenumerator<TNode, TAccumulate>
    : TreenumeratorWrapper<TNode, TAccumulate>
  {
    public RootfixScanDepthFirstTreenumerator(
      Func<ITreenumerator<TNode>> innerTreenumeratorFactory,
      Func<NodeContext<TAccumulate>, NodeContext<TNode>, TAccumulate> accumulator,
      TAccumulate seed) : base(innerTreenumeratorFactory)
    {
      _Accumulator = accumulator;

      var seedVisit =
        new NodeVisit<TAccumulate>(
          TreenumeratorMode.VisitingNode,
          seed,
          1,
          new NodePosition(0, -1));

      _Stack.Push(seedVisit);
    }

    private readonly Func<NodeContext<TAccumulate>, NodeContext<TNode>, TAccumulate> _Accumulator;

    private readonly Stack<NodeVisit<TAccumulate>> _Stack = new Stack<NodeVisit<TAccumulate>>();
    private readonly Stack<NodeVisit<TAccumulate>> _SkippedStack = new Stack<NodeVisit<TAccumulate>>();

    private Stack<NodeVisit<TAccumulate>> GetStackWithDeepestNodeVisit()
    {
      if (_SkippedStack.Count > 0
        && _SkippedStack.Peek().Position.Depth > _Stack.Peek().Position.Depth)
      {
        return _SkippedStack;
      }

      return _Stack;
    }

    private int GetDeepestSeenDepth() => GetStackWithDeepestNodeVisit().Peek().Position.Depth;

    private NodeVisit<TAccumulate> PopStackWithDeepestNodeVisit() => GetStackWithDeepestNodeVisit().Pop();

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode))
      {
        _SkippedStack.Push(_Stack.Pop());
      }

      if (!InnerTreenumerator.MoveNext(nodeTraversalStrategies))
        return false;

      var currentDepth = InnerTreenumerator.Position.Depth;

      if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
      {
        while (GetDeepestSeenDepth() >= currentDepth)
          PopStackWithDeepestNodeVisit();
      }
      else
      {
        while (_Stack.Peek().Position.Depth > currentDepth)
          PopStackWithDeepestNodeVisit();
      }

      var node =
        InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        ? _Accumulator(GetStackWithDeepestNodeVisit().Peek().ToNodeContext(), InnerTreenumerator.ToNodeContext())
        : _Stack.Pop().Node;

      var newVisit =
        new NodeVisit<TAccumulate>(
          InnerTreenumerator.Mode,
          node,
          InnerTreenumerator.VisitCount,
          InnerTreenumerator.Position);

      _Stack.Push(newVisit);

      UpdateStateFromNodeVisit(newVisit);

      return true;
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
