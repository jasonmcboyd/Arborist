using System;

namespace Arborist
{
  public struct SimpleNodeChildEnumerator<TValue>
  {
    public SimpleNodeChildEnumerator(SimpleNode<TValue>[] children)
    {
      _ChildIndex = -1;
      _Children = children ?? Array.Empty<SimpleNode<TValue>>();
    }

    private int _ChildIndex;
    private SimpleNode<TValue>[] _Children;

    public NodeAndSiblingIndex<SimpleNode<TValue>> CurrentChild =>
      _ChildIndex == -1
      ? default
      : new NodeAndSiblingIndex<SimpleNode<TValue>>(_Children[_ChildIndex], _ChildIndex);

    public bool MoveNext()
    {
      if (_ChildIndex + 1 >= _Children.Length)
        return false;

      _ChildIndex++;

      return true;
    }
  }
}
