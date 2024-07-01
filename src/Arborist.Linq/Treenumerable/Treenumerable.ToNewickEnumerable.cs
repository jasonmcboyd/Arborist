using Arborist.Core;
using Arborist.Linq.Newick;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<NewickToken<TNode>> ToNewickEnumerable<TNode>(
      this ITreenumerable<TNode> source)
    {
      using (var treenumerator = source.GetDepthFirstTreenumerator())
      {
        var previousDepth = 0;

        while (treenumerator.MoveNext(NodeTraversalStrategy.TraverseSubtree))
        {
          if (treenumerator.VisitCount != 1)
            continue;

          var depth = treenumerator.Position.Depth;

          if (depth != previousDepth)
          {
            if (depth > previousDepth)
            {
              while (depth > previousDepth)
              {
                yield return new NewickToken<TNode>(NewickTokenType.StartChildGroup);
                previousDepth++;
              }
            }
            else
            {
              while (depth < previousDepth)
              {
                yield return new NewickToken<TNode>(NewickTokenType.EndChildGroup);
                previousDepth--;
              }
            }

            previousDepth = depth;
          }

          yield return new NewickToken<TNode>(treenumerator.Node);
        }

        while (previousDepth > 0)
        {
          yield return new NewickToken<TNode>(NewickTokenType.EndChildGroup);
          previousDepth--;
        }
      }
    }
  }
}
