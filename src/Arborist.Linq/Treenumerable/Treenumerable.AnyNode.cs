using Arborist.Common;
using Arborist.Core;
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
      var materializedSource = source.Materialize();

      var traversal =
        treeTraversalStrategy == TreeTraversalStrategy.BreadthFirst
        ? materializedSource.LevelOrderTraversal()
        : materializedSource.PreOrderTraversal();

      return traversal.Any(predicate);
    }
  }
}
