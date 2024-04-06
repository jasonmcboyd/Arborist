﻿using Arborist.Core;
using Arborist.Linq.Extensions;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class PruneBeforeDepthFirstTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public PruneBeforeDepthFirstTreenumerator(
      ITreenumerator<TNode> innerTreenumerator,
      Func<NodeVisit<TNode>, bool> predicate)
      : base(innerTreenumerator)
    {
      _Predicate = predicate;
    }

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;

    private List<int> _SkippedSiblingsCounts = new List<int>();

    protected override bool OnMoveNext(TraversalStrategy traversalStrategy)
    {
      if (Mode == TreenumeratorMode.EnumerationFinished)
        return false;

      if (Mode == TreenumeratorMode.VisitingNode)
        traversalStrategy = TraversalStrategy.TraverseSubtree;

      return InnerTreenumeratorMoveNext(traversalStrategy);
    }

    private bool InnerTreenumeratorMoveNext(TraversalStrategy traversalStrategy)
    {
      var currentDepth =
        Mode == TreenumeratorMode.EnumerationNotStarted
        ? -1
        : InnerTreenumerator.OriginalPosition.Depth;

      while (InnerTreenumerator.MoveNext(traversalStrategy))
      {
        if (InnerTreenumerator.OriginalPosition.Depth < currentDepth - 1)
        {
          _SkippedSiblingsCounts.RemoveAt(_SkippedSiblingsCounts.Count - 1);
        }
        else if (InnerTreenumerator.OriginalPosition.Depth > currentDepth)
        {
          _SkippedSiblingsCounts.Add(0);
        }

        currentDepth = InnerTreenumerator.OriginalPosition.Depth;

        if (InnerTreenumerator.Mode == TreenumeratorMode.VisitingNode
          || !_Predicate(InnerTreenumerator.ToNodeVisit()))
        {
          UpdateState();

          return true;
        }

        _SkippedSiblingsCounts[InnerTreenumerator.OriginalPosition.Depth]++;

        traversalStrategy = TraversalStrategy.SkipSubtree;
      }

      UpdateState();

      return false;
    }

    private void UpdateState()
    {
      Mode = InnerTreenumerator.Mode;

      if (Mode != TreenumeratorMode.EnumerationFinished)
      {
        Node = InnerTreenumerator.Node;
        VisitCount = InnerTreenumerator.VisitCount;
        OriginalPosition = InnerTreenumerator.OriginalPosition + (-_SkippedSiblingsCounts[InnerTreenumerator.OriginalPosition.Depth], 0);
        Position = InnerTreenumerator.Position;
      }
    }
  }
}
