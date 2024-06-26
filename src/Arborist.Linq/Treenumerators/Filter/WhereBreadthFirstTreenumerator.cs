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
      Func<NodeVisit<TNode>, bool> predicate,
      NodeTraversalStrategy nodeTraversalStrategy)
      : base(innerTreenumerator)
    {
      _Predicate = predicate;
      _TraversalStrategy = nodeTraversalStrategy;
      _NodePositionAndVisitCounts.AddToBack(new NodeTraversalStatus(innerTreenumerator.Position, 0));
    }

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;
    private readonly NodeTraversalStrategy _TraversalStrategy;

    private readonly Deque<NodeTraversalStatus> _NodePositionAndVisitCounts = new Deque<NodeTraversalStatus>();
    private readonly Stack<NodeVisit<TNode>> _SkippedStack = new Stack<NodeVisit<TNode>>();

    private int _SeenRootNodesCount = 0;

    private bool _EnumerationFinished = false;

    protected override bool OnMoveNext(NodeTraversalStrategy nodeTraversalStrategy)
    {
      if (_EnumerationFinished)
      {
        return false;
      }

      if (Mode == TreenumeratorMode.VisitingNode)
      {
        nodeTraversalStrategy = NodeTraversalStrategy.TraverseSubtree;
      }

      return InnerTreenumeratorMoveNext(nodeTraversalStrategy);
    }

    private bool InnerTreenumeratorMoveNext(NodeTraversalStrategy nodeTraversalStrategy)
    {
      var previouslySeenNodeWasScheduledAndSkipped =
        Position != (0, -1)
        && InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && (nodeTraversalStrategy == NodeTraversalStrategy.SkipNode || nodeTraversalStrategy == NodeTraversalStrategy.SkipSubtree);

      if (previouslySeenNodeWasScheduledAndSkipped)
      {
        _NodePositionAndVisitCounts[_NodePositionAndVisitCounts.Count - 1] = _NodePositionAndVisitCounts[_NodePositionAndVisitCounts.Count - 1].Skip(nodeTraversalStrategy);
      }

      var previousModeWasVisitingNode = Mode == TreenumeratorMode.VisitingNode;

      while (InnerTreenumerator.MoveNext(nodeTraversalStrategy))
      {
        while (_SkippedStack.Count > 0 && _SkippedStack.Peek().Position.Depth >= InnerTreenumerator.Position.Depth)
        {
          _SkippedStack.Pop();
        }

        var effectiveDepth = InnerTreenumerator.Position.Depth - _SkippedStack.Count;

        if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        {
          var skipped = _Predicate(InnerTreenumerator.ToNodeVisit());

          if (skipped)
          {
            _SkippedStack.Push(InnerTreenumerator.ToNodeVisit());

            nodeTraversalStrategy = _TraversalStrategy;

            continue;
          }
          else
          {
            var effectivePosition = GetEffectivePosition();

            var lastScheduleNodeVisitWasSkipped = _NodePositionAndVisitCounts[_NodePositionAndVisitCounts.Count - 1].Skipped;

            if (lastScheduleNodeVisitWasSkipped)
            {
              _NodePositionAndVisitCounts.RemoveFromBack();
            }

            _NodePositionAndVisitCounts.AddToBack(new NodeTraversalStatus(effectivePosition, 0));
          }
        }
        else
        {
          if (InnerTreenumerator.VisitCount == 1)
          {
            _NodePositionAndVisitCounts.RemoveFromFront();
          }
          else if (previousModeWasVisitingNode)
          {
            continue;
          }

          _NodePositionAndVisitCounts[0] = _NodePositionAndVisitCounts[0].IncrementVisitCount();
        }

        UpdateState();

        return true;
      }

      UpdateState();

      _EnumerationFinished = true;

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
        // TODO: I am not sure this block is 100% correct. I would like to add
        // more tests to try and uncover more edge cases here.
        var previousNodePosition = _NodePositionAndVisitCounts[_NodePositionAndVisitCounts.Count - 1].Position;

        effectiveSiblingIndex =
          previousNodePosition.Depth == effectiveDepth
          ? previousNodePosition.SiblingIndex + 1
          : 0;
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
        {
          _SeenRootNodesCount++;
        }

        Node = InnerTreenumerator.Node;
        VisitCount = nodePositionAndVisitCount.VisitCount;
        Position = nodePositionAndVisitCount.Position;
      }
    }

    private readonly struct NodeTraversalStatus
    {
      public NodeTraversalStatus(
        NodePosition position,
        int visitCount,
        NodeTraversalStrategy nodeTraversalStrategy = NodeTraversalStrategy.TraverseSubtree)
      {
        Position = position;
        VisitCount = visitCount;
        TraversalStrategy = nodeTraversalStrategy;
      }

      public NodePosition Position { get; }
      public int VisitCount { get; }
      public NodeTraversalStrategy TraversalStrategy { get; }
      public bool Skipped => TraversalStrategy != NodeTraversalStrategy.TraverseSubtree;

      public NodeTraversalStatus IncrementVisitCount() =>
        new NodeTraversalStatus(Position, VisitCount + 1, TraversalStrategy);

      public NodeTraversalStatus Skip(NodeTraversalStrategy nodeTraversalStrategy) =>
        new NodeTraversalStatus(Position, VisitCount, nodeTraversalStrategy);

      public override string ToString() => $"({Position}), {VisitCount}, {TraversalStrategy}";
    }
  }
}
