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
      if (State == TreenumeratorState.SchedulingNode
        && !_Predicate(InnerTreenumerator.Current))
        schedulingStrategy = _SkipStrategy;

      var result = InnerTreenumerator.MoveNext(schedulingStrategy);

      if (!result)
        return false;

      Current = InnerTreenumerator.Current;
      State = InnerTreenumerator.State;

      return true;
    }
  }
}
