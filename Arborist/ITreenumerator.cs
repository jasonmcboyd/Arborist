using System;

namespace Arborist
{
  public interface ITreenumerator<TNode> : IDisposable
  {
    bool MoveNext(SchedulingStrategy schedulingStrategy);
    NodeVisit<TNode> Current { get; }
    TreenumeratorState State { get; }
    NodePosition OriginalPosition { get; }
    NodePosition Position { get; }
    // TODO: I think I want to add Skipped
  }
}
