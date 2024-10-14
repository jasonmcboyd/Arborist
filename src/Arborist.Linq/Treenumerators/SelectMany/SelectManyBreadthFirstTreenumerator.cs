using Arborist.Common;
using Arborist.Core;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class SelectManyBreadthFirstTreenumerator<TSource, TResult>
    : TreenumeratorWrapper<TSource, TResult>
  {
    public SelectManyBreadthFirstTreenumerator(
      Func<ITreenumerator<TSource>> sourceTreenumeratorFactory,
      Func<TSource, ITreenumerable<TResult>> selector)
      : base(sourceTreenumeratorFactory)
    {
      _Selector = selector;

      _Stack.Push(new NodeVisit<TResult>(TreenumeratorMode.VisitingNode, default, 1, new NodePosition(0, -1)));
    }

    public readonly Func<TSource, ITreenumerable<TResult>> _Selector;

    private readonly Stack<NodeVisit<TResult>> _Stack = new Stack<NodeVisit<TResult>>();

    private ITreenumerator<TResult> _NodeTreenumerator;

    private bool _HasCachedVisit = false;

    protected override bool OnMoveNext(NodeTraversalStrategy nodeTraversalStrategy)
    {
      if (EnumerationFinished)
        return false;

      if (Position.Depth == -1)
        return OnStarting();

      if (_HasCachedVisit)
      {
        _HasCachedVisit = false;

        UpdateStateFromNodeVisit(_Stack.Peek());

        return true;
      }

      throw new NotImplementedException();

      //if (MoveNextNodeTreenumerator(nodeTraversalStrategy))
      //  return true;

      //if (MoveNextInnerSubtree())
      //  return true;

      //Mode = TreenumeratorMode.EnumerationFinished;

      //return false;
    }

    private bool OnStarting()
    {
      if (!MoveNextInnerTreenumerator())
        return false;

      _Stack.Push(
        new NodeVisit<TResult>(
          _NodeTreenumerator.Mode,
          _NodeTreenumerator.Node,
          _NodeTreenumerator.VisitCount,
          new NodePosition(_Stack.Peek().VisitCount - 1, InnerTreenumerator.Position.Depth)));

      UpdateStateFromNodeVisit(_Stack.Peek());

      return true;
    }

    private bool MoveNextInnerTreenumerator()
    {
      var previousDepth = _Stack.Peek().Position.Depth;

      while (true)
      {
        if (InnerTreenumerator.MoveNext(NodeTraversalStrategy.TraverseSubtree))
        {
          // Ignore the scheduling node visit.
          if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
            continue;

          var newDepth = InnerTreenumerator.Position.Depth;

          if (newDepth < previousDepth)
            return true;

          _NodeTreenumerator = _Selector(InnerTreenumerator.Node).GetDepthFirstTreenumerator();

          if (_NodeTreenumerator.MoveNext(NodeTraversalStrategy.TraverseSubtree))
            return true;

          _NodeTreenumerator.Dispose();
          _NodeTreenumerator = null;
        }

        return false;
      }
    }

    private void UpdateStateFromNodeVisit(NodeVisit<TResult> visit)
    {
      Mode = visit.Mode;
      Node = visit.Node;
      VisitCount = visit.VisitCount;
      Position = visit.Position;
    }
  }
}
