using System;

namespace Arborist.Linq.Treenumerables
{
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
