using System;

namespace Arborist.Core
{
  public interface ITreenumerator<TNode> : IDisposable
  {
    bool MoveNext(SchedulingStrategy schedulingStrategy);

    SchedulingStrategy SchedulingStrategy { get; }
    TNode Node { get; }
    int VisitCount { get; }
    TreenumeratorState State { get; }
    NodePosition OriginalPosition { get; }
    NodePosition Position { get; }
  }
}
