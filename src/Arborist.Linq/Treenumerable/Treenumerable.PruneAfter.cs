using Arborist.Core;
using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<T> PruneAfter<T>(
      this ITreenumerable<T> source,
      Func<NodeContext<T>, bool> predicate)
    {
      var result =
        TreenumerableFactory.Create(
          source,
          breadthFirstEnumerator => new PruneAfterTreenumerator<T>(breadthFirstEnumerator, predicate),
          depthFirstEnumerator => new PruneAfterTreenumerator<T>(depthFirstEnumerator, predicate));

      return result;
    }
  }
}
