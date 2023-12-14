using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Treenumerables.Treenumerators
{
  internal sealed class IndexableBreadthFirstTreenumerator<TIndexableNode, TIndexableNodeValue>
    : TreenumeratorBase<TIndexableNodeValue>
    where TIndexableNode : INodeWithIndexableChildren<TIndexableNode, TIndexableNodeValue>
  {
    public IndexableBreadthFirstTreenumerator(IEnumerable<TIndexableNode> roots)
    {
      var enumerator =
        roots
        .Select((node, index) => NodeVisit.Create(node, 1, index, 0))
        .GetEnumerator();

      _CurrentSemiQueue = new EnumeratorSemiQueue<NodeVisit<TIndexableNode>>(enumerator);
    }

    private readonly IEnumerator<TIndexableNode> _RootsEnumerator;

    private ISemiQueue<NodeVisit<TIndexableNode>> _CurrentSemiQueue { get; set; }

    private readonly QueueSemiQueue<NodeVisit<TIndexableNode>> _QueueSemiQueue = new QueueSemiQueue<NodeVisit<TIndexableNode>>();

    private Queue<NodeVisit<TIndexableNode>> _CurrentLevel = new Queue<NodeVisit<TIndexableNode>>();
    private Queue<NodeVisit<TIndexableNode>> _NextLevel = new Queue<NodeVisit<TIndexableNode>>();

    private int _Depth = 0;

    private bool _StartedEnumeration = false;

    protected override bool OnMoveNext(bool skipChildren)
    {
      if (!_StartedEnumeration)
      {
        _StartedEnumeration = true;

        if (skipChildren || _CurrentSemiQueue.IsEmpty)
          return false;

        Current = _CurrentSemiQueue.Peek().WithNode(_CurrentSemiQueue.Peek().Node.Value);

        return true;
      }

      if (_CurrentSemiQueue.IsEmpty)
      {
        (_CurrentLevel, _NextLevel) = (_NextLevel, _CurrentLevel);
        _QueueSemiQueue.Queue = _CurrentLevel;
        _CurrentSemiQueue = _QueueSemiQueue;
        _Depth++;

        if (_CurrentSemiQueue.IsEmpty)
          return false;

        Current = _CurrentSemiQueue.Peek().WithNode(_QueueSemiQueue.Peek().Node.Value);

        return true;
      }

      var currentQueued = _CurrentSemiQueue.Peek();

      var currentQueuedDequeued = false;

      if (currentQueued.SiblingIndex != Current.SiblingIndex || currentQueued.Depth != Current.Depth)
      {
        Current = currentQueued.WithNode(currentQueued.Node.Value);

        return true;
      }

      if (currentQueued.Node.ChildCount >= Current.VisitCount)
      {
        var childIndex = Current.VisitCount - 1;

        if (skipChildren)
        {
          _CurrentSemiQueue.Dequeue();
          currentQueuedDequeued = true;
        }
        else
        {
          var childNode = currentQueued.Node[childIndex];

          _NextLevel.Enqueue(NodeVisit.Create(childNode, 1, childIndex, _Depth + 1));
        }
      }

      Current = Current.IncrementVisitCount();

      if (!currentQueuedDequeued && Current.VisitCount > currentQueued.Node.ChildCount)
        _CurrentSemiQueue.Dequeue();

      return true;
    }

    public override void Dispose()
    {
      _RootsEnumerator?.Dispose();
    }

    private interface ISemiQueue<T> : IDisposable
    {
      T Dequeue();
      T Peek();
      bool IsEmpty { get; }
    }

    private class EnumeratorSemiQueue<T> : ISemiQueue<T>
    {
      public EnumeratorSemiQueue(IEnumerator<T> enumerator)
      {
        _Enumerator = enumerator;

        IsEmpty = !_Enumerator.MoveNext();
      }

      private readonly IEnumerator<T> _Enumerator;

      public T Dequeue()
      {
        if (IsEmpty)
          throw new InvalidOperationException("The queue is empty.");

        var current = _Enumerator.Current;

        IsEmpty = !_Enumerator.MoveNext();

        return current;
      }

      public T Peek()
      {
        if (IsEmpty)
          throw new InvalidOperationException("The queue is empty.");

        return _Enumerator.Current;
      }

      public bool IsEmpty { get; private set; }

      public void Dispose() => _Enumerator?.Dispose();
    }
  
    private class QueueSemiQueue<T> : ISemiQueue<T>
    {
      public QueueSemiQueue()
      {
      }

      public Queue<T> Queue { get; set; }

      public T Dequeue() => Queue.Dequeue();

      public T Peek() => Queue.Peek();

      public bool IsEmpty => Queue.Count == 0;

      public void Dispose() { }
    }
  }
}
