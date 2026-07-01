using Copse.Core;
using Copse.Linq.Treenumerators;
using System;

namespace Copse.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<T> PruneAfter<T>(
      this ITreenumerable<T> source,
      Func<NodeContext<T>, bool> predicate)
    {
      if (predicate == null)
        return source;

      return
        TreenumerableFactory.Create(
          () => new PruneAfterTreenumerator<T>(source.GetBreadthFirstTreenumerator, predicate),
          () => new PruneAfterTreenumerator<T>(source.GetDepthFirstTreenumerator, predicate));
    }
  }
}
