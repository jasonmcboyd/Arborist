using Arborist.Core;
using Arborist.Linq.Extensions;
using System;
using System.Linq;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static bool AnyNodes<TNode>(
      this ITreenumerable<TNode> source,
      Func<NodeContext<TNode>, bool> predicate,
      TreeTraversalStrategy treeTraversalStrategy = TreeTraversalStrategy.BreadthFirst)
    {
      var nodeTraversalStrategies =
        treeTraversalStrategy == TreeTraversalStrategy.BreadthFirst
        ? NodeTraversalStrategies.TraverseAll
        : NodeTraversalStrategies.SkipNode;

      using (var treenumerator = source.GetTreenumerator(treeTraversalStrategy))
        while (treenumerator.MoveNext(nodeTraversalStrategies))
          if (treenumerator.Mode == TreenumeratorMode.SchedulingNode && predicate(treenumerator.ToNodeContext()))
            return true;

      return false;
    }
  }
}
