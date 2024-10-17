using Arborist.Core;
using Arborist.Linq.Extensions;
using Nito.Collections;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class RootfixScanBreadthFirstTreenumerator<TNode, TAccumulate>
    : TreenumeratorWrapper<TNode, TAccumulate>
  {
    public RootfixScanBreadthFirstTreenumerator(
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

      _CurrentLevel.AddToBack(seedVisit);
    }

    private readonly Func<NodeContext<TAccumulate>, NodeContext<TNode>, TAccumulate> _Accumulator;

    private Deque<NodeVisit<TAccumulate>> _CurrentLevel = new Deque<NodeVisit<TAccumulate>>();
    private Deque<NodeVisit<TAccumulate>> _NextLevel = new Deque<NodeVisit<TAccumulate>>();

    private Stack<NodeVisit<TAccumulate>> _SkippedStack = new Stack<NodeVisit<TAccumulate>>();

    protected override bool OnMoveNext(NodeTraversalStrategy nodeTraversalStrategy)
    {
      if (Mode == TreenumeratorMode.SchedulingNode)
      {
        if (nodeTraversalStrategy == NodeTraversalStrategy.SkipNode)
          _SkippedStack.Push(_NextLevel.RemoveFromBack());
        else if (nodeTraversalStrategy == NodeTraversalStrategy.SkipSubtree)
          _NextLevel.RemoveFromBack();
      }

      if (!InnerTreenumerator.MoveNext(nodeTraversalStrategy))
        return false;

      if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        OnSchedulingNode();
      else
        OnVisitingNode();

      return true;
    }

    private void OnSchedulingNode()
    {
      while (_SkippedStack.Count > 0
        && InnerTreenumerator.Position.Depth <= _SkippedStack.Peek().Position.Depth)
      {
        _SkippedStack.Pop();
      }

      var accumulateNodeVisit = _SkippedStack.Count > 0
        ? _SkippedStack.Peek()
        : _CurrentLevel[0];

      var node = _Accumulator(accumulateNodeVisit.ToNodeContext(), InnerTreenumerator.ToNodeContext());

      var visit = InnerTreenumerator.ToNodeVisit().WithNode(node);

      _NextLevel.AddToBack(visit);

      UpdateStateFromNodeVisit(visit);
    }

    private void OnVisitingNode()
    {
      if (InnerTreenumerator.VisitCount == 1)
      {
        _SkippedStack.Clear();
        _CurrentLevel.RemoveFromFront();

        if (_CurrentLevel.Count == 0)
          (_CurrentLevel, _NextLevel) = (_NextLevel, _CurrentLevel);
      }

      var visit = _CurrentLevel[0];

      var newVisit =
        new NodeVisit<TAccumulate>(
          InnerTreenumerator.Mode,
          visit.Node,
          InnerTreenumerator.VisitCount,
          InnerTreenumerator.Position);

      _CurrentLevel[0] = newVisit;

      UpdateStateFromNodeVisit(newVisit);
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
