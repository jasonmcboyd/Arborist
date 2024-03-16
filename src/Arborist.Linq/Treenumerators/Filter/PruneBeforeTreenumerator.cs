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

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (Mode == TreenumeratorMode.EnumerationFinished)
        return false;

      if (Mode == TreenumeratorMode.VisitingNode)
        schedulingStrategy = SchedulingStrategy.TraverseSubtree;

      return InnerTreenumeratorMoveNext(schedulingStrategy);
    }

    private bool InnerTreenumeratorMoveNext(SchedulingStrategy schedulingStrategy)
    {
      while (InnerTreenumerator.MoveNext(schedulingStrategy))
      {
        if (InnerTreenumerator.Mode == TreenumeratorMode.VisitingNode
          || !_Predicate(InnerTreenumerator.ToNodeVisit()))
        {
          UpdateState();

          return true;
        }

        schedulingStrategy = SchedulingStrategy.SkipSubtree;
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
        SchedulingStrategy = InnerTreenumerator.SchedulingStrategy;
      }
    }
  }
}

