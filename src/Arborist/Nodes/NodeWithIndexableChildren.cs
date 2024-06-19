using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Nodes
{
  public sealed class NodeWithIndexableChildren<TNode> : INodeWithIndexableChildren<TNode>
  {
    public NodeWithIndexableChildren(TNode value)
      : this(value, Array.Empty<NodeWithIndexableChildren<TNode>>())
    {
    }

    public NodeWithIndexableChildren(
      TNode value,
      IEnumerable<NodeWithIndexableChildren<TNode>> children)
      : this(value, children?.ToArray())
    {
    }

    public NodeWithIndexableChildren(
      TNode value,
      params TNode[] children)
      : this(value, children?.Select(Node.CreateNodeWithIndexableChildren))
    {
    }

    public NodeWithIndexableChildren(
      TNode value,
      params NodeWithIndexableChildren<TNode>[] children)
    {
      Value = value;
      _Children = children ?? Array.Empty<NodeWithIndexableChildren<TNode>>();
    }

    private readonly NodeWithIndexableChildren<TNode>[] _Children;

    public INodeWithIndexableChildren<TNode> this[int index] => _Children[index];

    public int ChildCount => _Children.Length;

    public TNode Value { get; }
  }
}
