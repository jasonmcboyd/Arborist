using Arborist.Core;
using Arborist.Treenumerables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Nodes
{
  public sealed class IndexableTreeNode<TNode> : INodeContainerWithIndexableChildren<TNode>
  {
    public IndexableTreeNode(TNode value)
      : this(value, Array.Empty<IndexableTreeNode<TNode>>())
    {
    }

    public IndexableTreeNode(
      TNode value,
      IEnumerable<IndexableTreeNode<TNode>> children)
      : this(value, children?.ToArray())
    {
    }

    public IndexableTreeNode(
      TNode value,
      params TNode[] children)
      : this(value, children?.Select(IndexableTreeNode.Create).ToArray() ?? Array.Empty<IndexableTreeNode<TNode>>())
    {
    }

    public IndexableTreeNode(
      TNode value,
      params IndexableTreeNode<TNode>[] children)
    {
      Value = value;
      _Children = children ?? Array.Empty<IndexableTreeNode<TNode>>();
    }

    private readonly IndexableTreeNode<TNode>[] _Children;

    public INodeContainerWithIndexableChildren<TNode> this[int index] => _Children[index];

    public int ChildCount => _Children.Length;

    public TNode Value { get; }
  }

  public static class IndexableTreeNode
  {
    public static IndexableTreeNode<TNode> Create<TNode>(TNode value) => new IndexableTreeNode<TNode>(value);

    public static IndexableTreeNode<TNode> Create<TNode>(TNode value, params TNode[] children) => new IndexableTreeNode<TNode>(value, children);

    public static IndexableTreeNode<TNode> Create<TNode>(TNode value, params IndexableTreeNode<TNode>[] children) => new IndexableTreeNode<TNode>(value, children);

    public static ITreenumerable<TNode> ToTreenumerable<TNode>(this INodeContainerWithIndexableChildren<TNode> rootNode)
      => new IndexableTreenumerable<TNode>(rootNode);
  }
}
