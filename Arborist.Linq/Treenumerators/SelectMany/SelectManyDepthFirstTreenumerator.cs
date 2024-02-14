﻿using System;
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

    private readonly Stack<NodeVisit<TNode>> _Stack = new Stack<NodeVisit<TNode>>();

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      throw new NotImplementedException();
      //while (InnerTreenumerator.MoveNext(schedulingStrategy))
      //{
      //  using (var currentEnumerator = InnerTreenumerator.Node.Node.GetDepthFirstTreenumerator())
      //  {
      //    NodeVisit<TNode>? previousVisit = null;

      //    while (currentEnumerator.MoveNext(SchedulingStrategy.ScheduleForTraversal))
      //    {
      //      if (previousVisit != null)
      //      {
      //        Current = previousVisit.Value;
      //        previousVisit = currentEnumerator.Current.WithNode(_Selector(currentEnumerator.Current));
      //        return true;
      //      }
      //    }

      //    if (previousVisit != null)
      //      _Stack.Push(previousVisit.Value);
      //  }
      //}

      //while (_Stack.Count > 0)
      //{
      //  Current = _Stack.Pop();
      //  return true;
      //}

      //return false;
    }
  }
}
