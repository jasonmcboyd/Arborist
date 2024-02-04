using System;
using System.Collections;
using System.Collections.Generic;

namespace Arborist.Treenumerables
{
  internal class PooledNodeWithIndexableChildrenWrapper<TNode, TValue>
    : INodeWithEnumerableChildren<TValue>,
      IEnumerable<INodeWithEnumerableChildren<TValue>>,
      IEnumerator<INodeWithEnumerableChildren<TValue>>
    where TNode : INodeWithIndexableChildren<TNode, TValue>
  {
    public PooledNodeWithIndexableChildrenWrapper(PooledNodeWithIndexableChildrenWrapperPool<TNode, TValue> pool)
    {
      _Pool = pool;
    }

    private readonly PooledNodeWithIndexableChildrenWrapperPool<TNode, TValue> _Pool;

    public void SetInnerNode(TNode innerNode) => _InnerNode = innerNode;

    private TNode _InnerNode;

    public TValue Value => _InnerNode.Value;

    private int _ChildIndex = -1;

    public IEnumerable<INodeWithEnumerableChildren<TValue>> Children => this;

    private INodeWithEnumerableChildren<TValue> _Current;
    public INodeWithEnumerableChildren<TValue> Current
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

    public IEnumerator<INodeWithEnumerableChildren<TValue>> GetEnumerator() => this;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool MoveNext()
    {
      if (_ChildIndex == _InnerNode.ChildCount)
        return false;

      _ChildIndex++;

      var result = _ChildIndex < _InnerNode.ChildCount;

      if (result)
        _Current = _Pool.Get(_InnerNode[_ChildIndex]);

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
