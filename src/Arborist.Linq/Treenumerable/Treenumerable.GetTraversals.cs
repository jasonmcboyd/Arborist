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

          if (treenumerator.Mode == TreenumeratorMode.SchedulingNode)
            nodeTraversalStrategies = nodeTraversalStrategiesSelector(treenumerator.ToNodeContext());
          else
            nodeTraversalStrategies = NodeTraversalStrategies.TraverseAll;
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
