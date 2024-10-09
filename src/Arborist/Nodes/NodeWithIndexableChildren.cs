using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Nodes
{
  public sealed class NodeWithIndexableChildren<TValue> : INode<TValue, NodeWithIndexableChildrenChildEnumerator<TValue>>
  {
    public NodeWithIndexableChildren(TValue value)
      : this(value, Array.Empty<NodeWithIndexableChildren<TValue>>())
    {
    }

    public NodeWithIndexableChildren(
      TValue value,
      IEnumerable<NodeWithIndexableChildren<TValue>> children)
      : this(value, children?.ToArray())
    {
    }

    public NodeWithIndexableChildren(
      TValue value,
      params TValue[] children)
      : this(value, children?.Select(Node.CreateNodeWithIndexableChildren))
    {
    }

    public NodeWithIndexableChildren(
      TValue value,
      params NodeWithIndexableChildren<TValue>[] children)
    {
      Value = value;
      _Children = children ?? Array.Empty<NodeWithIndexableChildren<TValue>>();
    }

    private readonly NodeWithIndexableChildren<TValue>[] _Children;

    public IList<NodeWithIndexableChildren<TValue>> CopyChildren() => _Children.ToList();

    public TValue Value { get; }

    public NodeWithIndexableChildrenChildEnumerator<TValue> GetChildEnumerator()
    {
      return new NodeWithIndexableChildrenChildEnumerator<TValue>(_Children);
    }
  }
}
