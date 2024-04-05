using Arborist.Core;
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
      _SkippedSiblingCounts.AddToBack(0);
    }

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;

    private Deque<int> _SkippedSiblingCounts = new Deque<int>();

    private int _CurrentNodesSkippedChildrenCount = 0;

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
      var adjustedTraversalStrategy = traversalStrategy;

      if (traversalStrategy == TraversalStrategy.SkipSubtree
        && InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && InnerTreenumerator.OriginalPosition.Depth == 0)
      {
        _SkippedSiblingCounts.RemoveFromBack();
      }

      while (InnerTreenumerator.MoveNext(traversalStrategy))
      {
        adjustedTraversalStrategy = traversalStrategy;

        if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        {
          var skipped = _Predicate(InnerTreenumerator.ToNodeVisit());

          if (skipped)
          {
            _CurrentNodesSkippedChildrenCount++;
            traversalStrategy = TraversalStrategy.SkipSubtree;
            continue;
          }
          else
          {
            _SkippedSiblingCounts.AddToBack(_CurrentNodesSkippedChildrenCount);
          }
        }
        else if (InnerTreenumerator.VisitCount == 1)
          //&& InnerTreenumerator.Position.Depth > 0)
        {
          _SkippedSiblingCounts.RemoveFromFront();
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

      if (Mode != TreenumeratorMode.EnumerationFinished)
      {
        var skippedSiblingsCount =
          InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
          ? _SkippedSiblingCounts.Last()
          : _SkippedSiblingCounts[0];

        Node = InnerTreenumerator.Node;
        VisitCount = InnerTreenumerator.VisitCount;
        OriginalPosition = InnerTreenumerator.OriginalPosition + (-skippedSiblingsCount, 0);
        Position = InnerTreenumerator.Position;
      }
    }
  }
}
