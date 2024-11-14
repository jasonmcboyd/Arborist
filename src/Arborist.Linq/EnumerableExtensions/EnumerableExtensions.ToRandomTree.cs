using Arborist.Core;
using Arborist.Linq.Treenumerables;
using System;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class EnumerableExtensions
  {
    public static ITreenumerable<TNode> ToRandomTree<TNode>(
      this IEnumerable<TNode> source,
      double weight = 0.5d)
    {
      var random = new Random();

      return
        new EnumerableTreenumerable<TNode>(source, nodeContext => nodeContext.Position.Depth == 1 || random.NextDouble() < weight);
    }
  }
}
