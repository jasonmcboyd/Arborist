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

      _Stack.Push(new NodeVisit<TResult>(TreenumeratorMode.VisitingNode, default, 1, (0, -1)));
    }

    public readonly Func<TSource, ITreenumerable<TResult>> _Selector;

    private Stack<NodeVisit<TResult>> _Stack = new Stack<NodeVisit<TResult>>();

    private ITreenumerator<TResult> _NodeTreenumerator;

    private bool _HasCachedVisit = false;

    protected override bool OnMoveNext(TraversalStrategy traversalStrategy)
    {
      if (EnumerationFinished)
        return false;

      if (OriginalPosition.Depth == -1)
        return OnStarting();

      if (_HasCachedVisit)
      {
        _HasCachedVisit = false;

        UpdateStateFromNodeVisit(_Stack.Peek());

        return true;
      }

      if (MoveNextNodeTreenumerator(traversalStrategy))
        return true;

      if (MoveNextInnerSubtree())
        return true;

      EnumerationFinished = true;

      return false;
    }

    private bool OnStarting()
    {
      if (!MoveNextInnerTreenumerator())
      {
        EnumerationFinished = true;
        return false;
      }

      _Stack.Push(
        new NodeVisit<TResult>(
          _NodeTreenumerator.Mode,
          _NodeTreenumerator.Node,
          _NodeTreenumerator.VisitCount,
          (_Stack.Peek().VisitCount - 1, InnerTreenumerator.OriginalPosition.Depth)));

      UpdateStateFromNodeVisit(_Stack.Peek());

      return true;
    }

    private bool MoveNextInnerTreenumerator()
    {
      var previousDepth = _Stack.Peek().OriginalPosition.Depth;

      while (true)
      {
        if (InnerTreenumerator.MoveNext(TraversalStrategy.TraverseSubtree))
        {
          // Ignore the scheduling node visit.
          if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
            continue;

          var newDepth = InnerTreenumerator.OriginalPosition.Depth;

          if (newDepth < previousDepth)
            return true;

          _NodeTreenumerator = _Selector(InnerTreenumerator.Node).GetDepthFirstTreenumerator();

          if (_NodeTreenumerator.MoveNext(TraversalStrategy.TraverseSubtree))
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
            (_Stack.Peek().VisitCount - 1, InnerTreenumerator.OriginalPosition.Depth)));
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
            (_Stack.Peek().VisitCount - 1, InnerTreenumerator.OriginalPosition.Depth)));
      }

      UpdateStateFromNodeVisit(_Stack.Peek());

      return true;
    }

    private bool MoveNextNodeTreenumerator(TraversalStrategy traversalStrategy)
    {
      if (_NodeTreenumerator == null)
        return false;

      var previousOriginalPosition = _NodeTreenumerator.OriginalPosition;

      if (_NodeTreenumerator.MoveNext(traversalStrategy))
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
              (_Stack.Peek().VisitCount - 1, _Stack.Peek().OriginalPosition.Depth + 1)));

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
                visit.OriginalPosition));

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
                  (_Stack.Peek().VisitCount - 1, InnerTreenumerator.OriginalPosition.Depth)));

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
                  (_Stack.Peek().VisitCount - 1, InnerTreenumerator.OriginalPosition.Depth)));

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
    }
  }
}
