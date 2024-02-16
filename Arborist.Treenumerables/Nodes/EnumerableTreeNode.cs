using System.Collections.Generic;
using System.Linq;

namespace Arborist.Treenumerables.Nodes
{
  public sealed class EnumerableTreeNode<TValue>
    : INodeContainerWithEnumerableChildren<TValue>
  {
    public EnumerableTreeNode(TValue value)
      : this(value, Enumerable.Empty<EnumerableTreeNode<TValue>>())
    {
    }

    public EnumerableTreeNode(
      TValue value,
      IEnumerable<EnumerableTreeNode<TValue>> children)
    {
      Value = value;
      Children = children;
    }

    public TValue Value { get; }

    public IEnumerable<INodeContainerWithEnumerableChildren<TValue>> Children { get; private set; }
  }

  public static class EnumerableTreeNode
  {
    private static EnumerableTreeNode<TNode> Create<TNode>(
      INodeContainerWithIndexableChildren<TNode> node)
    {
      var children = new INodeContainerWithIndexableChildren<TNode>[node.ChildCount];

      for (int i = 0; i < node.ChildCount; i++)
        children[i] = node[i];

      return new EnumerableTreeNode<TNode>(node.Value, children.Select(nodeContainer => Create(nodeContainer)));
    }

    public static IEnumerable<EnumerableTreeNode<TNode>> Create<TNode>(
      IEnumerable<INodeContainerWithIndexableChildren<TNode>> roots)
    {
      foreach (var root in roots)
        yield return Create(root);
    }
  }
}
