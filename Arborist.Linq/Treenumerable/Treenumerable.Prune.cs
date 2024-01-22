using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<T> Prune<T>(
      this ITreenumerable<T> source,
      Func<NodeVisit<T>, bool> predicate)
    {
      var result =
        TreenumerableFactory.Create(
          source,
          breadthFirstEnumerator => new FilterTreenumerator<T>(breadthFirstEnumerator, visit => !predicate(visit), SchedulingStrategy.SkipSubtree),
          depthFirstEnumerator => new FilterTreenumerator<T>(depthFirstEnumerator, predicate, SchedulingStrategy.SkipSubtree));

      return result;
    }
  }
}
