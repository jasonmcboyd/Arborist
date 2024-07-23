using Arborist.Core;
using System;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TResult> WithLevelIndex<TSource, TResult>(
      this ITreenumerable<TSource> source,
      Func<NodeContext<TSource>, int, TResult> selector)
    {
      var levelIndexes = new List<int>();

      return
        source
        .Do(visit =>
        {
          if (visit.Mode == TreenumeratorMode.VisitingNode)
            return;

          if (visit.Position.Depth >= levelIndexes.Count)
            levelIndexes.Add(0);
          else
            levelIndexes[visit.Position.Depth]++;
        })
        .Select(nodeContext => selector(nodeContext, levelIndexes[nodeContext.Position.Depth]));
    }
  }
}
