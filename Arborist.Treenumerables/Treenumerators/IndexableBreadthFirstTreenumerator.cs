using Nito.Collections;
using System.Collections.Generic;
using System.Linq;

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
      if (State == TreenumeratorState.SchedulingNode)
        OnSchedulingVisit(schedulingStrategy);

      if (!_RootsEnumerationCompleted)
        return BeforeRootsEnumerated(schedulingStrategy);

      return AfterRootsEnumerated(schedulingStrategy);
    }

    private void OnSchedulingVisit(SchedulingStrategy schedulingStrategy)
    {
      var lastScheduled = _CurrentLevel.RemoveFromFront();

      if (schedulingStrategy == SchedulingStrategy.SkipNode)
      {
        lastScheduled = lastScheduled.Skip();
        _CurrentLevel.AddToFront(lastScheduled);
      }
      else if (schedulingStrategy == SchedulingStrategy.ScheduleForTraversal)
      {
        if (_RootsEnumerationCompleted)
        {
          // TODO: This is not ideal. I would prefer to cache the index of the nodes that were no skipped
          // to avoid this enumeration.
          var visitIndex = _CurrentLevel.TakeWhile(x => x.Skipped).Count();
          var unskippedVisit = _CurrentLevel[visitIndex];
          lastScheduled = lastScheduled.With(null, unskippedVisit.VisitCount - 1, null, null, null);
          _NextLevel.AddToBack(lastScheduled);
        }
        else
          _CurrentLevel.AddToBack(lastScheduled);
      }
    }

    private bool BeforeRootsEnumerated(SchedulingStrategy schedulingStrategy)
    {
      if (_CurrentLevel.Count > 0 && _CurrentLevel[0].Skipped)
      {
        var skippedNode = _CurrentLevel.RemoveFromFront();

        if (skippedNode.HasNextChild())
        {
          var childIndex = skippedNode.VisitedChildrenCount;

          var nextVisit =
            IndexableTreenumeratorNodeVisit
            .Create<TNode, TValue>(
              skippedNode.Node[childIndex],
              0,
              childIndex,
              skippedNode.Depth + 1,
              0,
              false);
          
          skippedNode = skippedNode.IncrementVisitedChildrenCount();
          Current = nextVisit.ToNodeVisit();

          _CurrentLevel.AddToFront(skippedNode);

          _CurrentLevel.AddToFront(nextVisit);

          State = TreenumeratorState.SchedulingNode;

          return true;
        }
      }

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

        _CurrentLevel.AddToFront(nextVisit);

        Current = nextVisit.ToNodeVisit();

        State = TreenumeratorState.SchedulingNode;

        return true;
      }

      _RootsEnumerationCompleted = true;
      _SiblingIndex = 0;

      return AfterRootsEnumerated(schedulingStrategy);
    }

    // TODO: Remove _SiblingIndex reference from AfterRootsEnumerated. (I don't remember why I added this note).
    private bool AfterRootsEnumerated(SchedulingStrategy schedulingStrategy)
    {
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
          if (visit.Skipped)
          {
            _CurrentLevel.AddToFront(visit);
            var visitIndex = _CurrentLevel.TakeWhile(x => x.Skipped).Count();

            visit = _CurrentLevel[visitIndex].IncrementVisitCount();

            _CurrentLevel[visitIndex] = visit;

            Current =
              visit
              .ToNodeVisit()
              .WithDepth(_Depth)
              .WithSiblingIndex(_SiblingIndex);

            State = TreenumeratorState.VisitingNode;

            return true;
          }

          if (visit.SiblingIndex == 0)
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

          _CurrentLevel.AddToFront(nextVisit);

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

    public override void Dispose()
    {
      _RootsEnumerator?.Dispose();
    }
  }
}
