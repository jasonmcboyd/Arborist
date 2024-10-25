using Arborist.Core;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<TNode> PostOrderTraversal<TNode>(this ITreenumerable<TNode> source)
    {
      if (source == null)
        yield break;

      var nodes = new RefSemiDeque<TNode>();

      using (var treenumerator = source.GetDepthFirstTreenumerator())
      {
        while (treenumerator.MoveNext(NodeTraversalStrategies.SkipNode))
        {
          while (nodes.Count - 1 >= treenumerator.Position.Depth)
            yield return nodes.RemoveLast();

          nodes.AddLast(treenumerator.Node);
        }
      }

      while (nodes.Count > 0)
        yield return nodes.RemoveLast();
    }
  }
}
