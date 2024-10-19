using Arborist.Core;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<TNode> GetLeaves<TNode>(this ITreenumerable<TNode> source)
    {
      using (var treenumerator = source.GetDepthFirstTreenumerator())
      {
        if (!treenumerator.MoveNext(NodeTraversalStrategy.TraverseSubtree))
          yield break;

        TNode previousNode = treenumerator.Node;
        int previousDepth = treenumerator.Position.Depth;

        while (treenumerator.MoveNext(NodeTraversalStrategy.SkipNode))
        {
          if (previousDepth >= treenumerator.Position.Depth)
            yield return previousNode;

          previousNode = treenumerator.Node;
          previousDepth = treenumerator.Position.Depth;
        }

        yield return previousNode;
      }
    }
  }
}
