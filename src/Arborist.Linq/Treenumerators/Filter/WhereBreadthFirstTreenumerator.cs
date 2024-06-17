using Arborist.Core;
using Arborist.Linq.Extensions;
using Nito.Collections;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class WhereBreadthFirstTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public WhereBreadthFirstTreenumerator(
      ITreenumerator<TNode> innerTreenumerator,
      Func<NodeVisit<TNode>, bool> predicate)
      : base(innerTreenumerator)
    {
      _Predicate = predicate;
      _NodePositionAndVisitCounts.AddToBack(new NodePositionAndVisitCount(innerTreenumerator.Position, 0));
    }

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;

    private Deque<NodePositionAndVisitCount> _NodePositionAndVisitCounts = new Deque<NodePositionAndVisitCount>();
    private Stack<NodeVisit<TNode>> _SkippedStack = new Stack<NodeVisit<TNode>>();

    private int _SeenRootNodesCount = 0;

    private bool _EnumerationFinished = false;

    protected override bool OnMoveNext(TraversalStrategy traversalStrategy)
    {
      if (_EnumerationFinished)
        return false;

      if (Mode == TreenumeratorMode.VisitingNode)
        traversalStrategy = TraversalStrategy.TraverseSubtree;

      return InnerTreenumeratorMoveNext(traversalStrategy);
    }

    private bool InnerTreenumeratorMoveNext(TraversalStrategy traversalStrategy)
    {
      var previouslySeenNodeWasScheduledAndSkipped =
        Position != (0, -1)
        && InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && (traversalStrategy == TraversalStrategy.SkipNode || traversalStrategy == TraversalStrategy.SkipSubtree);

      if (previouslySeenNodeWasScheduledAndSkipped)
        _NodePositionAndVisitCounts[_NodePositionAndVisitCounts.Count - 1] = _NodePositionAndVisitCounts[_NodePositionAndVisitCounts.Count - 1].Skip();

      var previousModeWasVisitingNode = Mode == TreenumeratorMode.VisitingNode;

      while (InnerTreenumerator.MoveNext(traversalStrategy))
      {
        while (_SkippedStack.Count > 0 && _SkippedStack.Peek().Position.Depth >= InnerTreenumerator.Position.Depth)
        {
          _SkippedStack.Pop();
        }

        var effectiveDepth = InnerTreenumerator.Position.Depth - _SkippedStack.Count;

        if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        {
          var skipped = !_Predicate(InnerTreenumerator.ToNodeVisit());

          if (skipped)
          {
            _SkippedStack.Push(InnerTreenumerator.ToNodeVisit());

            traversalStrategy = TraversalStrategy.SkipNode;

            continue;
          }
          else
          {
            var effectivePosition = GetEffectivePosition();

            var lastScheduleNodeVisitWasSkipped = _NodePositionAndVisitCounts[_NodePositionAndVisitCounts.Count - 1].Skipped;

            if (lastScheduleNodeVisitWasSkipped)
              _NodePositionAndVisitCounts.RemoveFromBack();

            _NodePositionAndVisitCounts.AddToBack(new NodePositionAndVisitCount(effectivePosition, 0));
          }
        }
        else
        {
          if (InnerTreenumerator.VisitCount == 1)
            _NodePositionAndVisitCounts.RemoveFromFront();
          else if (previousModeWasVisitingNode)
            continue;

          //var lastScheduleNodeVisitWasSkipped = _NodePositionAndVisitCounts[_NodePositionAndVisitCounts.Count - 1].Skipped;

          //if (lastScheduleNodeVisitWasSkipped)
          //  _NodePositionAndVisitCounts.RemoveFromBack();

          _NodePositionAndVisitCounts[0] = _NodePositionAndVisitCounts[0].IncrementVisitCount();
        }

        UpdateState();

        return true;
      }

      UpdateState();

      return false;
    }

    private NodePosition GetEffectivePosition()
    {
      var effectiveDepth = InnerTreenumerator.Position.Depth - _SkippedStack.Count;

      int effectiveSiblingIndex;

      if (effectiveDepth == 0)
      {
        effectiveSiblingIndex = _SeenRootNodesCount;
      }
      else if (Mode == TreenumeratorMode.VisitingNode)
      {
        effectiveSiblingIndex = _NodePositionAndVisitCounts[0].VisitCount - 1;
      }
      else
      {
        var previousNodePosition = _NodePositionAndVisitCounts[_NodePositionAndVisitCounts.Count - 1].Position;

        if (previousNodePosition.Depth == effectiveDepth)
          effectiveSiblingIndex = previousNodePosition.SiblingIndex + 1;
        else
          effectiveSiblingIndex = 0;
      }

      return (effectiveSiblingIndex, effectiveDepth);
    }

    private void UpdateState()
    {
      Mode = InnerTreenumerator.Mode;

      if (!_EnumerationFinished)
      {
        var nodePositionAndVisitCountIndex =
          InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
          ? _NodePositionAndVisitCounts.Count - 1
          : 0;

        var nodePositionAndVisitCount = _NodePositionAndVisitCounts[nodePositionAndVisitCountIndex];

        if (nodePositionAndVisitCount.Position.Depth == 0
          && InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
          _SeenRootNodesCount++;

        Node = InnerTreenumerator.Node;
        VisitCount = nodePositionAndVisitCount.VisitCount;
        Position = nodePositionAndVisitCount.Position;
      }
    }

    private struct NodePositionAndVisitCount
    {
      public NodePositionAndVisitCount(
        NodePosition position,
        int visitCount,
        bool skipped = false)
      {
        Position = position;
        VisitCount = visitCount;
        Skipped = skipped;
      }

      public NodePosition Position { get; }
      public int VisitCount { get; }
      public bool Skipped { get; }

      public NodePositionAndVisitCount IncrementVisitCount() =>
        new NodePositionAndVisitCount(Position, VisitCount + 1, Skipped);

      public NodePositionAndVisitCount Skip() =>
        new NodePositionAndVisitCount(Position, VisitCount, true);

      public override string ToString() => $"({Position}), {VisitCount}, {Skipped}";
    }
  }
}
