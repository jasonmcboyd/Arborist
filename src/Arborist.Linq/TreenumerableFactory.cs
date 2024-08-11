using Arborist.Core;
using Arborist.Linq.Treenumerables;
using System;

namespace Arborist.Linq
{
  public static class TreenumerableFactory
  {
    public static ITreenumerable<TNode> Create<TInner, TNode>(
      ITreenumerable<TInner> innerTreenumerable,
      Func<ITreenumerator<TInner>, ITreenumerator<TNode>> breadthFirstTreenumeratorFactory,
      Func<ITreenumerator<TInner>, ITreenumerator<TNode>> depthFirstTreenumeratorFactory)
      => new DelegatingTreenumerable<TInner, TNode>(
        innerTreenumerable,
        breadthFirstTreenumeratorFactory,
        depthFirstTreenumeratorFactory);
  }
}
