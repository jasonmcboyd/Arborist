using Arborist.Core;
using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<T> PruneBefore<T>(
      this ITreenumerable<T> source,
      Func<NodeContext<T>, bool> predicate)
    {
      var result =
        TreenumerableFactory.Create(
          () => new WhereBreadthFirstTreenumerator<T>(
            source.GetBreadthFirstTreenumerator,
            predicate,
            NodeTraversalStrategy.SkipSubtree),
          () => new PruneBeforeDepthFirstTreenumerator<T>(
            source.GetDepthFirstTreenumerator,
            predicate));

      return result;
    }
  }
}
