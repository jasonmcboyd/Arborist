using Arborist.Core;
using Arborist.Linq.Extensions;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class WhereDepthFirstTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public WhereDepthFirstTreenumerator(
      ITreenumerator<TNode> innerTreenumerator,
      Func<NodeVisit<TNode>, bool> predicate)
      : base(innerTreenumerator)
    {
      _Predicate = predicate;
    }

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;

    private List<int> _SkippedSiblingsCount = new List<int>();

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

      var currentDepth =
        Mode == TreenumeratorMode.EnumerationNotStarted
        ? -1
        : InnerTreenumerator.Position.Depth;

      while (InnerTreenumerator.MoveNext(adjustedTraversalStrategy))
      {
        adjustedTraversalStrategy = traversalStrategy;

        if (InnerTreenumerator.Position.Depth < currentDepth)
        {
          _SkippedSiblingsCount.RemoveAt(_SkippedSiblingsCount.Count - 1);
        }
        else if (InnerTreenumerator.Position.Depth > currentDepth)
        {
          _SkippedSiblingsCount.Add(0);
        }
        else
        {
          if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
          {
            if (InnerTreenumerator.OriginalPosition.Depth > currentDepth)
              _SkippedSiblingsCount[InnerTreenumerator.Position.Depth]--;
            else if (InnerTreenumerator.OriginalPosition.Depth < currentDepth)
              _SkippedSiblingsCount[InnerTreenumerator.Position.Depth]++;
          }
        }

        if (InnerTreenumerator.Mode == TreenumeratorMode.VisitingNode
          || _Predicate(InnerTreenumerator.ToNodeVisit()))
        {
          UpdateState();

          return true;
        }

        _SkippedSiblingsCount[InnerTreenumerator.Position.Depth]++;

        adjustedTraversalStrategy = TraversalStrategy.SkipNode;

        currentDepth = InnerTreenumerator.Position.Depth;
      }

      UpdateState();

      return false;
    }

    private void UpdateState()
    {
      Mode = InnerTreenumerator.Mode;

      var depthDelta = InnerTreenumerator.OriginalPosition.Depth - _SkippedSiblingsCount.Count;

      if (Mode != TreenumeratorMode.EnumerationFinished)
      {
        Node = InnerTreenumerator.Node;
        VisitCount = InnerTreenumerator.VisitCount;
        OriginalPosition =
          (InnerTreenumerator.OriginalPosition.SiblingIndex - _SkippedSiblingsCount[InnerTreenumerator.Position.Depth], InnerTreenumerator.Position.Depth);
        Position = InnerTreenumerator.Position;
      }
    }
  }
}
