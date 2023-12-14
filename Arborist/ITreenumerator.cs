using System;

namespace Arborist
{
  public interface ITreenumerator<TNode> : IDisposable
  {
    bool MoveNext(bool skipChildren);
    NodeVisit<TNode> Current { get; }
  }
}
