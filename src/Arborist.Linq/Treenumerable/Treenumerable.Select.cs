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
        source,
        breadthFirstEnumerator => new SelectTreenumerator<TSource, TResult>(breadthFirstEnumerator, selector),
        depthFirstEnumerator => new SelectTreenumerator<TSource, TResult>(depthFirstEnumerator, selector));
  }
}
