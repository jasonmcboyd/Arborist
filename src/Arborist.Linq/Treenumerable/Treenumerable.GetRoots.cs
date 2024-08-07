﻿using Arborist.Core;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<T> GetRoots<T>(this ITreenumerable<T> source)
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
