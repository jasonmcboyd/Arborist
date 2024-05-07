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
      // There is no reason a consumer _should_ ever pass a traversal strategy
      // before enumeration has begun. But just because they should not does not
      // mean they will not. If a user were to pass a traversal strategy on the
      // very first call to MoveNext, ideally, all treenumerators would handle that
      // correctly.
      //
      // So, the "correct" thing to do here might be to simply ignore the
      // traversal strategy on the first call to MoveNext. In fact, that is
      // what I did initially. But it turned out there were some treenumerators
      // that were not handling this initial call to MoveNext correctly. By ignoring
      // the traversal strategy, I was hiding bugs. So, I decided to change the
      // behavior here so that those bugs would be exposed
      var traversalStrategy = traversalStrategySelector(treenumerator.ToNodeVisit());

      if (!treenumerator.MoveNext(traversalStrategy))
        yield break;

      yield return treenumerator.ToNodeVisit();

      traversalStrategy = traversalStrategySelector(treenumerator.ToNodeVisit());

      while (treenumerator.MoveNext(traversalStrategy))
      {
        yield return treenumerator.ToNodeVisit();

        // Should only need to get the traversal strategy if we are scheduling a
        // node. If we are traversing a node, the treenumerator should ignore
        // the traversal strategy. I was doing that originally, but then I found
        // that it was covering up poorly behaved treenumerators. So I changed the
        // behavior here so that those bugs would get surfaced.
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
