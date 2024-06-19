using System.Collections.Generic;
using System.Linq;

namespace Arborist.Nodes
{
  public sealed class NodeWithEnumerableChildren<TValue>
    : INodeWithEnumerableChildren<TValue>
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
      Children = children ?? Enumerable.Empty<NodeWithEnumerableChildren<TValue>>();
    }

    public TValue Value { get; }

    public IEnumerable<INodeWithEnumerableChildren<TValue>> Children { get; private set; }
  }
}
