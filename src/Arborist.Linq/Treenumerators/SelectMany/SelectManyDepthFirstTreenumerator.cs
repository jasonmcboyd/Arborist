using Arborist.Common;
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
      {
        return false;
      }

      if (Position.Depth == -1)
        return OnStarting();

      if (_HasCachedVisit)
      {
        _HasCachedVisit = false;

        UpdateStateFromNodeVisit(_Stack.Peek());

        return true;
      }

      if (MoveNextNodeTreenumerator(nodeTraversalStrategy))
        return true;

      if (MoveNextInnerSubtree())
        return true;

      return false;
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

    private bool MoveNextInnerSubtree()
    {
      var previousDepth = InnerTreenumerator.Position.Depth;

      if (!MoveNextInnerTreenumerator())
        return false;

      var newDepth = InnerTreenumerator.Position.Depth;

      if (newDepth > previousDepth)
      {
        _Stack.Push(
          new NodeVisit<TResult>(
            _NodeTreenumerator.Mode,
            _NodeTreenumerator.Node,
            _NodeTreenumerator.VisitCount,
            new NodePosition(_Stack.Peek().VisitCount - 1, InnerTreenumerator.Position.Depth)));
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
            new NodePosition(_Stack.Peek().VisitCount - 1, InnerTreenumerator.Position.Depth)));
      }

      UpdateStateFromNodeVisit(_Stack.Peek());

      return true;
    }

    private bool MoveNextNodeTreenumerator(NodeTraversalStrategy nodeTraversalStrategy)
    {
      if (_NodeTreenumerator is null)
      {
        return false;
      }

      var previousPosition = _NodeTreenumerator.Position;

      if (_NodeTreenumerator.MoveNext(nodeTraversalStrategy))
      {
        var newPosition = _NodeTreenumerator.Position;

        if (newPosition.Depth < previousPosition.Depth)
        {
          _Stack.Pop();
          _Stack.Push(_Stack.Pop().IncrementVisitCount());

          UpdateStateFromNodeVisit(_Stack.Peek());

          return true;
        }
        else if (newPosition.Depth > previousPosition.Depth)
        {
          _Stack.Push(
            new NodeVisit<TResult>(
              _NodeTreenumerator.Mode,
              _NodeTreenumerator.Node,
              _NodeTreenumerator.VisitCount,
              new NodePosition(_Stack.Peek().VisitCount - 1, _Stack.Peek().Position.Depth + 1)));

          UpdateStateFromNodeVisit(_Stack.Peek());

          return true;
        }
        else
        {
          if (previousPosition == newPosition)
          {
            var visit = _Stack.Pop();

            _Stack.Push(
              new NodeVisit<TResult>(
                _NodeTreenumerator.Mode,
                _NodeTreenumerator.Node,
                visit.VisitCount + 1,
                visit.Position));

            UpdateStateFromNodeVisit(_Stack.Peek());

            return true;
          }

          if (previousPosition.Depth == 0
            && _NodeTreenumerator.Position.Depth == 0
            && newPosition.SiblingIndex > previousPosition.SiblingIndex)
          {
            _Stack.Pop();
            _Stack.Push(_Stack.Pop().IncrementVisitCount());

            if (InnerTreenumerator.Position.Depth == 0)
            {
              _Stack.Push(
                new NodeVisit<TResult>(
                  _NodeTreenumerator.Mode,
                  _NodeTreenumerator.Node,
                  _NodeTreenumerator.VisitCount,
                  new NodePosition(_Stack.Peek().VisitCount - 1, InnerTreenumerator.Position.Depth)));

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
                  new NodePosition(_Stack.Peek().VisitCount - 1, InnerTreenumerator.Position.Depth)));

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
      Position = visit.Position;
    }
  }
}
