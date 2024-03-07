using System;

namespace Arborist.Core
{
  public abstract class TreenumeratorBase<TNode> : ITreenumerator<TNode>
  {
    // TODO: I am thinking I might want to remove throwing and just
    // return default similar to IEnumerable. That would likely
    // requiring removing the EnumerationNotStarted and the
    // EnumerationFinished states.
    private void ValidateState()
    {
      if (State == TreenumeratorState.EnumerationNotStarted)
        throw new InvalidOperationException("Enumeration has not begun.");

      // TODO:
      //if (State == TreenumeratorState.EnumerationFinished)
      //  throw new InvalidOperationException("Enumeration has completed.");
    }

    private TNode _Node;
    public TNode Node
    {
      get
      {
        ValidateState();

        return _Node;
      }
      protected set => _Node = value;
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

    public abstract void Dispose();

    public bool MoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (State == TreenumeratorState.EnumerationFinished)
        return false;

      if (OnMoveNext(schedulingStrategy))
        return true;

      State = TreenumeratorState.EnumerationFinished;

      return false;
    }

    protected abstract bool OnMoveNext(SchedulingStrategy schedulingStrategy);
  }
}
