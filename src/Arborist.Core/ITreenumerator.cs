using System;

namespace Arborist.Core
{
  public interface ITreenumerator<TNode> : IDisposable
  {
    bool MoveNext(SchedulingStrategy schedulingStrategy);

    TNode Node { get; }
    int VisitCount { get; }
    TreenumeratorMode Mode { get; }
    NodePosition OriginalPosition { get; }
    NodePosition Position { get; }
  }
}
