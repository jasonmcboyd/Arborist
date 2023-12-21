using Arborist.Linq;
using System;
using System.Collections.Generic;

namespace Arborist.Collatz
{
  public class CollatzTreenumerable : ITreenumerable<long>
  {
    public ITreenumerator<long> GetBreadthFirstTreenumerator()
      => new BreadthFirstTreenumerator();

    public ITreenumerator<long> GetDepthFirstTreenumerator()
    {
      throw new System.NotImplementedException();
    }

    private class DepthFirstTreenumerator : TreenumeratorBase<long>
    {
      private long _Previous = -1;

      protected override bool OnMoveNext(ChildStrategy childStrategy)
      {
        //if (_Previous == -1)
        //{
        //  if (skipChildren)
        //    return false;

        //  Current = NodeVisit.Create(1L, 1, 0, 0);

        //  _Previous = 0;

        //  return true;
        //}

        //if (_Previous < Current.Node)
        //{
        //  _Previous = Current.Node;

        //  if (skipChildren)
        //  {
        //    Current = Current.IncrementVisitCount();
        //    return true;
        //  }

        //  Current = NodeVisit.Create(checked(Current.Node << 1), 1, 0, Current.Depth + 1);
        //}
        //else if (_Previous == Current.Node)
        //{
        //  _Previous = Current.Node;

        //  if (Current.Node % 2 == 0)
        //  {
        //    Current = NodeVisit.Create(checked(Current.Node >> 1), 1, 1, Current.Depth - 1);
        //  }
        //}
        throw new NotImplementedException();
      }

      public override void Dispose()
      {
      }
    }

    private class BreadthFirstTreenumerator : TreenumeratorBase<long>
    {
      public BreadthFirstTreenumerator()
      {
        _CurrentLevel.Enqueue(NodeVisit.Create(1L, 1, 0, 0));
      }

      private int _Depth = 0;

      private Queue<NodeVisit<long>> _CurrentLevel = new Queue<NodeVisit<long>>();
      private Queue<NodeVisit<long>> _NextLevel = new Queue<NodeVisit<long>>();

      private readonly Queue<long> _Children = new Queue<long>();

      protected override bool OnMoveNext(ChildStrategy childStrategy)
      {
        throw new NotImplementedException();
        //if (_CurrentLevel.Count == 0
        //  && _Children.Count == 0)
        //{
        //  (_CurrentLevel, _NextLevel) = (_NextLevel, _CurrentLevel);
        //  _Depth++;
        //}

        //if (_CurrentLevel.Count == 0
        //  && _Children.Count == 0)
        //  return false;

        //if (_Children.Count > 0)
        //{
        //  if (childStrategy)
        //    _Children.Clear();
        //  else
        //  {
        //    var node = _Children.Dequeue();
        //    var siblingIndex = node % 2 == 0 ? 0 : 1;

        //    _NextLevel.Enqueue(NodeVisit.Create(node, 1, siblingIndex, _Depth + 1));
        //  }

        //  Current = Current.IncrementVisitCount();

        //  return true;
        //}

        //Current = _CurrentLevel.Dequeue();

        //_Children.Enqueue(checked(Current.Node << 1));

        //if (Current.Node > 4
        //  && (Current.Node - 1) % 3 == 0)
        //  _Children.Enqueue((Current.Node - 1) / 3);

        //return true;
      }

      public override void Dispose()
      {
      }
    }
  }
}