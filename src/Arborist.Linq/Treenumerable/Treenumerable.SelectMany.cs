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
        source,
        breadthFirstTreenumerator => new SelectManyBreadthFirstTreenumerator<TSource, TResult>(breadthFirstTreenumerator, selector),
        depthFirstTreenumerator => new SelectManyDepthFirstTreenumerator<TSource, TResult>(depthFirstTreenumerator, selector));

    public static ITreenumerable<TSource> SelectMany<TSource>(
      this ITreenumerable<ITreenumerable<TSource>> source)
      => TreenumerableFactory.Create(
        source,
        breadthFirstTreenumerator => new SelectManyBreadthFirstTreenumerator<ITreenumerable<TSource>, TSource>(breadthFirstTreenumerator, tree => tree),
        depthFirstTreenumerator => new SelectManyDepthFirstTreenumerator<ITreenumerable<TSource>, TSource>(depthFirstTreenumerator, tree => tree));
  }
}
