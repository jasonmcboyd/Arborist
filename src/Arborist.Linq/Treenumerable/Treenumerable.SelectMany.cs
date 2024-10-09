using Arborist.Core;
using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TResult> SelectMany<TSource, TResult>(
      this ITreenumerable<TSource> source,
      Func<TSource, ITreenumerable<TResult>> selector)
      => TreenumerableFactory.Create(
        () => new SelectManyBreadthFirstTreenumerator<TSource, TResult>(source.GetBreadthFirstTreenumerator, selector),
        () => new SelectManyDepthFirstTreenumerator<TSource, TResult>(source.GetDepthFirstTreenumerator, selector));

    public static ITreenumerable<TSource> SelectMany<TSource>(
      this ITreenumerable<ITreenumerable<TSource>> source)
      => TreenumerableFactory.Create(
        () => new SelectManyBreadthFirstTreenumerator<ITreenumerable<TSource>, TSource>(source.GetBreadthFirstTreenumerator, tree => tree),
        () => new SelectManyDepthFirstTreenumerator<ITreenumerable<TSource>, TSource>(source.GetDepthFirstTreenumerator, tree => tree));
  }
}
