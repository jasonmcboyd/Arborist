﻿using Arborist.Core;
using System;

namespace Arborist.Linq.Treenumerables
{
  public class DelegatingTreenumerable<TNode> : ITreenumerable<TNode>
  {
    public DelegatingTreenumerable(
      Func<ITreenumerator<TNode>> breadthFirstTreenumeratorFactory,
      Func<ITreenumerator<TNode>> depthFirstTreenumeratorFactory)
    {
      _BreadthFirstTreenumeratorFactory = breadthFirstTreenumeratorFactory;
      _DepthFirstTreenumeratorFactory = depthFirstTreenumeratorFactory;
    }

    private readonly Func<ITreenumerator<TNode>> _BreadthFirstTreenumeratorFactory;
    private readonly Func<ITreenumerator<TNode>> _DepthFirstTreenumeratorFactory;

    public ITreenumerator<TNode> GetBreadthFirstTreenumerator()
      => _BreadthFirstTreenumeratorFactory();

    public ITreenumerator<TNode> GetDepthFirstTreenumerator()
      => _DepthFirstTreenumeratorFactory();
  }

  public class DelegatingTreenumerable<TInner, TNode> : ITreenumerable<TNode>
  {
    public DelegatingTreenumerable(
      ITreenumerable<TInner> innerTreenumerable,
      Func<ITreenumerator<TInner>, ITreenumerator<TNode>> breadthFirstTreenumeratorFactory,
      Func<ITreenumerator<TInner>, ITreenumerator<TNode>> depthFirstTreenumeratorFactory)
    {
      _InnerTreenumerable = innerTreenumerable;
      _BreadthFirstTreenumeratorFactory = breadthFirstTreenumeratorFactory;
      _DepthFirstTreenumeratorFactory = depthFirstTreenumeratorFactory;
    }

    private readonly ITreenumerable<TInner> _InnerTreenumerable;
    private readonly Func<ITreenumerator<TInner>, ITreenumerator<TNode>> _BreadthFirstTreenumeratorFactory;
    private readonly Func<ITreenumerator<TInner>, ITreenumerator<TNode>> _DepthFirstTreenumeratorFactory;

    public ITreenumerator<TNode> GetBreadthFirstTreenumerator()
      => _BreadthFirstTreenumeratorFactory(_InnerTreenumerable.GetBreadthFirstTreenumerator());

    public ITreenumerator<TNode> GetDepthFirstTreenumerator()
      => _DepthFirstTreenumeratorFactory(_InnerTreenumerable.GetDepthFirstTreenumerator());
  }
}
