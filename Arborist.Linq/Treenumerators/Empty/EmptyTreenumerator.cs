using System;

namespace Arborist.Linq.Treenumerators
{
  internal class EmptyTreenumerator<TNode> : ITreenumerator<TNode>
  {
    public EmptyTreenumerator()
    {
    }

    public NodeVisit<TNode> Current => throw new InvalidOperationException();

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
