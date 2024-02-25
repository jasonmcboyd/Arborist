using Arborist.Core;
using Arborist.Linq.Treenumerators;
using Arborist.Linq.Treenumerators.Memoize;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerables
{
  internal class MemoizeTreenumerable<TNode> : ITreenumerableBuffer<TNode>
  {
    public MemoizeTreenumerable(ITreenumerable<TNode> innerTreenumerable)
    {
      _InnerTreenumerable = innerTreenumerable;
      _BreadthFirstTreenumerator = innerTreenumerable.GetBreadthFirstTreenumerator();
      _DepthFirstTreenumerator = innerTreenumerable.GetDepthFirstTreenumerator();
    }

    private readonly ITreenumerable<TNode> _InnerTreenumerable;

    private readonly List<NodeVisit<TNode>> _BreadthFirstMemo = new List<NodeVisit<TNode>>();
    private readonly List<NodeVisit<TNode>> _DepthFirstMemo = new List<NodeVisit<TNode>>();

    private readonly ITreenumerator<TNode> _BreadthFirstTreenumerator;
    private readonly ITreenumerator<TNode> _DepthFirstTreenumerator;

    private bool _BreadthFirstTreenumeratorExhausted;
    private bool _DepthFirstTreenumeratorExhausted;

    private readonly object _BreadthFirstTreenumeratorLock = new object();
    private readonly object _DepthFirstTreenumeratorLock = new object();

    private bool _Disposed;

    public ITreenumerator<TNode> GetBreadthFirstTreenumerator()
      => new MemoizeTreenumerator<TNode>(GetNextBreadthFirst);

    public ITreenumerator<TNode> GetDepthFirstTreenumerator()
      => new MemoizeTreenumerator<TNode>(GetNextDepthFirst);

    private MoveNextResult<TNode> GetNext(
      int index,
      List<NodeVisit<TNode>> memo,
      object treenumeratorLock,
      ref bool exhaustedFlag)
    {
      throw new NotImplementedException();
      //if (_Disposed)
      //  throw new ObjectDisposedException("");

      //if (index > _BreadthFirstMemo.Count && !_BreadthFirstTreenumeratorExhausted)
      //{
      //  lock (_BreadthFirstTreenumeratorLock)
      //  {
      //    if (index > _BreadthFirstMemo.Count && !_BreadthFirstTreenumeratorExhausted)
      //    {
      //      if (_BreadthFirstTreenumerator.MoveNext(SchedulingStrategy.ScheduleForTraversal))
      //        _BreadthFirstMemo.Add(_BreadthFirstTreenumerator.Node);
      //      else
      //        _BreadthFirstTreenumeratorExhausted = true;
      //    }
      //  }
      //}

      //if (index < _BreadthFirstMemo.Count)
      //  return new MoveNextResult<TNode>(true, _BreadthFirstMemo[index]);

      //return new MoveNextResult<TNode>(false);
    }

    internal MoveNextResult<TNode> GetNextBreadthFirst(int index)
    {
      throw new NotImplementedException();
      //if (index > _BreadthFirstMemo.Count && !_BreadthFirstTreenumeratorExhausted)
      //{
      //  lock (_BreadthFirstTreenumeratorLock)
      //  {
      //    if (index > _BreadthFirstMemo.Count && !_BreadthFirstTreenumeratorExhausted)
      //    {
      //      if (_BreadthFirstTreenumerator.MoveNext(SchedulingStrategy.ScheduleForTraversal))
      //        _BreadthFirstMemo.Add(_BreadthFirstTreenumerator.Node);
      //      else
      //        _BreadthFirstTreenumeratorExhausted = true;
      //    }
      //  }
      //}

      //if (index < _BreadthFirstMemo.Count)
      //  return new MoveNextResult<TNode>(true, _BreadthFirstMemo[index]);

      //return new MoveNextResult<TNode>(false);
    }

    internal MoveNextResult<TNode> GetNextDepthFirst(int index)
    {
      throw new NotImplementedException();
      //if (index > _DepthFirstMemo.Count && !_DepthFirstTreenumeratorExhausted)
      //{
      //  lock (_DepthFirstTreenumeratorLock)
      //  {
      //    if (index > _DepthFirstMemo.Count && !_DepthFirstTreenumeratorExhausted)
      //    {
      //      if (_DepthFirstTreenumerator.MoveNext(SchedulingStrategy.ScheduleForTraversal))
      //        _DepthFirstMemo.Add(_DepthFirstTreenumerator.Node);
      //      else
      //        _DepthFirstTreenumeratorExhausted = true;
      //    }
      //  }
      //}

      //if (index < _DepthFirstMemo.Count)
      //  return new MoveNextResult<TNode>(true, _DepthFirstMemo[index]);

      //return new MoveNextResult<TNode>(false);
    }

    public void Dispose()
    {
      _BreadthFirstTreenumerator.Dispose();
      _DepthFirstTreenumerator.Dispose();
    }
  }
}
