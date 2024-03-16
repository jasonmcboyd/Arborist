using Arborist.Core;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class SelectManyBreadthFirstTreenumerator<TSource, TResult>
    : TreenumeratorWrapper<TSource, TResult>
  {
    public SelectManyBreadthFirstTreenumerator(
      ITreenumerator<TSource> source,
      Func<TSource, ITreenumerable<TResult>> selector)
      : base(source)
    {
      _Selector = selector;

      _Stack.Push(new NodeVisit<TResult>(TreenumeratorMode.VisitingNode, default, 1, (0, -1), (0, -1), SchedulingStrategy.TraverseSubtree));
    }

    public readonly Func<TSource, ITreenumerable<TResult>> _Selector;

    private Stack<NodeVisit<TResult>> _Stack = new Stack<NodeVisit<TResult>>();

    private ITreenumerator<TResult> _NodeTreenumerator;

    private bool _HasCachedVisit = false;

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (Mode == TreenumeratorMode.EnumerationFinished)
        return false;

      if (Mode == TreenumeratorMode.EnumerationNotStarted)
        return OnStarting();

      if (_HasCachedVisit)
      {
        _HasCachedVisit = false;

        UpdateStateFromNodeVisit(_Stack.Peek());

        return true;
      }

      throw new NotImplementedException();

      //if (MoveNextNodeTreenumerator(schedulingStrategy))
      //  return true;

      //if (MoveNextInnerSubtree())
      //  return true;

      //Mode = TreenumeratorMode.EnumerationFinished;

      //return false;
    }

    private bool OnStarting()
    {
      if (!MoveNextInnerTreenumerator())
      {
        Mode = TreenumeratorMode.EnumerationFinished;
        return false;
      }

      _Stack.Push(
        new NodeVisit<TResult>(
          _NodeTreenumerator.Mode,
          _NodeTreenumerator.Node,
          _NodeTreenumerator.VisitCount,
          (_Stack.Peek().VisitCount - 1, InnerTreenumerator.OriginalPosition.Depth),
          (_Stack.Peek().VisitCount - 1, InnerTreenumerator.Position.Depth),
          SchedulingStrategy.TraverseSubtree));

      UpdateStateFromNodeVisit(_Stack.Peek());

      return true;
    }

    private bool MoveNextInnerTreenumerator()
    {
      var previousDepth = _Stack.Peek().OriginalPosition.Depth;

      while (true)
      {
        if (InnerTreenumerator.MoveNext(SchedulingStrategy.TraverseSubtree))
        {
          // Ignore the scheduling step.
          if (InnerTreenumerator.VisitCount == 0)
            continue;

          var newDepth = InnerTreenumerator.OriginalPosition.Depth;

          if (newDepth < previousDepth)
            return true;

          _NodeTreenumerator = _Selector(InnerTreenumerator.Node).GetDepthFirstTreenumerator();

          if (_NodeTreenumerator.MoveNext(SchedulingStrategy.TraverseSubtree))
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
      OriginalPosition = visit.OriginalPosition;
      Position = visit.Position;
      SchedulingStrategy = visit.SchedulingStrategy;
    }
  }
}
