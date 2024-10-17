﻿using Arborist.Core;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<TNode> GetRoots<TNode>(this ITreenumerable<TNode> source)
    {
      using (var treenumerator = source.GetDepthFirstTreenumerator())
      {
        while (treenumerator.MoveNext(NodeTraversalStrategy.SkipDescendants))
          if (treenumerator.Mode == TreenumeratorMode.SchedulingNode)
            yield return treenumerator.Node;
      }
    }
  }
}
