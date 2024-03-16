using Arborist.Core;
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

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (Mode == TreenumeratorMode.EnumerationFinished)
        return false;

      if (Mode == TreenumeratorMode.SchedulingNode)
        schedulingStrategy = GetSchedulingStrategy(schedulingStrategy);

      var result = InnerTreenumerator.MoveNext(schedulingStrategy);

      UpdateState();

      return result;
    }

    private SchedulingStrategy GetSchedulingStrategy(SchedulingStrategy schedulingStrategy)
    {
      var skippingNode =
        schedulingStrategy == SchedulingStrategy.SkipSubtree
        || schedulingStrategy == SchedulingStrategy.SkipNode;

      var skippingDescendants =
        _Predicate(this.ToNodeVisit())
        || schedulingStrategy == SchedulingStrategy.SkipDescendants
        || schedulingStrategy == SchedulingStrategy.SkipSubtree;

      if (skippingNode && skippingDescendants)
        return SchedulingStrategy.SkipSubtree;
      else if (skippingNode && !skippingDescendants)
        return SchedulingStrategy.SkipNode;
      else if (!skippingNode && skippingDescendants)
        return SchedulingStrategy.SkipDescendants;
      else
        return SchedulingStrategy.TraverseSubtree;
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
