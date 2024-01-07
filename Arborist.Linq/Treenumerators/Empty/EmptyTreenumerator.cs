﻿using System;

namespace Arborist.Linq.Treenumerators
{
  internal class EmptyTreenumerator<TNode> : ITreenumerator<TNode>
  {
    private EmptyTreenumerator()
    {
    }

    private static readonly EmptyTreenumerator<TNode> _Instance = new EmptyTreenumerator<TNode>();
    public static EmptyTreenumerator<TNode> Instance => _Instance;

    public NodeVisit<TNode> Current => throw new InvalidOperationException();

    public TreenumeratorState State => TreenumeratorState.EnumerationFinished;

    public bool MoveNext(ChildStrategy childStrategy) => false;

    public void Dispose()
    {
    }
  }
}
