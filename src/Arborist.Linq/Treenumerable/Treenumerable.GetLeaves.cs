﻿using Arborist.Core;
using Arborist.Linq.Extensions;
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

        treenumerator.MoveNext(NodeTraversalStrategy.TraverseSubtree);

        NodeVisit<TNode> previousVisit;
        NodeVisit<TNode> currentVisit = treenumerator.ToNodeVisit();

        while (treenumerator.MoveNext(NodeTraversalStrategy.TraverseSubtree))
        {
          if (treenumerator.VisitCount == 0)
            continue;

          previousVisit = currentVisit;
          currentVisit = treenumerator.ToNodeVisit();

          if (previousVisit.VisitCount == 1)
          {
            if (previousVisit.Position.Depth > currentVisit.Position.Depth
              || previousVisit.Position.Depth == currentVisit.Position.Depth)
            {
              yield return previousVisit.Node;
            }
          }
        }

        if (currentVisit.VisitCount == 1)
          yield return currentVisit.Node;
      }
    }
  }
}
