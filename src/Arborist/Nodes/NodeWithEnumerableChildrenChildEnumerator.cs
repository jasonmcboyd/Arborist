using Arborist.Treenumerables;
using System.Collections.Generic;

namespace Arborist.Nodes
{
  public struct NodeWithEnumerableChildrenChildEnumerator<TValue>
  {
    public NodeWithEnumerableChildrenChildEnumerator(IEnumerable<NodeWithEnumerableChildren<TValue>> children)
    {
      _ChildEnumerator = children.GetEnumerator();
      _ChildIndex = 0;
    }

    private readonly IEnumerator<NodeWithEnumerableChildren<TValue>> _ChildEnumerator;
    private int _ChildIndex;

    public TryMoveNextChildResult<NodeWithEnumerableChildren<TValue>> TryMoveNext()
    {
      if (_ChildEnumerator.MoveNext())
        return TryMoveNextChildResult.NextChild(_ChildEnumerator.Current, _ChildIndex++);

      return TryMoveNextChildResult.NoNextChild<NodeWithEnumerableChildren<TValue>>();
    }

    public void Dispose()
    {
      _ChildEnumerator?.Dispose();
    }
  }
}
