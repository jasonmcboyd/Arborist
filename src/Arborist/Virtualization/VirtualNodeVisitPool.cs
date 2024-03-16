using Arborist.Core;
using Arborist.Virtualization;
using System.Collections.Generic;

namespace Arborist.Virtualization
{
  internal class VirtualNodeVisitPool<TNode>
  {
    private readonly Stack<VirtualNodeVisit<TNode>> _Stack = new Stack<VirtualNodeVisit<TNode>>();

    private readonly object _Lock = new object();

    private int _Leased;
    public int Leased
    {
      get
      {
        lock (_Lock)
        {
          return _Leased;
        }
      }
      set
      {
        lock (_Lock)
        {
          {
            _Leased = value;
          }
        }
      }
    }

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

    public VirtualNodeVisit<TNode> Lease(ITreenumerator<TNode> treenumerator)
      => Lease(
        treenumerator.Mode,
        treenumerator.Node,
        treenumerator.VisitCount,
        treenumerator.OriginalPosition,
        treenumerator.Position,
        treenumerator.SchedulingStrategy);

    public VirtualNodeVisit<TNode> Lease(
      TreenumeratorMode mode,
      TNode node,
      int visitCount,
      NodePosition originalPosition,
      NodePosition position,
      SchedulingStrategy schedulingStrategy)
    {
      VirtualNodeVisit<TNode> result;

      lock (_Lock)
      {
        result =
          _Stack.Count > 0
          ? _Stack.Pop()
          : new VirtualNodeVisit<TNode>();

        _Leased++;
      }

      result.Mode = mode;
      result.Node = node;
      result.VisitCount = visitCount;
      result.OriginalPosition = originalPosition;
      result.Position = position;
      result.SchedulingStrategy = schedulingStrategy;

      return result;
    }

    public void Return(VirtualNodeVisit<TNode> virtualNodeVisit)
    {
      virtualNodeVisit.Node = default;

      lock (_Lock)
      {
        _Stack.Push(virtualNodeVisit);
        _Leased--;
      }
    }
  }
}
