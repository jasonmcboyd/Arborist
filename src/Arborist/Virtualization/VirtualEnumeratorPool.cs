using Arborist.Nodes;
using System.Collections.Generic;

namespace Arborist.Virtualization
{
  internal class VirtualEnumeratorPool<TValue, TNode>
    where TNode : INodeWithIndexableChildren<TValue, TNode>
  {
    private readonly Stack<VirtualEnumerator<TValue, TNode>> _Stack =
      new Stack<VirtualEnumerator<TValue, TNode>>();

    private readonly object _Lock = new object();

    public int Leased { get; private set; }
    public int Available
    {
      get
      {
        lock (_Lock)
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

    public IEnumerator<TNode> Lease(TNode node)
    {
      VirtualEnumerator<TValue, TNode> result;

      lock (_Lock)
      {
        result =
          _Stack.Count > 0
          ? _Stack.Pop()
          : new VirtualEnumerator<TValue, TNode>(this);

        Leased++;
      }

      result.SetInnerNode(node);

      return result;
    }

    public void Return(VirtualEnumerator<TValue, TNode> wrapper)
    {
      lock (_Lock)
      {
        _Stack.Push(wrapper);
        Leased--;
      }
    }
  }
}
