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
          source,
          breadthFirstEnumerator => new WhereBreadthFirstTreenumerator<T>(breadthFirstEnumerator, predicate, NodeTraversalStrategy.SkipSubtree),
          depthFirstEnumerator => new PruneBeforeDepthFirstTreenumerator<T>(depthFirstEnumerator, predicate));

      return result;
    }
  }
}
