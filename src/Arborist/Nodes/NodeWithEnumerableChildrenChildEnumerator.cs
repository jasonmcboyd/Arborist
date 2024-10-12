using Arborist.Common;
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

    public bool TryMoveNext(out NodeAndSiblingIndex<NodeWithEnumerableChildren<TValue>> childNodeAndSiblingIndex)
    {
      if (_ChildEnumerator.MoveNext())
      {
        childNodeAndSiblingIndex = (_ChildEnumerator.Current, _ChildIndex++);
        return true;
      }

      childNodeAndSiblingIndex = default;
      return false;
    }

    public void Dispose()
    {
      _ChildEnumerator?.Dispose();
    }
  }
}
