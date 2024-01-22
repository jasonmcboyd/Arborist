using System;

namespace Arborist
{
  public abstract class TreenumeratorBase<TNode> : ITreenumerator<TNode>
  {
    private NodeVisit<TNode> _Current;
    public NodeVisit<TNode> Current
    {
      get
      {
        if (State == TreenumeratorState.EnumerationNotStarted)
          throw new InvalidOperationException("Enumeration has not begun.");

        if (State == TreenumeratorState.EnumerationFinished)
          throw new InvalidOperationException("Enumeration has completed.");

        return _Current;
      }
      protected set => _Current = value;
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
