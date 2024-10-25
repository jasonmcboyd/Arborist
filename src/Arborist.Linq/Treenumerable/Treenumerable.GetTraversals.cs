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
      Func<NodeContext<TNode>, NodeTraversalStrategies> nodeTraversalStrategiesSelector)
    {
      return GetTraversal(source, TreeTraversalStrategy.DepthFirst, nodeTraversalStrategiesSelector);
    }

    public static IEnumerable<NodeVisit<TNode>> GetBreadthFirstTraversal<TNode>(
      this ITreenumerable<TNode> source,
      Func<NodeContext<TNode>, NodeTraversalStrategies> nodeTraversalStrategiesSelector)
    {
      return GetTraversal(source, TreeTraversalStrategy.BreadthFirst, nodeTraversalStrategiesSelector);
    }

    public static IEnumerable<NodeVisit<TNode>> GetDepthFirstTraversal<TNode>(
      this ITreenumerable<TNode> source)
    {
      return GetTraversal(source, TreeTraversalStrategy.DepthFirst);
    }

    public static IEnumerable<NodeVisit<TNode>> GetBreadthFirstTraversal<TNode>(
      this ITreenumerable<TNode> source)
    {
      return GetTraversal(source, TreeTraversalStrategy.BreadthFirst);
    }

    public static IEnumerable<NodeVisit<TNode>> GetTraversal<TNode>(
      this ITreenumerable<TNode> source,
      TreeTraversalStrategy treeTraversalStrategy,
      Func<NodeContext<TNode>, NodeTraversalStrategies> nodeTraversalStrategiesSelector)
    {
      using (var treenumerator = source.GetTreenumerator(treeTraversalStrategy))
      {
        if (!treenumerator.MoveNext(NodeTraversalStrategies.TraverseAll))
          yield break;

        yield return treenumerator.ToNodeVisit();

        var nodeTraversalStrategies = nodeTraversalStrategiesSelector(treenumerator.ToNodeContext());

        while (treenumerator.MoveNext(nodeTraversalStrategies))
        {
          yield return treenumerator.ToNodeVisit();

          // Should only need to get the traversal strategy if we are scheduling a
          // node. If we are traversing a node, the treenumerator should ignore
          // the traversal strategy. I was doing that originally, but then I found
          // that it was covering up poorly behaved treenumerators. So I changed the
          // behavior here so that those bugs would get surfaced.
          nodeTraversalStrategies = nodeTraversalStrategiesSelector(treenumerator.ToNodeContext());
        }
      }
    }

    public static IEnumerable<NodeVisit<TNode>> GetTraversal<TNode>(
      this ITreenumerable<TNode> source,
      TreeTraversalStrategy treeTraversalStrategy)
    {
      using (var treenumerator = source.GetTreenumerator(treeTraversalStrategy))
      {
        while (treenumerator.MoveNext(NodeTraversalStrategies.TraverseAll))
          yield return treenumerator.ToNodeVisit();
      }
    }
  }
}
