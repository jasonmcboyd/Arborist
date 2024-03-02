using Arborist.Core;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class SelectManyDepthFirstTreenumerator<TInner, TNode>
    : TreenumeratorWrapper<ITreenumerable<TInner>, TNode>
  {
    public SelectManyDepthFirstTreenumerator(
      ITreenumerator<ITreenumerable<TInner>> innerTreenumerator,
      Func<NodeVisit<TInner>, TNode> selector)
      : base(innerTreenumerator)
    {
      _Selector = selector;
    }

    public readonly Func<NodeVisit<TInner>, TNode> _Selector;

    private Stack<ITreenumerator<TInner>> _PriorNodeTreenumerators = new Stack<ITreenumerator<TInner>>();

    private ITreenumerator<TInner> _NodeTreenumerator;

    private NodePosition _NodeTreenumeratorStartingOriginalPosition = (0, 0);
    private NodePosition _NodeTreenumeratorStartingPosition = (0, 0);

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (State == TreenumeratorState.EnumerationFinished)
        return false;

      if (State == TreenumeratorState.EnumerationNotStarted)
        return OnStartingEnumeration();

      if (_NodeTreenumerator.MoveNext(schedulingStrategy))
      {
        UpdateState();

        return true;
      }

      return MoveNextInnerSubtree();
    }

    private bool OnStartingEnumeration()
    {
      return MoveNextInnerSubtree();
    }

    private bool MoveNextInnerSubtree()
    {
      while (true)
      {
        if (_NodeTreenumerator != null)
        {
          _NodeTreenumeratorStartingOriginalPosition = OriginalPosition;
          _NodeTreenumeratorStartingPosition = Position;
        }

        if (InnerTreenumerator.MoveNext(SchedulingStrategy.TraverseSubtree))
        {
          if (InnerTreenumerator.VisitCount != 1)
            continue;

          _NodeTreenumerator = InnerTreenumerator.Node.GetDepthFirstTreenumerator();

          while (true)
          {
            if (_NodeTreenumerator.MoveNext(SchedulingStrategy.TraverseSubtree))
            {
              UpdateState();

              return true;
            }

            _NodeTreenumerator.Dispose();
            _NodeTreenumerator = null;
          }
        }

        return false;
      }
    }

    private void UpdateState()
    {
      State = _NodeTreenumerator.State;

      if (State != TreenumeratorState.EnumerationFinished)
      {
        Node = _Selector(_NodeTreenumerator.ToNodeVisit());
        VisitCount = _NodeTreenumerator.VisitCount;
        OriginalPosition = _NodeTreenumerator.OriginalPosition + _NodeTreenumeratorStartingOriginalPosition;
        Position = _NodeTreenumerator.Position + _NodeTreenumeratorStartingPosition;
        SchedulingStrategy = _NodeTreenumerator.SchedulingStrategy;
      }
    }
  }
}
