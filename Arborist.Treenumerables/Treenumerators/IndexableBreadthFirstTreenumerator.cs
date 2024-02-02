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

    private bool _ScheduledToFront = false;

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
      //IndexableTreenumeratorNodeVisit<TNode, TValue> lastScheduled;

      //if (_ScheduledToFront)
      //  lastScheduled = _CurrentLevel.RemoveFromFront();
      //else
      //  lastScheduled = _NextLevel.RemoveFromBack();
      var lastScheduled = _CurrentLevel.RemoveFromFront();

      if (schedulingStrategy == SchedulingStrategy.SkipSubtree)
        return;

      if (schedulingStrategy == SchedulingStrategy.SkipDescendantSubtrees)
        lastScheduled = lastScheduled.With(null, null, null, lastScheduled.Node.ChildCount);
      else if (schedulingStrategy == SchedulingStrategy.SkipNode)
        lastScheduled = lastScheduled.Skip();

      //if (_ScheduledToFront)
      //  _CurrentLevel.AddToFront(lastScheduled);
      //else
      //  _NextLevel.AddToBack(lastScheduled);

      if (_CurrentLevel.Count > 0 && _CurrentLevel[0].Skipped)
        _CurrentLevel.AddToFront(lastScheduled);
      else
        _NextLevel.AddToBack(lastScheduled);

      //_ScheduledToFront = false;
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

        //_NextLevel.AddToBack(nextVisit);
        _CurrentLevel.AddToFront(nextVisit);

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
      var timesThroughLoop = 0;

      while (true)
      {
        if (_CurrentLevel.Count == 0)
          (_CurrentLevel, _NextLevel) = (_NextLevel, _CurrentLevel);

        if (_CurrentLevel.Count == 0)
          return false;

        var visit = _CurrentLevel.RemoveFromFront();

        if (timesThroughLoop > 0
          || visit.VisitCount == 0
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

          //if (visit.Skipped)
          //{
          //  _ScheduledToFront = true;
          //  _CurrentLevel.AddToFront(nextVisit);
          //}
          //else
          //  _NextLevel.AddToBack(nextVisit);
          _CurrentLevel.AddToFront(nextVisit);

          State = TreenumeratorState.SchedulingNode;

          return true;
        }

        timesThroughLoop++;
      }
    }

    public override void Dispose()
    {
      _RootsEnumerator?.Dispose();
    }
  }
}
