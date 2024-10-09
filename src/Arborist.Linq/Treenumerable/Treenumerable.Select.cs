using Arborist.Core;
using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TResult> Select<TSource, TResult>(
      this ITreenumerable<TSource> source,
      Func<NodeContext<TSource>, TResult> selector)
      => TreenumerableFactory.Create(
        () => new SelectTreenumerator<TSource, TResult>(source.GetBreadthFirstTreenumerator, selector),
        () => new SelectTreenumerator<TSource, TResult>(source.GetDepthFirstTreenumerator, selector));
  }
}
