using Arborist.Core;
using Arborist.Linq.Extensions;
using System;

namespace Arborist.Linq.Treenumerators
{
  internal class PruneBeforeTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public PruneBeforeTreenumerator(
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

      if (Mode == TreenumeratorMode.VisitingNode)
        traversalStrategy = TraversalStrategy.TraverseSubtree;

      return InnerTreenumeratorMoveNext(traversalStrategy);
    }

    private bool InnerTreenumeratorMoveNext(TraversalStrategy traversalStrategy)
    {
      while (InnerTreenumerator.MoveNext(traversalStrategy))
      {
        if (InnerTreenumerator.Mode == TreenumeratorMode.VisitingNode
          || !_Predicate(InnerTreenumerator.ToNodeVisit()))
        {
          UpdateState();

          return true;
        }

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
        OriginalPosition = InnerTreenumerator.Position;
        Position = InnerTreenumerator.Position;
      }
    }
  }
}

