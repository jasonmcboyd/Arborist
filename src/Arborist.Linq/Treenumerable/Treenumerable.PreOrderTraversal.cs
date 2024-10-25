using Arborist.Core;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<TNode> PreOrderTraversal<TNode>(this ITreenumerable<TNode> source)
    {
      if (source == null)
        yield break;

      using (var treenumerator = source.GetDepthFirstTreenumerator())
        while (treenumerator.MoveNext(NodeTraversalStrategies.SkipNode))
          yield return treenumerator.Node;
    }
  }
}
