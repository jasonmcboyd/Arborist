using Arborist.Core;
using Arborist.Linq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Linq.Treenumerators
{
  internal class PruneBeforeBreadthFirstTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public PruneBeforeBreadthFirstTreenumerator(
      Func<ITreenumerator<TNode>> innerTreenumeratorFactory,
      Func<NodeVisit<TNode>, bool> predicate)
      : base(innerTreenumeratorFactory)
    {
      _Predicate = predicate;
      _Positions.AddLast(new NodeTraversalStatus(InnerTreenumerator.Position, 0));
    }

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;

    private readonly RefSemiDeque<NodeTraversalStatus> _Positions = new RefSemiDeque<NodeTraversalStatus>();

    private readonly Stack<NodeTraversalStatus> _CachedPositions = new Stack<NodeTraversalStatus>();

    private bool _EnumerationFinished = false;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (_EnumerationFinished)
        return false;

      if (Mode == TreenumeratorMode.VisitingNode)
        nodeTraversalStrategies = NodeTraversalStrategies.TraverseAll;

      return InnerTreenumeratorMoveNext(nodeTraversalStrategies);
    }

    private bool InnerTreenumeratorMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      var previousNodeSkipped =
        InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && nodeTraversalStrategies == NodeTraversalStrategies.SkipNode;

      var previousSubtreeSkipped =
        InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && nodeTraversalStrategies == NodeTraversalStrategies.SkipNodeAndDescendants;

      if (previousSubtreeSkipped || previousNodeSkipped)
        _Positions.GetLast().Skipped = true;

      var previousInnerTreenumeratorVisit = InnerTreenumerator.ToNodeVisit();

      while (InnerTreenumerator.MoveNext(nodeTraversalStrategies))
      {
        if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        {
          var skipped = _Predicate(InnerTreenumerator.ToNodeVisit());

          if (skipped)
          {
            nodeTraversalStrategies = NodeTraversalStrategies.SkipNodeAndDescendants;

            continue;
          }
          else
          {
            var priorNodePosition = _Positions.Last();

            if (InnerTreenumerator.Position.Depth == priorNodePosition.Position.Depth)
            {
              if (InnerTreenumerator.Position.SiblingIndex > priorNodePosition.Position.SiblingIndex)
                _Positions.AddLast(new NodeTraversalStatus(priorNodePosition.Position + new NodePosition(1, 0), 0));
              else
                _Positions.AddLast(new NodeTraversalStatus(new NodePosition(0, priorNodePosition.Position.Depth), 0));
            }
            else if (InnerTreenumerator.Position.Depth > priorNodePosition.Position.Depth)
            {
              if (priorNodePosition.Position.Depth != -1)
                _CachedPositions.Push(priorNodePosition);

              _Positions.AddLast(new NodeTraversalStatus(new NodePosition(0, InnerTreenumerator.Position.Depth), 0));
            }
            else
            {
              if (_CachedPositions.Count > 0)
                priorNodePosition = _CachedPositions.Pop();

              _Positions.AddLast(new NodeTraversalStatus(priorNodePosition.Position + new NodePosition(1, 0), 0));
            }
          }
        }
        else if (InnerTreenumerator.VisitCount == 1)
        {
          _Positions.RemoveFirst();

          while (_Positions.GetFirst().Skipped)
            _Positions.RemoveFirst();
        }

        UpdateState();

        return true;
      }

      UpdateState();

      _EnumerationFinished = true;

      return false;
    }

    private void UpdateState()
    {
      Mode = InnerTreenumerator.Mode;

      if (!_EnumerationFinished)
      {
        ref var nodeTraversalStatus = ref GetNodeTraversalStatusToUpdateState();

        Node = InnerTreenumerator.Node;
        VisitCount = InnerTreenumerator.VisitCount;
        Position = nodeTraversalStatus.Position;
      }
    }

    private ref NodeTraversalStatus GetNodeTraversalStatusToUpdateState()
    {
      if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        return ref _Positions.GetLast();
      else
        return ref _Positions.GetFirst();
    }

    private struct NodeTraversalStatus
    {
      public NodeTraversalStatus(
        NodePosition position,
        int visitCount,
        bool skipped = false)
      {
        Position = position;
        VisitCount = visitCount;
        Skipped = skipped;
      }

      public NodePosition Position { get; set; }
      public int VisitCount { get; set; }
      public bool Skipped { get; set; }

      public override string ToString() => $"({Position}), {VisitCount}, {Skipped}";
    }
  }
}
