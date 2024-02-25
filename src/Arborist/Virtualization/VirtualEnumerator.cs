using Arborist.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Arborist.Virtualization
{
  internal class VirtualEnumerator<TNode>
    : IEnumerator<INodeContainerWithIndexableChildren<TNode>>
  {
    public VirtualEnumerator(VirtualEnumeratorPool<TNode> pool)
    {
      _Pool = pool;
    }

    private readonly VirtualEnumeratorPool<TNode> _Pool;

    public void SetInnerNode(INodeContainerWithIndexableChildren<TNode> innerNode) => _InnerNode = innerNode;

    private INodeContainerWithIndexableChildren<TNode> _InnerNode;

    private int _ChildIndex = -1;

    private INodeContainerWithIndexableChildren<TNode> _Current;
    public INodeContainerWithIndexableChildren<TNode> Current
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
