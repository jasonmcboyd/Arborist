using Nito.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

    private int _Depth = -1;

    private bool _ScheduledNodeAddedToFront = false;

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (State == TreenumeratorState.SchedulingNode)
      {
        if (schedulingStrategy == SchedulingStrategy.SkipSubtree)
          _NextLevel.RemoveFromBack();
        else if (schedulingStrategy == SchedulingStrategy.SkipNode)
        {
          if (_ScheduledNodeAddedToFront)
          {
            var lastScheduled = _CurrentLevel.RemoveFromFront();
            lastScheduled = lastScheduled.Skip();
            _CurrentLevel.AddToFront(lastScheduled);
          }
          else
          {
            var lastScheduled = _NextLevel.RemoveFromBack();
            lastScheduled = lastScheduled.Skip();
            _NextLevel.AddToBack(lastScheduled);
          }
        }
      }

      if (!_RootsEnumerationCompleted)
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
      }

      while (true)
      {
        if (_CurrentLevel.Count == 0)
        {
          _Depth++;
          _SiblingIndex = 0;
          (_CurrentLevel, _NextLevel) = (_NextLevel, _CurrentLevel);
        }

        if (_CurrentLevel.Count == 0)
        {
          State = TreenumeratorState.EnumerationFinished;
          return false;
        }

        var visit = _CurrentLevel.RemoveFromFront();

        if ((visit.VisitCount == 0
            && !visit.Skipped)
          || (State == TreenumeratorState.SchedulingNode
            && schedulingStrategy == SchedulingStrategy.ScheduleForTraversal))
        {
          if (visit.SiblingIndex == 0
            && !_ScheduledNodeAddedToFront)
            _SiblingIndex = 0;

          visit = visit.IncrementVisitCount();
          _CurrentLevel.AddToFront(visit);

          Current =
            visit
            .ToNodeVisit()
            .WithDepth(_Depth)
            .WithSiblingIndex(_SiblingIndex);

          State = TreenumeratorState.VisitingNode;

          return true;
        }

        if (visit.VisitedChildrenCount < visit.Node.ChildCount)
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

          if (visit.Skipped)
            ScheduleVisitNext(nextVisit);
          else
            ScheduleVisitLast(nextVisit);

          State = TreenumeratorState.SchedulingNode;

          return true;
        }

        if (visit.VisitCount == 1)
        {
          visit = visit.IncrementVisitCount();
          _CurrentLevel.AddToFront(visit);

          Current =
            visit
            .ToNodeVisit()
            .WithDepth(_Depth)
            .WithSiblingIndex(_SiblingIndex);

          State = TreenumeratorState.VisitingNode;

          return true;
        }

        if (!visit.Skipped)
          _SiblingIndex++;
      }
    }

    private void ScheduleVisitNext(IndexableTreenumeratorNodeVisit<TNode, TValue> visitToSchedule)
    {
      _CurrentLevel.AddToFront(visitToSchedule);
      _ScheduledNodeAddedToFront = true;
    }

    private void ScheduleVisitLast(IndexableTreenumeratorNodeVisit<TNode, TValue> visitToSchedule)
    {
      _NextLevel.AddToBack(visitToSchedule);
      _ScheduledNodeAddedToFront = false;
    }

    public override void Dispose()
    {
      _RootsEnumerator?.Dispose();
    }
  }
}
