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
    private static NodeWithEnumerableChildren<TValue> CreateNodeWithEnumerableChildren<TValue>(
      this NodeWithIndexableChildren<TValue> node)
    {
      var children = node.CopyChildren();

      return new NodeWithEnumerableChildren<TValue>(node.Value, children.Select(CreateNodeWithEnumerableChildren));
    }

    public static IEnumerable<NodeWithEnumerableChildren<TValue>> CreateNodeWithEnumerableChildren<TValue>(
      this IEnumerable<NodeWithIndexableChildren<TValue>> roots)
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

    private static TryMoveNextChildResult<NodeWithEnumerableChildren<TValue>> NodeWithEnumerableChildrenMoveNext<TValue>(
      ref NodeWithEnumerableChildrenChildEnumerator<TValue> childEnumerator)
      => childEnumerator.TryMoveNext();

    private static void NodeWithEnumerableChildrenDispose<TValue>(
      ref NodeWithEnumerableChildrenChildEnumerator<TValue> childEnumerator)
      => childEnumerator.Dispose();

    private static TryMoveNextChildResult<NodeWithIndexableChildren<TValue>> NodeWithIndexableChildrenMoveNext<TValue>(
      ref NodeWithIndexableChildrenChildEnumerator<TValue> childEnumerator)
      => childEnumerator.TryMoveNext();

    private static void NodeWithIndexableChildrenDispose<TValue>(
      ref NodeWithIndexableChildrenChildEnumerator<TValue> childEnumerator)
    {
    }

    public static ITreenumerable<TValue> ToTreenumerable<TValue>(
      this IEnumerable<NodeWithEnumerableChildren<TValue>> rootNodes)
      => new Treenumerable<TValue, NodeWithEnumerableChildren<TValue>, NodeWithEnumerableChildrenChildEnumerator<TValue>>(
        node => node.GetChildEnumerator(),
        NodeWithEnumerableChildrenMoveNext,
        NodeWithEnumerableChildrenDispose,
        node => node.Value,
        rootNodes);

    public static ITreenumerable<TValue> ToTreenumerable<TValue>(
      this NodeWithEnumerableChildren<TValue> rootNode)
      => new Treenumerable<TValue, NodeWithEnumerableChildren<TValue>, NodeWithEnumerableChildrenChildEnumerator<TValue>>(
        node => node.GetChildEnumerator(),
        NodeWithEnumerableChildrenMoveNext,
        NodeWithEnumerableChildrenDispose,
        node => node.Value,
        rootNode);

    public static ITreenumerable<TValue> ToTreenumerable<TValue>(
      this IEnumerable<NodeWithIndexableChildren<TValue>> rootNodes)
      => new Treenumerable<TValue, NodeWithIndexableChildren<TValue>, NodeWithIndexableChildrenChildEnumerator<TValue>>(
        node => node.GetChildEnumerator(),
        NodeWithIndexableChildrenMoveNext,
        NodeWithIndexableChildrenDispose,
        node => node.Value,
        rootNodes);

    public static ITreenumerable<TValue> ToTreenumerable<TValue>(
      this NodeWithIndexableChildren<TValue> rootNode)
      => new Treenumerable<TValue, NodeWithIndexableChildren<TValue>, NodeWithIndexableChildrenChildEnumerator<TValue>>(
        node => node.GetChildEnumerator(),
        NodeWithIndexableChildrenMoveNext,
        NodeWithIndexableChildrenDispose,
        node => node.Value,
        rootNode);

    #endregion Treenumerable Factory Methods
  }
}
