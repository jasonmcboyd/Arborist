using Arborist.Treenumerables;
using System.Collections.Generic;

namespace Arborist.Nodes
{
  public struct NodeWithIndexableChildrenChildEnumerator<TValue>
  {
    public NodeWithIndexableChildrenChildEnumerator(IList<NodeWithIndexableChildren<TValue>> children)
    {
      _Children = children;
      _CurrentChildIndex = -1;
    }

    private IList<NodeWithIndexableChildren<TValue>> _Children;
    public int _CurrentChildIndex;

    public TryMoveNextChildResult<NodeWithIndexableChildren<TValue>> TryMoveNext()
    {
      if (_CurrentChildIndex < _Children.Count - 1)
      {
        _CurrentChildIndex++;
        return TryMoveNextChildResult.NextChild(_Children[_CurrentChildIndex], _CurrentChildIndex);
      }

      return TryMoveNextChildResult.NoNextChild<NodeWithIndexableChildren<TValue>>();
    }
  }
}
