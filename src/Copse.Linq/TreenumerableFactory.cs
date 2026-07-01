using Copse.Core;
using Copse.Linq.Treenumerables;
using System;

namespace Copse.Linq
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
