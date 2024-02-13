using System;

namespace Arborist
{
  public interface ITreenumerator<TNode> : IDisposable
  {
    bool MoveNext(SchedulingStrategy schedulingStrategy);

    NodeVisit<TNode> Current { get; }
    int VisitCount { get; }
    TreenumeratorState State { get; }
    NodePosition OriginalPosition { get; }
    NodePosition Position { get; }
    SchedulingStrategy SchedulingStrategy { get; }
  }
}
