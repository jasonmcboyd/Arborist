using Arborist.Core;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<TNode> LevelOrderTraversal<TNode>(this ITreenumerable<TNode> source)
    {
      if (source == null)
        yield break;

      using (var treenumerator = source.GetBreadthFirstTreenumerator())
        while (treenumerator.MoveNext(NodeTraversalStrategies.TraverseAll))
          if (treenumerator.VisitCount == 0)
            yield return treenumerator.Node;
    }
  }
}
