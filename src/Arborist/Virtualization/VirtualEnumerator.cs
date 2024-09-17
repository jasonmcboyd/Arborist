using Arborist.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Arborist.Virtualization
{
  internal class VirtualEnumerator<TValue, TNode>
    : IEnumerator<TNode>
    where TNode : INodeWithIndexableChildren<TValue, TNode>
  {
    public VirtualEnumerator(VirtualEnumeratorPool<TValue, TNode> pool)
    {
      _Pool = pool;
    }

    private readonly VirtualEnumeratorPool<TValue, TNode> _Pool;

    public void SetInnerNode(TNode innerNode) => _InnerNode = innerNode;

    private TNode _InnerNode;

    private int _ChildIndex = -1;

    private TNode _Current;
    public TNode Current
    {
      get
      {
        if (_ChildIndex == -1)
          throw new InvalidOperationException("Enumeration has not begun.");

        if (_ChildIndex > _InnerNode.ChildCount)
          throw new InvalidOperationException("Enumeration has completed.");

        return _Current;
      }
    }

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
      if (_ChildIndex == _InnerNode.ChildCount)
        return false;

      _ChildIndex++;

      var result = _ChildIndex < _InnerNode.ChildCount;

      if (result)
        _Current = _InnerNode[_ChildIndex];

      return result;
    }

    public void Reset()
    {
      _ChildIndex = -1;
      _InnerNode = default;
      _Disposed = false;
    }

    private bool _Disposed;
    public void Dispose()
    {
      if (!_Disposed)
      {
        _Disposed = true;
        Reset();
        _Pool.Return(this);
      }
    }
  }
}
