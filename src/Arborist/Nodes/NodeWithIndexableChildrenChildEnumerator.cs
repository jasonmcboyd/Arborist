using Arborist.Common;
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

    public bool TryMoveNext(out NodeAndSiblingIndex<NodeWithIndexableChildren<TValue>> childNodeAndSiblingIndex)
    {
      if (_CurrentChildIndex < _Children.Count - 1)
      {
        _CurrentChildIndex++;
        
        childNodeAndSiblingIndex = (_Children[_CurrentChildIndex], _CurrentChildIndex);

        return true;
      }

      childNodeAndSiblingIndex = default;
      return false;
    }
  }
}
