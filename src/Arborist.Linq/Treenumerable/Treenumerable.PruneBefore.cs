﻿using Arborist.Core;
using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<T> PruneBefore<T>(
      this ITreenumerable<T> source,
      Func<NodeVisit<T>, bool> predicate)
    {
      var result =
        TreenumerableFactory.Create(
          source,
          breadthFirstEnumerator => new PruneBeforeBreadthFirstTreenumerator<T>(breadthFirstEnumerator, visit => predicate(visit)),
          depthFirstEnumerator => new PruneBeforeDepthFirstTreenumerator<T>(depthFirstEnumerator, visit => predicate(visit)));

      return result;
    }
  }
}
