﻿using Arborist.Core;

namespace Arborist.Linq
{
  internal class EmptyTreenumerator<TNode> : ITreenumerator<TNode>
  {
    private EmptyTreenumerator()
    {
    }

    public static EmptyTreenumerator<TNode> Instance { get; } = new EmptyTreenumerator<TNode>();

    public TNode Node { get; } = default;
    public int VisitCount => default;
    public NodePosition Position => (0, -1);
    public TraversalStrategy TraversalStrategy => default;
    public TreenumeratorMode Mode => default;

    public bool MoveNext(TraversalStrategy traversalStrategy) => false;

    public void Dispose()
    {
    }
  }
}
