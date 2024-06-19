using Arborist.Nodes;
using System.Collections.Generic;

namespace Arborist.Virtualization
{
  internal class VirtualEnumeratorPool<TNode>
  {
    private readonly Stack<VirtualEnumerator<TNode>> _Stack =
      new Stack<VirtualEnumerator<TNode>>();

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

    public IEnumerator<INodeWithIndexableChildren<TNode>> Lease(INodeWithIndexableChildren<TNode> node)
    {
      VirtualEnumerator<TNode> result;

      lock (_Lock)
      {
        result =
          _Stack.Count > 0
          ? _Stack.Pop()
          : new VirtualEnumerator<TNode>(this);

        Leased++;
      }

      result.SetInnerNode(node);

      return result;
    }

    public void Return(VirtualEnumerator<TNode> wrapper)
    {
      lock (_Lock)
      {
        _Stack.Push(wrapper);
        Leased--;
      }
    }
  }
}
