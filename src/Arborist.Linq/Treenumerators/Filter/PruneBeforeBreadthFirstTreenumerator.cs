﻿using Arborist.Core;
using Arborist.Linq.Extensions;
using Nito.Collections;
using System;
using System.Linq;

namespace Arborist.Linq.Treenumerators
{
  internal class PruneBeforeBreadthFirstTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public PruneBeforeBreadthFirstTreenumerator(
      ITreenumerator<TNode> innerTreenumerator,
      Func<NodeVisit<TNode>, bool> predicate)
      : base(innerTreenumerator)
    {
      _Predicate = predicate;
      _SkippedSiblingsCounts.AddToBack(0);
    }

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;

    private Deque<int> _SkippedSiblingsCounts = new Deque<int>();

    private int _CurrentNodesSkippedChildrenCount = 0;

    private int _PrunedRootNodesCount = 0;

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
      var previousNodeSkipped =
        InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && traversalStrategy == TraversalStrategy.SkipNode;

      var previousSubtreeSkipped =
        InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && traversalStrategy == TraversalStrategy.SkipSubtree;

      if ((previousSubtreeSkipped || previousNodeSkipped)
        && InnerTreenumerator.OriginalPosition.Depth == 0)
      {
        _SkippedSiblingsCounts.RemoveFromBack();
      }

      // TODO:
      //var previousDepth =
      //  InnerTreenumerator.Mode == TreenumeratorMode.EnumerationNotStarted
      //  ? -1
      //  : InnerTreenumerator.OriginalPosition.Depth;
      var previousDepth = InnerTreenumerator.OriginalPosition.Depth;

      while (InnerTreenumerator.MoveNext(traversalStrategy))
      {
        if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        {
          var skipped = _Predicate(InnerTreenumerator.ToNodeVisit());

          if (skipped)
          {
            if (InnerTreenumerator.OriginalPosition.Depth == 0)
              _PrunedRootNodesCount++;

            _CurrentNodesSkippedChildrenCount++;
            traversalStrategy = TraversalStrategy.SkipSubtree;
            continue;
          }
          else
          {
            if (InnerTreenumerator.OriginalPosition.Depth == 0
              && previousDepth == 0)
            {
              _CurrentNodesSkippedChildrenCount = _PrunedRootNodesCount; 
            }

            _SkippedSiblingsCounts.AddToBack(_CurrentNodesSkippedChildrenCount);
          }
        }
        else if (InnerTreenumerator.VisitCount == 1)
        {
          _SkippedSiblingsCounts.RemoveFromFront();
          _CurrentNodesSkippedChildrenCount = 0;
        }

        UpdateState();

        return true;
      }

      UpdateState();

      return false;
    }

    private void UpdateState()
    {
      Mode = InnerTreenumerator.Mode;

      if (!_EnumerationFinished)
      {
        var skippedSiblingsCount =
          InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
          ? _SkippedSiblingsCounts.Last()
          : _SkippedSiblingsCounts[0];

        Node = InnerTreenumerator.Node;
        VisitCount = InnerTreenumerator.VisitCount;
        OriginalPosition = InnerTreenumerator.OriginalPosition + (-skippedSiblingsCount, 0);
        Position = InnerTreenumerator.Position;
      }
    }
  }
}