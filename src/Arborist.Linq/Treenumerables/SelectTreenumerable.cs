using Arborist.Common;
using Arborist.Core;
using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq.Treenumerables
{
  internal sealed class SelectTreenumerable<TSource, TResult> : ISelectTreenumerable<TResult>
  {
    public SelectTreenumerable(
      ITreenumerable<TSource> source,
      Func<NodeContext<TSource>, TResult> selector)
    {
      _Source = source;
      _Selector = selector;
    }

    private readonly ITreenumerable<TSource> _Source;
    private readonly Func<NodeContext<TSource>, TResult> _Selector;

    public ITreenumerator<TResult> GetBreadthFirstTreenumerator() =>
      new SelectTreenumerator<TSource, TResult>(_Source.GetBreadthFirstTreenumerator, _Selector);

    public ITreenumerator<TResult> GetDepthFirstTreenumerator() =>
      new SelectTreenumerator<TSource, TResult>(_Source.GetDepthFirstTreenumerator, _Selector);

    public ISelectTreenumerable<TOuterResult> Compose<TOuterResult>(
      Func<NodeContext<TResult>, TOuterResult> _outerSelector)
    {
      return
        new SelectTreenumerable<TSource, TOuterResult>(
          _Source,
          nodeContext => _outerSelector(new NodeContext<TResult>(_Selector(nodeContext), nodeContext.Position)));
    }
  }
}
