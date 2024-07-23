using Arborist.Core;
using Arborist.Linq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TResult> WithParent<TSource, TResult>(
      this ITreenumerable<TSource> source,
      Func<NodeContext<TSource>, NodeContext<TSource>?, TResult> selector)
    {
      var nodeContexts = new List<NodeContext<TSource>>();

      return
        source
        .Do(visit =>
        {
          if (visit.Mode == TreenumeratorMode.VisitingNode)
            return;

          if (visit.Position.Depth > nodeContexts.Count - 1)
            nodeContexts.Add(visit.ToNodeContext());
          else if (visit.Position.Depth == nodeContexts.Count - 1)
            nodeContexts[nodeContexts.Count - 1] = visit.ToNodeContext();
          else
            nodeContexts.RemoveLast();
        })
        .Select(nodeContext => selector(nodeContext, nodeContexts.Count == 1 ? (NodeContext<TSource>?)null : nodeContexts[nodeContexts.Count - 2]));
    }
  }
}
