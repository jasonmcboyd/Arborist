using System;

namespace Arborist.Core
{
  public interface ITreenumerator<TNode> : IDisposable
  {
    bool MoveNext(NodeTraversalStrategy nodeTraversalStrategy);

    TNode Node { get; }
    int VisitCount { get; }
    TreenumeratorMode Mode { get; }
    NodePosition Position { get; }
  }
}
