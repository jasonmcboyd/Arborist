using Arborist.Core;
using Arborist.Nodes;
using Arborist.Treenumerables;
using System.Collections.Generic;

namespace Arborist
{
  public static class EnumerableExtensions
  {
    public static ITreenumerable<TNode> ToTreenumerable<TNode>(
      this IEnumerable<INodeContainerWithEnumerableChildren<TNode>> rootNodes)
      => new EnumerableTreenumerable<TNode>(rootNodes);

    public static ITreenumerable<TNode> ToTreenumerable<TNode>(
      this IEnumerable<INodeContainerWithIndexableChildren<TNode>> rootNodes)
      => new IndexableTreenumerable<TNode>(rootNodes);
  }
}
