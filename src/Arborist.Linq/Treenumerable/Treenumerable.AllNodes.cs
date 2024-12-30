using Arborist.Core;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static bool AllNodes<TNode>(
      this ITreenumerable<TNode> source,
      Func<NodeContext<TNode>, bool> predicate,
      TreeTraversalStrategy treeTraversalStrategy = default)
    {
      return source.AnyNodes(nodeContext => !predicate(nodeContext), treeTraversalStrategy);
    }
  }
}
