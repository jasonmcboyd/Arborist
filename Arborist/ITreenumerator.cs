using System;

namespace Arborist
{
  public interface ITreenumerator<TNode> : IDisposable
  {
    bool MoveNext(SchedulingStrategy schedulingStrategy);
    NodeVisit<TNode> Current { get; }
    TreenumeratorState State { get; }
  }
}
