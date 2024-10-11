using System.Collections.Generic;
using System.Linq;

namespace Arborist.Nodes
{
  public sealed class NodeWithEnumerableChildren<TValue> : INode<TValue, NodeWithEnumerableChildrenChildEnumerator<TValue>>
  {
    public NodeWithEnumerableChildren(TValue value)
      : this(value, null)
    {
    }

    public NodeWithEnumerableChildren(
      TValue value,
      IEnumerable<NodeWithEnumerableChildren<TValue>> children)
    {
      Value = value;
      _Children = children ?? Enumerable.Empty<NodeWithEnumerableChildren<TValue>>();
    }

    public TValue Value { get; }

    private readonly IEnumerable<NodeWithEnumerableChildren<TValue>> _Children;

    public NodeWithEnumerableChildrenChildEnumerator<TValue> GetChildEnumerator()
    {
      return new NodeWithEnumerableChildrenChildEnumerator<TValue>(_Children);
    }
  }
}
