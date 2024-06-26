using Arborist.Core;
using System;
using System.Linq;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static bool AllNodes<TNode>(
      this ITreenumerable<TNode> source,
      Func<TNode, bool> predicate,
      TreeTraversalStrategy treeTraversalStrategy = TreeTraversalStrategy.BreadthFirst)
    {
      var traversal =
        treeTraversalStrategy == TreeTraversalStrategy.BreadthFirst
        ? source.LevelOrderTraversal()
        : source.PreOrderTraversal();

      return traversal.All(node => predicate(node));
    }
  }
}
