using Arborist.Core;
using Arborist.Treenumerables;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Nodes
{
  public static class Node
  {
    #region Node With Enumerable Children Factory Methods

    // TODO: I don't like this. It is recursive.
    private static NodeWithEnumerableChildren<TNode> CreateNodeWithEnumerableChildren<TNode>(
      this INodeWithIndexableChildren<TNode> node)
    {
      var children = new INodeWithIndexableChildren<TNode>[node.ChildCount];

      for (int i = 0; i < node.ChildCount; i++)
        children[i] = node[i];

      return new NodeWithEnumerableChildren<TNode>(node.Value, children.Select(CreateNodeWithEnumerableChildren));
    }

    public static IEnumerable<NodeWithEnumerableChildren<TNode>> CreateNodeWithEnumerableChildren<TNode>(
      this IEnumerable<INodeWithIndexableChildren<TNode>> roots)
    {
      foreach (var root in roots)
        yield return CreateNodeWithEnumerableChildren(root);
    }

    #endregion Node With Enumerable Children Factory Methods

    #region Node With Indexable Children Factory Methods

    public static NodeWithIndexableChildren<TNode> CreateNodeWithIndexableChildren<TNode>(this TNode value) => new NodeWithIndexableChildren<TNode>(value);

    public static NodeWithIndexableChildren<TNode> CreateNodeWithIndexableChildren<TNode>(this TNode value, params TNode[] children) => new NodeWithIndexableChildren<TNode>(value, children);

    public static NodeWithIndexableChildren<TNode> CreateNodeWithIndexableChildren<TNode>(this TNode value, params NodeWithIndexableChildren<TNode>[] children) => new NodeWithIndexableChildren<TNode>(value, children);

    #endregion Node With Indexable Children Factory Methods

    #region Treenumerable Factory Methods

    public static ITreenumerable<TNode> ToTreenumerable<TNode>(
      this IEnumerable<INodeWithEnumerableChildren<TNode>> rootNodes)
      => new EnumerableTreenumerable<TNode>(rootNodes);

    public static ITreenumerable<TNode> ToTreenumerable<TNode>(
      this INodeWithEnumerableChildren<TNode> rootNodes)
      => new EnumerableTreenumerable<TNode>(rootNodes);

    public static ITreenumerable<TNode> ToTreenumerable<TNode>(
      this IEnumerable<INodeWithIndexableChildren<TNode>> rootNode)
      => new IndexableTreenumerable<TNode>(rootNode);

    public static ITreenumerable<TNode> ToTreenumerable<TNode>(
      this INodeWithIndexableChildren<TNode> rootNode)
      => new IndexableTreenumerable<TNode>(rootNode);

    #endregion Treenumerable Factory Methods
  }
}
