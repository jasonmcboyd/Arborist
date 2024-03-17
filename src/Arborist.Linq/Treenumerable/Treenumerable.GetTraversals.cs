using Arborist.Core;
using Arborist.Linq.Extensions;
using System;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<NodeVisit<TNode>> GetDepthFirstTraversal<TNode>(
      this ITreenumerable<TNode> source,
      Func<NodeVisit<TNode>, TraversalStrategy> traversalStrategySelector)
    {
      using (var treenumerator = source.GetDepthFirstTreenumerator())
        return GetTraversal(treenumerator, traversalStrategySelector);
    }

    public static IEnumerable<NodeVisit<TNode>> GetBreadthFirstTraversal<TNode>(
      this ITreenumerable<TNode> source,
      Func<NodeVisit<TNode>, TraversalStrategy> traversalStrategySelector)
    {
      using (var treenumerator = source.GetBreadthFirstTreenumerator())
        return GetTraversal(treenumerator, traversalStrategySelector);
    }

    public static IEnumerable<NodeVisit<TNode>> GetDepthFirstTraversal<TNode>(
      this ITreenumerable<TNode> source)
    {
      using (var treenumerator = source.GetDepthFirstTreenumerator())
        return GetTraversal(treenumerator);
    }

    public static IEnumerable<NodeVisit<TNode>> GetBreadthFirstTraversal<TNode>(
      this ITreenumerable<TNode> source)
    {
      using (var treenumerator = source.GetBreadthFirstTreenumerator())
        return GetTraversal(treenumerator);
    }

    private static IEnumerable<NodeVisit<TNode>> GetTraversal<TNode>(
      this ITreenumerator<TNode> treenumerator,
      Func<NodeVisit<TNode>, TraversalStrategy> traversalStrategySelector)
    {
      if (!treenumerator.MoveNext())
        yield break;

      yield return treenumerator.ToNodeVisit();

      var traversalStrategy = traversalStrategySelector(treenumerator.ToNodeVisit());

      while (treenumerator.MoveNext(traversalStrategy))
      {
        yield return treenumerator.ToNodeVisit();

        if (treenumerator.Mode == TreenumeratorMode.SchedulingNode)
          traversalStrategy = traversalStrategySelector(treenumerator.ToNodeVisit());
      }
    }

    private static IEnumerable<NodeVisit<TNode>> GetTraversal<TNode>(
      this ITreenumerator<TNode> treenumerator)
    {
      while (treenumerator.MoveNext())
        yield return treenumerator.ToNodeVisit();
    }
  }
}
