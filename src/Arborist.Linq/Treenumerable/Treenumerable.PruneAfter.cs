using Arborist.Core;
using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<T> PruneAfter<T>(
      this ITreenumerable<T> source,
      Func<NodeVisit<T>, bool> predicate)
    {
      var result =
        TreenumerableFactory.Create(
          source,
          breadthFirstEnumerator => new PruneAfterTreenumerator<T>(breadthFirstEnumerator, visit => predicate(visit)),
          depthFirstEnumerator => new PruneAfterTreenumerator<T>(depthFirstEnumerator, visit => predicate(visit)));

      return result;
    }
  }
}
