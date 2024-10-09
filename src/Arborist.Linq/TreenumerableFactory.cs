using Arborist.Core;
using Arborist.Linq.Treenumerables;
using System;

namespace Arborist.Linq
{
  public static class TreenumerableFactory
  {
    public static ITreenumerable<TNode> Create<TNode>(
      Func<ITreenumerator<TNode>> breadthFirstTreenumeratorFactory,
      Func<ITreenumerator<TNode>> depthFirstTreenumeratorFactory)
      => new DelegatingTreenumerable<TNode>(
        breadthFirstTreenumeratorFactory,
        depthFirstTreenumeratorFactory);
  }
}
