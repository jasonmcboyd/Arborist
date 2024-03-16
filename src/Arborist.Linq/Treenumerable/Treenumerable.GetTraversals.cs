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
      Func<NodeVisit<TNode>, SchedulingStrategy> schedulingStrategySelector)
    {
      using (var treenumerator = source.GetDepthFirstTreenumerator())
        return GetTraversal(treenumerator, schedulingStrategySelector);
    }

    public static IEnumerable<NodeVisit<TNode>> GetBreadthFirstTraversal<TNode>(
      this ITreenumerable<TNode> source,
      Func<NodeVisit<TNode>, SchedulingStrategy> schedulingStrategySelector)
    {
      using (var treenumerator = source.GetBreadthFirstTreenumerator())
        return GetTraversal(treenumerator, schedulingStrategySelector);
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
      Func<NodeVisit<TNode>, SchedulingStrategy> schedulingStrategySelector)
    {
      if (!treenumerator.MoveNext())
        yield break;

      yield return treenumerator.ToNodeVisit();

      var schedulingStrategy = schedulingStrategySelector(treenumerator.ToNodeVisit());

      while (treenumerator.MoveNext(schedulingStrategy))
      {
        yield return treenumerator.ToNodeVisit();

        if (treenumerator.Mode == TreenumeratorMode.SchedulingNode)
          schedulingStrategy = schedulingStrategySelector(treenumerator.ToNodeVisit());
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
