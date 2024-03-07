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
        breadthFirstTreenumerator => throw new NotSupportedException(),
        depthFirstTreenumerator => new SelectManyDepthFirstTreenumerator<TSource, TResult>(depthFirstTreenumerator, selector));
  }
}
