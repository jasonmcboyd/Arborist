using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist
{
  public abstract class StackTreenumeratorBase<TStack, TNode> : ITreenumerator<TNode>
  {
    public StackTreenumeratorBase(Func<TStack, TNode> selector)
    {
      _Selector = selector;
    }

    private readonly Func<TStack, TNode> _Selector;

    // TODO: I am thinking I might want to remove throwing and just
    // return default similar to IEnumerable. That would likely
    // requiring removing the EnumerationNotStarted and the
    // EnumerationFinished states.
    private void ValidateState()
    {
      if (State == TreenumeratorState.EnumerationNotStarted)
        throw new InvalidOperationException("Enumeration has not begun.");

      if (State == TreenumeratorState.EnumerationFinished)
        throw new InvalidOperationException("Enumeration has completed.");
    }

    private NodeVisit<TNode> _Current;
    public NodeVisit<TNode> Current
    {
      get
      {
        ValidateState();

        // Not sure why I implemented it this way.
        _Current = Stack.Last().WithNode(_Selector(Stack.Last().Node));

        return _Current;
      }
    }

    private int _VisitCount;
    public int VisitCount
    {
      get
      {
        ValidateState();

        return _VisitCount;
      }
      protected set => _VisitCount = value;
    }

    private NodePosition _OriginalPosition;
    public NodePosition OriginalPosition
    {
      get
      {
        ValidateState();

        return _OriginalPosition;
      }
      protected set => _OriginalPosition = value;
    }

    private NodePosition _Position;
    public NodePosition Position
    {
      get
      {
        ValidateState();

        return _Position;
      }
      protected set => _Position = value;
    }

    private SchedulingStrategy _SchedulingStrategy;
    public SchedulingStrategy SchedulingStrategy
    {
      get
      {
        ValidateState();

        return _SchedulingStrategy;
      }
      protected set => _SchedulingStrategy = value;
    }

    public TreenumeratorState State { get; protected set; } = TreenumeratorState.EnumerationNotStarted;

    protected List<NodeVisit<TStack>> Stack { get; } = new List<NodeVisit<TStack>>();

    public bool MoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (State == TreenumeratorState.EnumerationFinished)
        return false;

      OnMoveNext(schedulingStrategy);

      return Stack.Count > 0;
    }

    protected abstract void OnMoveNext(SchedulingStrategy schedulingStrategy);

    public abstract void Dispose();
  }

  public abstract class StackTreenumeratorBase<TNode> : StackTreenumeratorBase<TNode, TNode>
  {
    public StackTreenumeratorBase() : base(node => node)
    {
    }
  }
}
