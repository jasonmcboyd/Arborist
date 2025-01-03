﻿using System;

namespace Arborist
{
  public struct SimpleNodeChildEnumerator<TValue> : IChildEnumerator<SimpleNode<TValue>>
  {
    public SimpleNodeChildEnumerator(SimpleNode<TValue>[] children)
    {
      _ChildIndex = -1;
      _Children = children ?? Array.Empty<SimpleNode<TValue>>();
      _Disposed = false;
    }

    private int _ChildIndex;
    private SimpleNode<TValue>[] _Children;

    public NodeAndSiblingIndex<SimpleNode<TValue>> CurrentChild =>
      _ChildIndex == -1
      ? default
      : new NodeAndSiblingIndex<SimpleNode<TValue>>(_Children[_ChildIndex], _ChildIndex);

    public bool MoveNext(out NodeAndSiblingIndex<SimpleNode<TValue>> childNodeAndSiblingIndex)
    {
      if (_Disposed || _ChildIndex + 1 >= _Children.Length)
      {
        childNodeAndSiblingIndex = default;
        return false;
      }

      _ChildIndex++;
      childNodeAndSiblingIndex = new NodeAndSiblingIndex<SimpleNode<TValue>>(_Children[_ChildIndex], _ChildIndex);
      return true;
    }

    private bool _Disposed;

    public void Dispose()
    {
      _Disposed = true;
    }
  }
}
