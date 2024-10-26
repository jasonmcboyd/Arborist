﻿namespace Arborist.Trees
{
  public struct NDecrementTreeNodeChildEnumerator
    : IChildEnumerator<int>
  {
    public NDecrementTreeNodeChildEnumerator(int depth)
    {
      _Depth = depth;
    }

    private int _Depth;

    public bool MoveNext(out NodeAndSiblingIndex<int> childNodeAndSiblingIndex)
    {
      if (_Depth == 0)
      {
        childNodeAndSiblingIndex = default;
        return false;
      }

      _Depth--;
      childNodeAndSiblingIndex = new NodeAndSiblingIndex<int>(_Depth, 0);
      return true;
    }

    public void Dispose()
    {
      // Do nothing.
    }
  }
}
