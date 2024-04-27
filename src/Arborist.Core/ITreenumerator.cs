using System;

namespace Arborist.Core
{
  public interface ITreenumerator<TNode> : IDisposable
  {
    bool MoveNext(TraversalStrategy traversalStrategy);

    TNode Node { get; }
    int VisitCount { get; }
    TreenumeratorMode Mode { get; }
    NodePosition OriginalPosition { get; }
  }
}
