﻿using Arborist.Core;
using Arborist.Linq.Extensions;
using System;

namespace Arborist.Linq.Treenumerators
{
  internal class PruneAfterTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public PruneAfterTreenumerator(
      ITreenumerator<TNode> innerTreenumerator,
      Func<NodeVisit<TNode>, bool> predicate)
      : base(innerTreenumerator)
    {
      _Predicate = predicate;
    }

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;

    protected override bool OnMoveNext(TraversalStrategy traversalStrategy)
    {
      if (Mode == TreenumeratorMode.EnumerationFinished)
        return false;

      if (Mode == TreenumeratorMode.SchedulingNode)
        traversalStrategy = GetTraversalStrategy(traversalStrategy);

      var result = InnerTreenumerator.MoveNext(traversalStrategy);

      UpdateState();

      return result;
    }

    private TraversalStrategy GetTraversalStrategy(TraversalStrategy traversalStrategy)
    {
      var skippingNode =
        traversalStrategy == TraversalStrategy.SkipSubtree
        || traversalStrategy == TraversalStrategy.SkipNode;

      var skippingDescendants =
        _Predicate(this.ToNodeVisit())
        || traversalStrategy == TraversalStrategy.SkipDescendants
        || traversalStrategy == TraversalStrategy.SkipSubtree;

      if (skippingNode && skippingDescendants)
        return TraversalStrategy.SkipSubtree;
      else if (skippingNode && !skippingDescendants)
        return TraversalStrategy.SkipNode;
      else if (!skippingNode && skippingDescendants)
        return TraversalStrategy.SkipDescendants;
      else
        return TraversalStrategy.TraverseSubtree;
    }

    private void UpdateState()
    {
      Mode = InnerTreenumerator.Mode;
      if (Mode != TreenumeratorMode.EnumerationFinished)
      {
        Node = InnerTreenumerator.Node;
        VisitCount = InnerTreenumerator.VisitCount;
        OriginalPosition = InnerTreenumerator.OriginalPosition;
        Position = InnerTreenumerator.Position;
      }
    }
  }
}
