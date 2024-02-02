using System;
using System.Collections.Generic;

namespace Arborist.Treenumerables.Treenumerators
{
  internal sealed class IndexableDepthFirstTreenumerator<TNode, TValue>
    : TreenumeratorBase<TValue>
    where TNode : INodeWithIndexableChildren<TNode, TValue>
  {
    public IndexableDepthFirstTreenumerator(IEnumerable<TNode> roots)
    {
      _RootsEnumerator = roots.GetEnumerator();
    }

    private readonly IEnumerator<TNode> _RootsEnumerator;

    private int _RootsEnumeratorIndex = -1;

    private readonly Stack<IndexableTreenumeratorNodeVisit<TNode, TValue>> _Stack = new Stack<IndexableTreenumeratorNodeVisit<TNode, TValue>>();

    public int VisitCount
    {
      get
      {
        if (State == TreenumeratorState.EnumerationNotStarted)
          throw new InvalidOperationException("Enumeration has not begun.");

        if (State == TreenumeratorState.EnumerationFinished)
          throw new InvalidOperationException("Enumeration has completed.");

        return _Stack.Peek().VisitCount;
      }
    }

    public int Depth
    {
      get
      {
        if (State == TreenumeratorState.EnumerationNotStarted)
          throw new InvalidOperationException("Enumeration has not begun.");

        if (State == TreenumeratorState.EnumerationFinished)
          throw new InvalidOperationException("Enumeration has completed.");

        return _Stack.Peek().Depth;
      }
    }

    public int SiblingIndex
    {
      get
      {
        if (State == TreenumeratorState.EnumerationNotStarted)
          throw new InvalidOperationException("Enumeration has not begun.");

        if (State == TreenumeratorState.EnumerationFinished)
          throw new InvalidOperationException("Enumeration has completed.");

        return _Stack.Peek().SiblingIndex;
      }
    }

    public bool Skipped
    {
      get
      {
        if (State == TreenumeratorState.EnumerationNotStarted)
          throw new InvalidOperationException("Enumeration has not begun.");

        if (State == TreenumeratorState.EnumerationFinished)
          throw new InvalidOperationException("Enumeration has completed.");

        return _Stack.Peek().Skipped;
      }
    }

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      while (true)
      {
        if (_Stack.Count == 0)
          return OnEmptyStack();

        if (State == TreenumeratorState.SchedulingNode)
        {
          var onScheduling = OnScheduling(schedulingStrategy);

          if (onScheduling != null)
            return onScheduling.Value;
        }

        if (_Stack.Count == 0)
          continue;

        var onVisiting = OnVisiting();

        if (onVisiting != null)
          return onVisiting.Value;
      }
    }

    private bool OnEmptyStack()
    {
      if (_RootsEnumerator.MoveNext())
      {
        _RootsEnumeratorIndex++;

        var nextVisit =
          IndexableTreenumeratorNodeVisit
          .Create<TNode, TValue>(
            _RootsEnumerator.Current,
            0,
            _RootsEnumeratorIndex,
            0,
            0,
            false);

        _Stack.Push(nextVisit);
        Current = nextVisit.ToNodeVisit();
        State = TreenumeratorState.SchedulingNode;

        return true;
      }

      State = TreenumeratorState.EnumerationFinished;

      return false;
    }

    private bool? OnScheduling(SchedulingStrategy schedulingStrategy)
    {
      var previousVisit = _Stack.Pop();

      switch (schedulingStrategy)
      {
        case SchedulingStrategy.SkipDescendantSubtrees:
          previousVisit = previousVisit.With(1, null, null, previousVisit.Node.ChildCount, true);
          break;
        case SchedulingStrategy.SkipSubtree:
          return null;
        case SchedulingStrategy.SkipNode:
          previousVisit = previousVisit.With(1, null, null, null, true);
          break;
        default:
          previousVisit = previousVisit.IncrementVisitCount();
          break;
      }

      _Stack.Push(previousVisit);
      Current = previousVisit.ToNodeVisit();

      State = TreenumeratorState.VisitingNode;

      return true;
    }

    private bool? OnVisiting()
    {
      var previousVisit = _Stack.Pop();

      if (previousVisit.VisitCount == 0
        || (previousVisit.VisitCount == 1 && !previousVisit.HasNextChild())
        || State == TreenumeratorState.SchedulingNode)
      {
        previousVisit = previousVisit.IncrementVisitCount();
        Current = previousVisit.ToNodeVisit();
        _Stack.Push(previousVisit);
        State = TreenumeratorState.VisitingNode;
        return true;
      }

      if (previousVisit.HasNextChild())
      {
        var nextVisit = previousVisit.GetNextChildVisit();
        previousVisit = previousVisit.IncrementVisitedChildrenCount();
        _Stack.Push(previousVisit);
        _Stack.Push(nextVisit);

        Current = nextVisit.ToNodeVisit();

        State = TreenumeratorState.SchedulingNode;
        return true;
      }

      if (_Stack.Count > 0)
      {
        previousVisit = _Stack.Pop();

        previousVisit = previousVisit.IncrementVisitCount();
        _Stack.Push(previousVisit);

        Current = previousVisit.ToNodeVisit();

        State = TreenumeratorState.VisitingNode;

        return true;
      }

      return null;
    }

    public override void Dispose()
    {
      _RootsEnumerator?.Dispose();
    }
  }
}
