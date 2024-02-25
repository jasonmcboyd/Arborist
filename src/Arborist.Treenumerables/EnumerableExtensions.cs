using Arborist.Core;
using Arborist.Treenumerables.Nodes;
using Arborist.Treenumerables.Treenumerables;
using System.Collections.Generic;

namespace Arborist.Treenumerables
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
