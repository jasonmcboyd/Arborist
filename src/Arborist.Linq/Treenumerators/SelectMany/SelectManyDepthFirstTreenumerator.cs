using Arborist.Core;
using Arborist.Linq.Extensions;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class SelectManyDepthFirstTreenumerator<TSource, TResult>
    : TreenumeratorWrapper<TSource, TResult>
  {
    public SelectManyDepthFirstTreenumerator(
      ITreenumerator<TSource> source,
      Func<TSource, ITreenumerable<TResult>> selector)
      : base(source)
    {
      _Selector = selector;

      _Stack.Push(new NodeVisit<TResult>(TreenumeratorMode.VisitingNode, default, 1, (0, -1), (0, -1)));
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

      if (MoveNextNodeTreenumerator(schedulingStrategy))
        return true;

      if (MoveNextInnerSubtree())
        return true;

      Mode = TreenumeratorMode.EnumerationFinished;

      return false;
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
          (_Stack.Peek().VisitCount - 1, InnerTreenumerator.Position.Depth)));

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

    private bool MoveNextInnerSubtree()
    {
      var previousDepth = InnerTreenumerator.OriginalPosition.Depth;

      if (!MoveNextInnerTreenumerator())
        return false;

      var newDepth = InnerTreenumerator.OriginalPosition.Depth;

      if (newDepth > previousDepth)
      {
        _Stack.Push(
          new NodeVisit<TResult>(
            _NodeTreenumerator.Mode,
            _NodeTreenumerator.Node,
            _NodeTreenumerator.VisitCount,
            (_Stack.Peek().VisitCount - 1, InnerTreenumerator.OriginalPosition.Depth),
            (_Stack.Peek().VisitCount - 1, InnerTreenumerator.Position.Depth)));
      }
      else if (newDepth < previousDepth)
      {
        _Stack.Pop();
        _Stack.Push(_Stack.Pop().IncrementVisitCount());

        UpdateStateFromNodeVisit(_Stack.Peek());

        return true;
      }
      else
      {
        _Stack.Pop();
        _Stack.Push(_Stack.Pop().IncrementVisitCount());

        _Stack.Push(
          new NodeVisit<TResult>(
            _NodeTreenumerator.Mode,
            _NodeTreenumerator.Node,
            _NodeTreenumerator.VisitCount,
            (_Stack.Peek().VisitCount - 1, InnerTreenumerator.OriginalPosition.Depth),
            (_Stack.Peek().VisitCount - 1, InnerTreenumerator.Position.Depth)));
      }

      UpdateStateFromNodeVisit(_Stack.Peek());

      return true;
    }

    private bool MoveNextNodeTreenumerator(SchedulingStrategy schedulingStrategy)
    {
      if (_NodeTreenumerator == null)
        return false;

      var previousOriginalPosition = _NodeTreenumerator.OriginalPosition;

      if (_NodeTreenumerator.MoveNext(schedulingStrategy))
      {
        var newOriginalPosition = _NodeTreenumerator.OriginalPosition;

        if (newOriginalPosition.Depth < previousOriginalPosition.Depth)
        {
          _Stack.Pop();
          _Stack.Push(_Stack.Pop().IncrementVisitCount());

          UpdateStateFromNodeVisit(_Stack.Peek());

          return true;
        }
        else if (newOriginalPosition.Depth > previousOriginalPosition.Depth)
        {
          _Stack.Push(
            new NodeVisit<TResult>(
              _NodeTreenumerator.Mode,
              _NodeTreenumerator.Node,
              _NodeTreenumerator.VisitCount,
              (_Stack.Peek().VisitCount - 1, _Stack.Peek().OriginalPosition.Depth + 1),
              (_Stack.Peek().VisitCount - 1, _Stack.Peek().Position.Depth + 1)));

          UpdateStateFromNodeVisit(_Stack.Peek());

          return true;
        }
        else
        {
          if (previousOriginalPosition == newOriginalPosition)
          {
            var visit = _Stack.Pop();

            _Stack.Push(
              new NodeVisit<TResult>(
                _NodeTreenumerator.Mode,
                _NodeTreenumerator.Node,
                visit.VisitCount + 1,
                visit.OriginalPosition,
                visit.Position));

            UpdateStateFromNodeVisit(_Stack.Peek());

            return true;
          }

          if (previousOriginalPosition.Depth == 0
            && _NodeTreenumerator.OriginalPosition.Depth == 0
            && newOriginalPosition.SiblingIndex > previousOriginalPosition.SiblingIndex)
          {
            _Stack.Pop();
            _Stack.Push(_Stack.Pop().IncrementVisitCount());

            if (InnerTreenumerator.OriginalPosition.Depth == 0)
            {
              _Stack.Push(
                new NodeVisit<TResult>(
                  _NodeTreenumerator.Mode,
                  _NodeTreenumerator.Node,
                  _NodeTreenumerator.VisitCount,
                  (_Stack.Peek().VisitCount - 1, InnerTreenumerator.OriginalPosition.Depth),
                  (_Stack.Peek().VisitCount - 1, InnerTreenumerator.Position.Depth)));

              UpdateStateFromNodeVisit(_Stack.Peek());
            }
            else
            {
              UpdateStateFromNodeVisit(_Stack.Peek());

              _Stack.Push(
                new NodeVisit<TResult>(
                  _NodeTreenumerator.Mode,
                  _NodeTreenumerator.Node,
                  _NodeTreenumerator.VisitCount,
                  (_Stack.Peek().VisitCount - 1, InnerTreenumerator.OriginalPosition.Depth),
                  (_Stack.Peek().VisitCount - 1, InnerTreenumerator.Position.Depth)));

              _HasCachedVisit = true;
            }

            return true;
          }
        }
      }

      _NodeTreenumerator = null;

      return false;
    }

    private void UpdateStateFromNodeVisit(NodeVisit<TResult> visit)
    {
      Mode = visit.Mode;
      Node = visit.Node;
      VisitCount = visit.VisitCount;
      OriginalPosition = visit.OriginalPosition;
      Position = visit.Position;
    }
  }
}
