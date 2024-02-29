﻿using Arborist.Core;
using System;

namespace Arborist.Linq.Treenumerators
{
  internal class SelectTreenumerator<TInner, TNode> : ITreenumerator<TNode>
  {
    public SelectTreenumerator(
      ITreenumerator<TInner> innerTreenumerator,
      Func<NodeVisit<TInner>, TNode> selector)
    {
      _InnerTreenumerator = innerTreenumerator;
      _Selector = selector;
    }

    private readonly ITreenumerator<TInner> _InnerTreenumerator;
    private readonly Func<NodeVisit<TInner>, TNode> _Selector;

    public SchedulingStrategy SchedulingStrategy => _InnerTreenumerator.SchedulingStrategy;

    public TNode Node { get; private set; } = default;

    public int VisitCount => _InnerTreenumerator.VisitCount;

    public TreenumeratorState State => _InnerTreenumerator.State;

    public NodePosition OriginalPosition => _InnerTreenumerator.OriginalPosition;

    public NodePosition Position => _InnerTreenumerator.Position;

    public bool MoveNext(SchedulingStrategy schedulingStrategy)
    {
      var hasNext = _InnerTreenumerator.MoveNext(schedulingStrategy);

      if (!hasNext)
        return false;

      var visit = _InnerTreenumerator.ToNodeVisit();

      Node = _Selector(visit);

      return true;
    }

    public void Dispose()
    {
      _InnerTreenumerator?.Dispose();
    }
  }
}