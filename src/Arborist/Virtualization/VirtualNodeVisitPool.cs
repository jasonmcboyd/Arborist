using Arborist.Core;
using System.Collections.Generic;
using System.Threading;

namespace Arborist.Virtualization
{
  internal class VirtualNodeVisitPool<TNode>
  {
    private readonly Stack<VirtualNodeVisit<TNode>> _Stack = new Stack<VirtualNodeVisit<TNode>>();

    private SpinLock _Lock = new SpinLock();

    private int _Leased;
    public int Leased
    {
      get
      {
        bool lockTaken = false;

        _Lock.Enter(ref lockTaken);

        try
        {
          return _Leased;
        }
        finally
        {
          if (lockTaken)
            _Lock.Exit();
        }
      }
      set
      {
        bool lockTaken = false;

        _Lock.Enter(ref lockTaken);

        try
        {
          _Leased = value;
        }
        finally
        {
          if (lockTaken)
            _Lock.Exit();
        }
      }
    }

    public int Available
    {
      get
      {
        bool lockTaken = false;

        _Lock.Enter(ref lockTaken);

        try
        {
          return _Stack.Count;
        }
        finally
        {
          if (lockTaken)
            _Lock.Exit();
        }
      }
    }

    public int Count
    {
      get
      {
        bool lockTaken = false;

        _Lock.Enter(ref lockTaken);

        try
        {
          return _Stack.Count + _Leased;
        }
        finally
        {
          if (lockTaken)
            _Lock.Exit();
        }
      }
    }

    public VirtualNodeVisit<TNode> Lease(
      TreenumeratorMode mode,
      TNode node,
      int visitCount,
      NodePosition position,
      NodeTraversalStrategy nodeTraversalStrategy)
    {
      VirtualNodeVisit<TNode> result;

      bool lockTaken = false;

      _Lock.Enter(ref lockTaken);
      try
      {
        result =
          _Stack.Count > 0
          ? _Stack.Pop()
          : new VirtualNodeVisit<TNode>();

        _Leased++;
      }
      finally
      {
        if (lockTaken)
          _Lock.Exit();
      }

      result.Mode = mode;
      result.Node = node;
      result.VisitCount = visitCount;
      result.Position = position;
      result.TraversalStrategy = nodeTraversalStrategy;

      return result;
    }

    public void Return(VirtualNodeVisit<TNode> virtualNodeVisit)
    {
      virtualNodeVisit.Node = default;

      bool lockTaken = false;

      _Lock.Enter(ref lockTaken);
      
      try
      {
        _Stack.Push(virtualNodeVisit);
        _Leased--;
      }
      finally
      {
        if (lockTaken)
          _Lock.Exit();
      }
    }
  }
}
