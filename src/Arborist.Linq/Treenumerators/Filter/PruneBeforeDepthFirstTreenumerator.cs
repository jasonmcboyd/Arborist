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

    private bool _EnumerationFinished = false;

    protected override bool OnMoveNext(TraversalStrategy traversalStrategy)
    {
      if (_EnumerationFinished)
        return false;

      if (Mode == TreenumeratorMode.VisitingNode)
        traversalStrategy = TraversalStrategy.TraverseSubtree;

      return InnerTreenumeratorMoveNext(traversalStrategy);
    }

    private bool InnerTreenumeratorMoveNext(TraversalStrategy traversalStrategy)
    {
      var currentDepth = InnerTreenumerator.OriginalPosition.Depth;

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

      _EnumerationFinished = true;

      return false;
    }

    private void UpdateState()
    {
      Mode = InnerTreenumerator.Mode;

      var originalPositionDelta =
        _SkippedSiblingsCounts.Count > 0
        ? (-_SkippedSiblingsCounts[InnerTreenumerator.OriginalPosition.Depth], 0)
        : (0, 0);

      if (!_EnumerationFinished)
      {
        Node = InnerTreenumerator.Node;
        VisitCount = InnerTreenumerator.VisitCount;
        OriginalPosition = InnerTreenumerator.OriginalPosition + originalPositionDelta;
      }
    }
  }
}
