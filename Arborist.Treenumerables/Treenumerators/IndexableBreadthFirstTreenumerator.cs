using Nito.Collections;
using System.Collections.Generic;

namespace Arborist.Treenumerables.Treenumerators
{
  internal sealed class IndexableBreadthFirstTreenumerator<TNode, TValue>
    : TreenumeratorBase<TValue>
    where TNode : INodeWithIndexableChildren<TNode, TValue>
  {
    public IndexableBreadthFirstTreenumerator(IEnumerable<TNode> roots)
    {
      _RootsEnumerator = roots.GetEnumerator();
    }

    private readonly IEnumerator<TNode> _RootsEnumerator;
    private bool _RootsEnumerationCompleted = false;

    private int _SiblingIndex = -1;

    private Deque<IndexableTreenumeratorNodeVisit<TNode, TValue>> _CurrentLevel = new Deque<IndexableTreenumeratorNodeVisit<TNode, TValue>>();
    private Deque<IndexableTreenumeratorNodeVisit<TNode, TValue>> _NextLevel = new Deque<IndexableTreenumeratorNodeVisit<TNode, TValue>>();

    private int _Depth = 0;

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (State == TreenumeratorState.EnumerationFinished)
        return false;

      if (State == TreenumeratorState.SchedulingNode)
        OnSchedulingVisit(schedulingStrategy);

      if (!_RootsEnumerationCompleted)
      {
        var beforeRootsEnumerated = BeforeRootsEnumerated(schedulingStrategy);

        if (beforeRootsEnumerated.HasValue)
          return beforeRootsEnumerated.Value;
      }

      if (_CurrentLevel.Count == 0)
        (_CurrentLevel, _NextLevel) = (_NextLevel, _CurrentLevel);

      if (_CurrentLevel.Count == 0)
      {
        State = TreenumeratorState.EnumerationFinished;
        return false;
      }

      return AfterRootsEnumerated(schedulingStrategy);
    }

    private void OnSchedulingVisit(SchedulingStrategy schedulingStrategy)
    {
      var lastScheduled = _NextLevel.RemoveFromBack();

      if (schedulingStrategy == SchedulingStrategy.SkipSubtree)
        return;

      if (schedulingStrategy == SchedulingStrategy.SkipDescendantSubtrees)
        lastScheduled = lastScheduled.With(null, null, null, lastScheduled.Node.ChildCount);
      else if (schedulingStrategy == SchedulingStrategy.SkipNode)
        lastScheduled = lastScheduled.Skip();

      _NextLevel.AddToBack(lastScheduled);
    }

    private bool? BeforeRootsEnumerated(SchedulingStrategy schedulingStrategy)
    {
      if (_RootsEnumerator.MoveNext())
      {
        _SiblingIndex++;

        var nextVisit =
          IndexableTreenumeratorNodeVisit
          .Create<TNode, TValue>(
            _RootsEnumerator.Current,
            0,
            _SiblingIndex,
            0,
            0,
            false);

        _NextLevel.AddToBack(nextVisit);

        Current = nextVisit.ToNodeVisit();

        State = TreenumeratorState.SchedulingNode;

        return true;
      }

      _RootsEnumerationCompleted = true;
      _SiblingIndex = 0;

      return null;
    }

    private bool AfterRootsEnumerated(SchedulingStrategy schedulingStrategy)
    {
      while (true)
      {
        if (_CurrentLevel.Count == 0)
          (_CurrentLevel, _NextLevel) = (_NextLevel, _CurrentLevel);

        if (_CurrentLevel.Count == 0)
          return false;

        var visit = _CurrentLevel.RemoveFromFront();

        // TODO: State == Scheduling and VisitCount == 0 should always happen together.
        if (visit.VisitCount == 0
          || (visit.VisitCount == 1 && !visit.HasNextChild())
          || State == TreenumeratorState.SchedulingNode)
        {
          visit = visit.IncrementVisitCount();

          Current = visit.ToNodeVisit();

          _CurrentLevel.AddToFront(visit);

          State = TreenumeratorState.VisitingNode;

          return true;
        }

        if (visit.HasNextChild())
        {
          var childIndex = visit.VisitedChildrenCount;

          var nextVisit =
            IndexableTreenumeratorNodeVisit
            .Create<TNode, TValue>(
              visit.Node[childIndex],
              0,
              childIndex,
              visit.Depth + 1,
              0,
              false);

          visit = visit.IncrementVisitedChildrenCount();
          Current = nextVisit.ToNodeVisit();

          _CurrentLevel.AddToFront(visit);

          _NextLevel.AddToBack(nextVisit);

          State = TreenumeratorState.SchedulingNode;

          return true;
        }
      }
    }

    public override void Dispose()
    {
      _RootsEnumerator?.Dispose();
    }
  }
}
