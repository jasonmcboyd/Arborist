using Arborist.Common;
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
          () => new PruneAfterTreenumerator<T>(source.GetBreadthFirstTreenumerator, predicate),
          () => new PruneAfterTreenumerator<T>(source.GetDepthFirstTreenumerator, predicate));

      return result;
    }
  }
}
