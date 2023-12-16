using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<T> Prune<T>(
      this ITreenumerable<T> source,
      Func<NodeVisit<T>, bool> predicate,
      PruneOption pruneOption)
    {
      var result =
        TreenumerableFactory.Create(
          source,
          breadthFirstEnumerator => new PruneTreenumerator<T>(breadthFirstEnumerator, predicate),
          depthFirstEnumerator => new PruneTreenumerator<T>(depthFirstEnumerator, predicate));

      if (pruneOption == PruneOption.PruneBeforeNode)
        result = result.Where(visit => !predicate(visit));

      return result;
    }
  }
}
