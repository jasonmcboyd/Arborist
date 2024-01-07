using System;

namespace Arborist
{
  public interface ITreenumerator<TNode> : IDisposable
  {
    bool MoveNext(ChildStrategy childStrategy);
    NodeVisit<TNode> Current { get; }
    TreenumeratorState State { get; }
  }
}
