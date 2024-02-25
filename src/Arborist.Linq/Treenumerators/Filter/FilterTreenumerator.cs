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

    private bool _NodeIsScheduledToBeSkipped;

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (State == TreenumeratorState.EnumerationFinished)
        return false;

      if (State == TreenumeratorState.SchedulingNode)
        return OnSchedulingNode(schedulingStrategy);

      var result = InnerTreenumerator.MoveNext(schedulingStrategy);

      if (InnerTreenumerator.State != TreenumeratorState.SchedulingNode)
      {
        UpdateState(true);
        return result;
      }

      return OnInnerTreenumeratorSchedulingNode();
    }

    private bool OnSchedulingNode(SchedulingStrategy schedulingStrategy)
    {
      bool skippingNode = false;
      bool skippingDescendants = false;

      if (_NodeIsScheduledToBeSkipped)
      {
        _NodeIsScheduledToBeSkipped = false;

        skippingNode =
          schedulingStrategy == SchedulingStrategy.SkipSubtree
          || schedulingStrategy == SchedulingStrategy.SkipNode
          || _SkipStrategy == SchedulingStrategy.SkipSubtree
          || _SkipStrategy == SchedulingStrategy.SkipNode;

        skippingDescendants =
          schedulingStrategy == SchedulingStrategy.SkipSubtree
          || schedulingStrategy == SchedulingStrategy.SkipDescendants
          || _SkipStrategy == SchedulingStrategy.SkipSubtree
          || _SkipStrategy == SchedulingStrategy.SkipDescendants;
      }

      if (skippingNode && skippingDescendants)
        schedulingStrategy = SchedulingStrategy.SkipSubtree;
      else if (skippingNode && !skippingDescendants)
        schedulingStrategy = SchedulingStrategy.SkipNode;
      else if (!skippingNode && skippingDescendants)
        schedulingStrategy = SchedulingStrategy.SkipDescendants;
      else
        schedulingStrategy = SchedulingStrategy.SkipSubtree;

      if (!InnerTreenumerator.MoveNext(schedulingStrategy))
      {
        UpdateState(true);
        return false;
      }

      if (InnerTreenumerator.State == TreenumeratorState.SchedulingNode)
        return OnInnerTreenumeratorSchedulingNode();

      UpdateState(true);

      return true;
    }

    private bool OnInnerTreenumeratorSchedulingNode()
    {
      if (_SkipStrategy == SchedulingStrategy.SkipDescendants)
      {
        _NodeIsScheduledToBeSkipped = true;
        UpdateState(false);
        return true;
      }

      if (_SkipStrategy == SchedulingStrategy.SkipSubtree
        || _SkipStrategy == SchedulingStrategy.SkipNode)
      {
        while (InnerTreenumerator.State == TreenumeratorState.SchedulingNode
          && !_Predicate(InnerTreenumerator.ToNodeVisit()))
        {
          InnerTreenumerator.MoveNext(_SkipStrategy);

          if (InnerTreenumerator.State == TreenumeratorState.EnumerationFinished)
          {
            UpdateState(false);
            return false;
          }
        }
      }

      if (InnerTreenumerator.State == TreenumeratorState.SchedulingNode)
      {
        if (State == TreenumeratorState.EnumerationNotStarted)
        {
          UpdateState(false);
          //Current =
          //  NodeVisit
          //  .Create(
          //    InnerTreenumerator.Node.Node,
          //    0,
          //    default,
          //    default,
          //    InnerTreenumerator.Node.SchedulingStrategy);
        }
        else
        {
          UpdateState(false);
          //Current =
          //  NodeVisit
          //  .Create(
          //    InnerTreenumerator.Node.Node,
          //    0,
          //    Current.OriginalPosition + (1, -1),
          //    default,
          //    InnerTreenumerator.Node.SchedulingStrategy);
        }
      }
      else
      {
        UpdateState(false);
      }
        //Current = InnerTreenumerator.Node;

      //State = InnerTreenumerator.State;

      return true;
    }

    private void UpdateState(bool preserveOriginalPosition)
    {
      State = InnerTreenumerator.State;
      Node = InnerTreenumerator.Node;
      VisitCount = InnerTreenumerator.VisitCount;
      OriginalPosition = InnerTreenumerator.OriginalPosition;
      //OriginalPosition =
      //  preserveOriginalPosition
      //  ? InnerTreenumerator.OriginalPosition
      //  : InnerTreenumerator.Position;
      Position = InnerTreenumerator.Position;
      SchedulingStrategy = InnerTreenumerator.SchedulingStrategy;
    }
  }
}
