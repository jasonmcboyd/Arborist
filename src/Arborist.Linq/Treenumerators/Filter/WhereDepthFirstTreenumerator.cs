using Arborist.Core;
using Arborist.Linq.Extensions;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class WhereDepthFirstTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public WhereDepthFirstTreenumerator(
      ITreenumerator<TNode> innerTreenumerator,
      Func<NodeVisit<TNode>, bool> predicate)
      : base(innerTreenumerator)
    {
      _Predicate = predicate;

      _NodeVisits.Push(InnerTreenumerator.ToNodeVisit().IncrementVisitCount());
    }

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;

    private Stack<NodeVisit<TNode>> _NodeVisits = new Stack<NodeVisit<TNode>>();
    private Stack<NodeVisit<TNode>> _SkippedNodeVisits = new Stack<NodeVisit<TNode>>();

    private bool _EnumerationFinished = false;

    protected override bool OnMoveNext(TraversalStrategy traversalStrategy)
    {
      if (_EnumerationFinished)
        return false;

      if (Mode == TreenumeratorMode.VisitingNode)
        traversalStrategy = TraversalStrategy.TraverseSubtree;

      return InnerTreenumeratorMoveNext(traversalStrategy);
    }

    private bool InnerTreenumeratorMoveNext(TraversalStrategy traversalStrategy)
    {
      var previousNodeVisit = InnerTreenumerator.ToNodeVisit();

      while (InnerTreenumerator.MoveNext(traversalStrategy))
      {
        var currentNodeVisit = InnerTreenumerator.ToNodeVisit();

        if (currentNodeVisit.Mode == TreenumeratorMode.SchedulingNode)
        {
          if (_Predicate(currentNodeVisit))
          {
            _NodeVisits.Push(currentNodeVisit);

            if (_SkippedNodeVisits.Count > 0
              && _SkippedNodeVisits.Peek().OriginalPosition.Depth == currentNodeVisit.OriginalPosition.Depth)
            {
              _SkippedNodeVisits.Pop();
              _NodeVisits.Push(_NodeVisits.Pop().IncrementVisitCount());
            }

            UpdateState();

            return true;
          }

          _SkippedNodeVisits.Push(currentNodeVisit);
        }
        else
        {
          var isSkippedNode =
            _SkippedNodeVisits.Count > 0
            && _SkippedNodeVisits.Peek().OriginalPosition == currentNodeVisit.OriginalPosition;

          if (isSkippedNode)
          {
            if (currentNodeVisit.VisitCount == 1)
            {
              previousNodeVisit = currentNodeVisit;
              continue;
            }

            _SkippedNodeVisits.Pop();

            previousNodeVisit = currentNodeVisit;
            continue;
          }

          if (currentNodeVisit.OriginalPosition.Depth < previousNodeVisit.OriginalPosition.Depth)
          {
            if (_SkippedNodeVisits.Count > 0)
            {
              if (_SkippedNodeVisits.Peek().OriginalPosition.Depth > _NodeVisits.Peek().OriginalPosition.Depth)
              {
                _SkippedNodeVisits.Pop();

                continue;
              }
              else
              {
                _NodeVisits.Pop();
              }
            }

            _NodeVisits.Pop();
          }
          else if (currentNodeVisit.OriginalPosition.Depth > previousNodeVisit.OriginalPosition.Depth)
          {
            _NodeVisits.Push(currentNodeVisit);
          }
          else
          {
            if (currentNodeVisit.VisitCount == 1)
            {
              _NodeVisits.Push(_NodeVisits.Pop().IncrementVisitCount());
            }
            else
            {
              previousNodeVisit = currentNodeVisit;
              continue;
            }
          }

          UpdateState();

          return true;
        }
      }

      UpdateState();

      _EnumerationFinished = true;

      return false;
    }

    private void PopDeepestNode()
    {
      if (_SkippedNodeVisits.Count > 0)
      {
        if (_SkippedNodeVisits.Peek().OriginalPosition.Depth > _NodeVisits.Peek().OriginalPosition.Depth)
          _SkippedNodeVisits.Pop();
        else
          _NodeVisits.Pop();

        return;
      }

      _NodeVisits.Pop();
    }

    private void UpdateState()
    {
      Mode = InnerTreenumerator.Mode;

      var depthDelta = -1 * _SkippedNodeVisits.Count;

      var visit = _NodeVisits.Pop();
      var siblingIndex = _NodeVisits.Peek().VisitCount - 1;
      _NodeVisits.Push(visit);

      if (!_EnumerationFinished)
      {
        Node = InnerTreenumerator.Node;
        VisitCount = InnerTreenumerator.VisitCount;
        OriginalPosition = (siblingIndex, InnerTreenumerator.OriginalPosition.Depth + depthDelta);
        //OriginalPosition = InnerTreenumerator.OriginalPosition + originalPositionDelta;
      }
    }
  }
}
