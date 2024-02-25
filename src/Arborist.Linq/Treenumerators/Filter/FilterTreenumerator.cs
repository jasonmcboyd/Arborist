using Arborist.Core;
using System;

namespace Arborist.Linq.Treenumerators
{
  internal class FilterTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public FilterTreenumerator(
      ITreenumerator<TNode> innerTreenumerator,
      Func<NodeVisit<TNode>, bool> predicate,
      SchedulingStrategy skipStrategy)
      : base(innerTreenumerator)
    {
      _Predicate = predicate;
      _SkipStrategy = skipStrategy;
    }

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;
    private SchedulingStrategy _SkipStrategy;

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (State == TreenumeratorState.EnumerationFinished)
        return false;

      if (State == TreenumeratorState.SchedulingNode)
        return OnSchedulingNode(schedulingStrategy);

      return OnTraversing();
    }

    private bool OnSchedulingNode(SchedulingStrategy schedulingStrategy)
    {
      throw new NotImplementedException();
      //if (!_Predicate(InnerTreenumerator.Node))
      //{
      //  var skippingNode =
      //    schedulingStrategy == SchedulingStrategy.SkipSubtree
      //    || schedulingStrategy == SchedulingStrategy.SkipNode
      //    || _SkipStrategy == SchedulingStrategy.SkipSubtree
      //    || _SkipStrategy == SchedulingStrategy.SkipNode;

      //  var skippingDescendants =
      //    schedulingStrategy == SchedulingStrategy.SkipSubtree
      //    || schedulingStrategy == SchedulingStrategy.SkipDescendantSubtrees
      //    || _SkipStrategy == SchedulingStrategy.SkipSubtree
      //    || _SkipStrategy == SchedulingStrategy.SkipDescendantSubtrees;

      //  if (skippingDescendants)
      //  {
      //    schedulingStrategy =
      //      skippingNode
      //      ? SchedulingStrategy.SkipSubtree
      //      : SchedulingStrategy.SkipDescendantSubtrees;
      //  }
      //  else
      //  {
      //    schedulingStrategy =
      //      skippingNode
      //      ? SchedulingStrategy.SkipNode
      //      : SchedulingStrategy.ScheduleForTraversal;
      //  }
      //}

      //if (!InnerTreenumerator.MoveNext(schedulingStrategy))
      //{
      //  State = TreenumeratorState.EnumerationFinished;
      //  return false;
      //}

      //if (InnerTreenumerator.State == TreenumeratorState.SchedulingNode)
      //  return OnInnerTreenumeratorIsSchedulingNode();

      //Current = InnerTreenumerator.Node;
      //State = InnerTreenumerator.State;

      //return true;
    }

    private bool OnTraversing()
    {
      throw new NotImplementedException();
      //// The state is not scheduling so it does not matter what we pass for
      //// the strategy.
      //if (!InnerTreenumerator.MoveNext(SchedulingStrategy.ScheduleForTraversal))
      //{
      //  State = TreenumeratorState.EnumerationFinished;
      //  return false;
      //}

      //if (InnerTreenumerator.State == TreenumeratorState.SchedulingNode)
      //  return OnInnerTreenumeratorIsSchedulingNode();

      //Current = InnerTreenumerator.Node;
      //State = InnerTreenumerator.State;

      //return true;
    }

    private bool OnInnerTreenumeratorIsSchedulingNode()
    {
      throw new NotImplementedException();
      //if (_SkipStrategy == SchedulingStrategy.SkipSubtree
      //  || _SkipStrategy == SchedulingStrategy.SkipNode)
      //{
      //  while (!_Predicate(InnerTreenumerator.Node))
      //  {
      //    InnerTreenumerator.MoveNext(_SkipStrategy);

      //    if (InnerTreenumerator.State == TreenumeratorState.EnumerationFinished)
      //    {
      //      State = TreenumeratorState.EnumerationFinished;
      //      return false;
      //    }
      //  }
      //}

      //if (InnerTreenumerator.State == TreenumeratorState.SchedulingNode)
      //{
      //  if (State == TreenumeratorState.EnumerationNotStarted)
      //  {
      //    Current =
      //      NodeVisit
      //      .Create(
      //        InnerTreenumerator.Node.Node,
      //        0,
      //        default,
      //        default,
      //        InnerTreenumerator.Node.SchedulingStrategy);
      //  }
      //  else
      //  {
      //    Current =
      //      NodeVisit
      //      .Create(
      //        InnerTreenumerator.Node.Node,
      //        0,
      //        Current.OriginalPosition + (1, -1),
      //        default,
      //        InnerTreenumerator.Node.SchedulingStrategy);
      //  }
      //}
      //else
      //  Current = InnerTreenumerator.Node;

      //State = InnerTreenumerator.State;

      //return true;
    }
  }
}
