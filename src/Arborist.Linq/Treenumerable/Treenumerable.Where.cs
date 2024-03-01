using Arborist.Core;
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
        breadthFirstTreenumerator => new WhereTreenumerator<T>(breadthFirstTreenumerator, predicate),
        depthFirstTreenumerator => new WhereTreenumerator<T>(depthFirstTreenumerator, predicate));
  }
}
