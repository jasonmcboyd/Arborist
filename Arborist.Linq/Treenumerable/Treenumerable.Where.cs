using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<T> Where<T>(
      this ITreenumerable<T> source,
      Func<NodeVisit<T>, bool> predicate)
      => TreenumerableFactory.Create(
        source,
        breadthFirstTreenumerator => new FilterTreenumerator<T>(breadthFirstTreenumerator, predicate, SchedulingStrategy.SkipNode),
        depthFirstTreenumerator => new FilterTreenumerator<T>(depthFirstTreenumerator, predicate, SchedulingStrategy.SkipNode));
  }
}
