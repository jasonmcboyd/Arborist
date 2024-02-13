using System;

namespace Arborist.Linq.Treenumerators
{
  internal class EmptyTreenumerator<TNode> : ITreenumerator<TNode>
  {
    public EmptyTreenumerator()
    {
    }

    // TODO: I am just going to return defaults rather than throwing for now.
    public NodeVisit<TNode> Current => default;
    public int VisitCount => default;
    public NodePosition OriginalPosition => default;
    public NodePosition Position => default;
    public SchedulingStrategy SchedulingStrategy => default;

    public TreenumeratorState State { get; private set; } = TreenumeratorState.EnumerationNotStarted;


    public bool MoveNext(SchedulingStrategy schedulingStrategy)
    {
      State = TreenumeratorState.EnumerationFinished;

      return false;
    }

    public void Dispose()
    {
    }
  }
}
