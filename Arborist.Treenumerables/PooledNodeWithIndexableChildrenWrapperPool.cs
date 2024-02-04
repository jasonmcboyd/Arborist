using System.Collections.Generic;

namespace Arborist.Treenumerables
{
  internal class PooledNodeWithIndexableChildrenWrapperPool<TNode, TValue>
    where TNode : INodeWithIndexableChildren<TNode, TValue>
  {
    private readonly Stack<PooledNodeWithIndexableChildrenWrapper<TNode, TValue>> _Stack = new Stack<PooledNodeWithIndexableChildrenWrapper<TNode, TValue>>();

    private readonly object _Lock = new object();

    public int Leased { get; private set; }
    public int Available
    {
      get
      {
        lock(_Lock)
        {
          return _Stack.Count;
        }
      }
    }
    public int Count
    {
      get
      {
        lock (_Lock)
        {
          return _Stack.Count + Leased;
        }
      }
    }

    public PooledNodeWithIndexableChildrenWrapper<TNode, TValue> Get(TNode node)
    {
      PooledNodeWithIndexableChildrenWrapper<TNode, TValue> result;

      lock (_Lock)
      {
        result =
          _Stack.Count > 0
          ? _Stack.Pop()
          : new PooledNodeWithIndexableChildrenWrapper<TNode, TValue>(this);

        Leased++;
      }

      result.SetInnerNode(node);

      return result;
    }

    public void Return(PooledNodeWithIndexableChildrenWrapper<TNode, TValue> wrapper)
    {
      lock (_Lock)
      {
        _Stack.Push(wrapper);
        Leased--;
      }
    }
  }
}
