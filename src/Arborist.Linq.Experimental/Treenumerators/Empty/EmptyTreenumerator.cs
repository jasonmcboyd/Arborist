﻿using Arborist.Core;
using System;

namespace Arborist.Linq.Treenumerators
{
  internal class EmptyTreenumerator<TNode> : ITreenumerator<TNode>
  {
    public EmptyTreenumerator()
    {
    }

    // TODO: I am just going to return defaults rather than throwing for now.
    public TNode Node => default;
    public int VisitCount => default;
    public NodePosition OriginalPosition => default;
    public NodePosition Position => default;
    public TraversalStrategy TraversalStrategy => default;

    public TreenumeratorMode Mode { get; private set; } = TreenumeratorMode.EnumerationNotStarted;

    public bool MoveNext(TraversalStrategy traversalStrategy)
    {
      Mode = TreenumeratorMode.EnumerationFinished;

      return false;
    }

    public void Dispose()
    {
    }
  }
}
