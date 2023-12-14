using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<T> Prune<T>(
      this ITreenumerable<T> source,
      Func<NodeVisit<T>, bool> predicate,
      PruneOptions pruneOptions)
      => TreenumerableFactory.Create(
        source,
        breadthFirstEnumerator => new PruneTreenumerator<T>(breadthFirstEnumerator, predicate, pruneOptions),
        depthFirstEnumerator => new PruneTreenumerator<T>(depthFirstEnumerator, predicate, pruneOptions));
  }
}
