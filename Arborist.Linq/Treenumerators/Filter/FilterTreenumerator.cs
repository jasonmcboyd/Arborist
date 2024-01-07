﻿using System;

namespace Arborist.Linq.Treenumerators
{
  internal class FilterTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public FilterTreenumerator(
      ITreenumerator<TNode> innerTreenumerator,
      Func<NodeVisit<TNode>, bool> predicate,
      ChildStrategy skipStrategy)
      : base(innerTreenumerator)
    {
      _Predicate = predicate;
      _SkipStrategy = skipStrategy;
    }

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;
    private ChildStrategy _SkipStrategy;

    protected override bool OnMoveNext(ChildStrategy childStrategy)
    {
      if (State == TreenumeratorState.SchedulingNode
        && !_Predicate(InnerTreenumerator.Current))
        childStrategy = _SkipStrategy;

      var result = InnerTreenumerator.MoveNext(childStrategy);

      if (!result)
        return false;

      Current = InnerTreenumerator.Current;
      State = InnerTreenumerator.State;
      return true;
    }
  }
}
