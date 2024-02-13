using System.Collections.Generic;

namespace Arborist.Treenumerables
{
  public static class EnumerableExtensions
  {
    public static ITreenumerable<INodeWithEnumerableChildren<TNode>> ToTreenumerable<TNode>(
      this IEnumerable<INodeWithEnumerableChildren<TNode>> rootNodes)
      => new Treenumerable<INodeWithEnumerableChildren<TNode>>(rootNodes, node => node.Children.GetEnumerator());

    //public static ITreenumerable<INodeWithIndexableChildren<TNode>> ToTreenumerable<TNode>(
    //  this IEnumerable<INodeWithIndexableChildren<TNode>> rootNodes)
    //  => new Treenumerable<INodeWithIndexableChildren<TNode>>(rootNodes, node => node.Children.GetEnumerator());
  }
}
