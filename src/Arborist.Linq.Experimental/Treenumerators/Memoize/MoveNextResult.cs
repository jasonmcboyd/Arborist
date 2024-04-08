using Arborist.Core;
using System;

namespace Arborist.Linq.Experimental.Treenumerators
{
  internal struct MoveNextResult<TNode>
  {
    public MoveNextResult(bool moveNext, NodeVisit<TNode> current)
    {
      HadNext = moveNext;
      _Current = current;
    }

    public MoveNextResult(bool moveNext)
    {
      HadNext = moveNext;
      _Current = default;
    }

    public bool HadNext { get; }

    private readonly NodeVisit<TNode> _Current;
    public NodeVisit<TNode> Current
      => HadNext ? _Current : throw new InvalidOperationException();
  }
}
