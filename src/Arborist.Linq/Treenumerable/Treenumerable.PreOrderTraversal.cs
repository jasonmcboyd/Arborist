﻿using Arborist.Core;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<T> PreOrderTraversal<T>(this ITreenumerable<T> source)
    {
      if (source == null)
        yield break;

      using (var treenumerator = source.GetDepthFirstTreenumerator())
        while (treenumerator.MoveNext(NodeTraversalStrategy.TraverseSubtree))
          if (treenumerator.Mode == TreenumeratorMode.SchedulingNode)
            yield return treenumerator.Node;
    }
  }
}
