using Arborist.Core;
using Arborist.Linq.LevelOrderTree;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<LevelOrderTreeToken<TNode>> ToLevelOrderTreeEnumerable<TNode>(
      this ITreenumerable<TNode> source)
    {
      using (var treenumerator = source.GetBreadthFirstTreenumerator())
      {
        var previousDepth = -1;
        var hasCachedGenerationSeparator = false;
        LevelOrderTreeToken<TNode> cachedGenerationSeparator = default;

        if (!treenumerator.MoveNext(NodeTraversalStrategy.TraverseSubtree))
          yield break;

        yield return new LevelOrderTreeToken<TNode>(treenumerator.Node);

        while (treenumerator.MoveNext(NodeTraversalStrategy.TraverseSubtree))
        {
          if (hasCachedGenerationSeparator)
          {
            yield return cachedGenerationSeparator;
            hasCachedGenerationSeparator = false;
          }

          if (treenumerator.Mode == TreenumeratorMode.SchedulingNode)
          {
            yield return new LevelOrderTreeToken<TNode>(treenumerator.Node);
            continue;
          }

          if (treenumerator.VisitCount != 1)
            continue;

          if (treenumerator.Position.Depth == previousDepth)
          {
            yield return new LevelOrderTreeToken<TNode>(LevelOrderTreeTokenType.FamilySeparator);
          }
          else
          {
            cachedGenerationSeparator = new LevelOrderTreeToken<TNode>(LevelOrderTreeTokenType.GenerationSeparator);
            hasCachedGenerationSeparator = true;
            previousDepth = treenumerator.Position.Depth;
          }
        }
      }
    }
  }
}
