using Arborist.Core;
using Arborist.Linq.Extensions;
using System;
using System.Linq;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static bool AnyNode<TNode>(
      this ITreenumerable<TNode> source,
      Func<NodeContext<TNode>, bool> predicate,
      TreeTraversalStrategy treeTraversalStrategy = TreeTraversalStrategy.BreadthFirst)
    {
      var nodeTraversalStrategy =
        treeTraversalStrategy == TreeTraversalStrategy.BreadthFirst
        ? NodeTraversalStrategy.TraverseSubtree
        : NodeTraversalStrategy.SkipNode;

      using (var treenumerator = source.GetTreenumerator(treeTraversalStrategy))
        while (treenumerator.MoveNext(nodeTraversalStrategy))
          if (treenumerator.Mode == TreenumeratorMode.SchedulingNode && predicate(treenumerator.ToNodeContext()))
            return true;

      return false;
    }
  }
}
