using Arborist.Core;
using Arborist.Linq.PreorderTree;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<PreorderTreeToken<TNode>> ToPreorderTreeEnumerable<TNode>(
      this ITreenumerable<TNode> source)
    {
      using (var treenumerator = source.GetDepthFirstTreenumerator())
      {
        var previousDepth = 0;

        while (treenumerator.MoveNext(NodeTraversalStrategy.TraverseSubtree))
        {
          if (treenumerator.Mode != TreenumeratorMode.SchedulingNode)
            continue;

          var depth = treenumerator.Position.Depth;

          if (depth != previousDepth)
          {
            if (depth > previousDepth)
            {
              while (depth > previousDepth)
              {
                yield return new PreorderTreeToken<TNode>(PreorderTreeTokenType.StartChildGroup);
                previousDepth++;
              }
            }
            else
            {
              while (depth < previousDepth)
              {
                yield return new PreorderTreeToken<TNode>(PreorderTreeTokenType.EndChildGroup);
                previousDepth--;
              }
            }

            previousDepth = depth;
          }

          yield return new PreorderTreeToken<TNode>(treenumerator.Node);
        }

        while (previousDepth > 0)
        {
          yield return new PreorderTreeToken<TNode>(PreorderTreeTokenType.EndChildGroup);
          previousDepth--;
        }
      }
    }
  }
}
