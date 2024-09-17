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
    private static NodeWithEnumerableChildren<TValue> CreateNodeWithEnumerableChildren<TValue, TNode>(
      this TNode node)
      where TNode : INodeWithIndexableChildren<TValue, TNode>
    {
      var children = new TNode[node.ChildCount];

      for (int i = 0; i < node.ChildCount; i++)
        children[i] = node[i];

      return new NodeWithEnumerableChildren<TValue>(node.Value, children.Select(CreateNodeWithEnumerableChildren<TValue, TNode>));
    }

    public static IEnumerable<NodeWithEnumerableChildren<TValue>> CreateNodeWithEnumerableChildren<TValue, TNode>(
      this IEnumerable<TNode> roots)
      where TNode : INodeWithIndexableChildren<TValue, TNode>
    {
      foreach (var root in roots)
        yield return CreateNodeWithEnumerableChildren<TValue, TNode>(root);
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

    public static ITreenumerable<TValue> ToTreenumerable<TValue, TNode>(
      this IEnumerable<TNode> rootNode)
      where TNode : INodeWithIndexableChildren<TValue, TNode>
      => new IndexableTreenumerable<TValue, TNode>(rootNode);

    public static ITreenumerable<TValue> ToTreenumerable<TValue, TNode>(
      this TNode rootNode)
      where TNode : INodeWithIndexableChildren<TValue, TNode>
      => new IndexableTreenumerable<TValue, TNode>(rootNode);

    #endregion Treenumerable Factory Methods
  }
}
