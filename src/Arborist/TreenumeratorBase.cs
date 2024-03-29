﻿using Arborist.Core;
using System;

namespace Arborist
{
  public abstract class TreenumeratorBase<TNode> : ITreenumerator<TNode>
  {
    // TODO: I am thinking I might want to remove throwing and just
    // return default similar to IEnumerable. That would likely
    // requiring removing the EnumerationNotStarted and the
    // EnumerationFinished modes.
    private void ValidateState()
    {
      if (Mode == TreenumeratorMode.EnumerationNotStarted)
        throw new InvalidOperationException("Enumeration has not begun.");

      // TODO:
      //if (Mode == TreenumeratorMode.EnumerationFinished)
      //  throw new InvalidOperationException("Enumeration has completed.");
    }

    private TNode _Node;
    public TNode Node
    {
      get
      {
        ValidateState();

        return _Node;
      }
      protected set => _Node = value;
    }

    private int _VisitCount;
    public int VisitCount
    {
      get
      {
        ValidateState();

        return _VisitCount;
      }
      protected set => _VisitCount = value;
    }

    private NodePosition _OriginalPosition;
    public NodePosition OriginalPosition
    {
      get
      {
        ValidateState();

        return _OriginalPosition;
      }
      protected set => _OriginalPosition = value;
    }

    private NodePosition _Position;
    public NodePosition Position
    {
      get
      {
        ValidateState();

        return _Position;
      }
      protected set => _Position = value;
    }

    private TraversalStrategy _TraversalStrategy;
    public TraversalStrategy TraversalStrategy
    {
      get
      {
        ValidateState();

        return _TraversalStrategy;
      }
      protected set => _TraversalStrategy = value;
    }

    public TreenumeratorMode Mode { get; protected set; } = TreenumeratorMode.EnumerationNotStarted;

    public abstract void Dispose();

    public bool MoveNext(TraversalStrategy traversalStrategy)
    {
      if (Mode == TreenumeratorMode.EnumerationFinished)
        return false;

      if (OnMoveNext(traversalStrategy))
        return true;

      Mode = TreenumeratorMode.EnumerationFinished;

      return false;
    }

    protected abstract bool OnMoveNext(TraversalStrategy traversalStrategy);
  }
}
