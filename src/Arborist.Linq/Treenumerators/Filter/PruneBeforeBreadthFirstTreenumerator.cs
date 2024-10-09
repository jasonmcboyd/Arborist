using Arborist.Core;
using Arborist.Linq.Extensions;
using Nito.Collections;
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
      _Positions.AddToBack(new NodeTraversalStatus(InnerTreenumerator.Position, 0));
    }

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;

    private readonly Deque<NodeTraversalStatus> _Positions = new Deque<NodeTraversalStatus>();

    private readonly Stack<NodeTraversalStatus> _CachedPositions = new Stack<NodeTraversalStatus>();

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
      var previousNodeSkipped =
        InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && nodeTraversalStrategy == NodeTraversalStrategy.SkipNode;

      var previousSubtreeSkipped =
        InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && nodeTraversalStrategy == NodeTraversalStrategy.SkipSubtree;

      if (previousSubtreeSkipped || previousNodeSkipped)
      {
        _Positions[_Positions.Count - 1] = _Positions.Last().Skip();
      }

      //var previousDepth = InnerTreenumerator.Position.Depth;
      var previousInnerTreenumeratorVisit = InnerTreenumerator.ToNodeVisit();

      while (InnerTreenumerator.MoveNext(nodeTraversalStrategy))
      {
        if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        {
          var skipped = _Predicate(InnerTreenumerator.ToNodeVisit());

          if (skipped)
          {
            nodeTraversalStrategy = NodeTraversalStrategy.SkipSubtree;

            continue;
          }
          else
          {
            var priorNodePosition = _Positions.Last();

            if (InnerTreenumerator.Position.Depth == priorNodePosition.Position.Depth)
            {
              if (InnerTreenumerator.Position.SiblingIndex > priorNodePosition.Position.SiblingIndex)
              {
                _Positions.AddToBack(new NodeTraversalStatus(priorNodePosition.Position + (1, 0), 0));
              }
              else
              {
                _Positions.AddToBack(new NodeTraversalStatus((0, priorNodePosition.Position.Depth), 0));
              }
            }
            else if (InnerTreenumerator.Position.Depth > priorNodePosition.Position.Depth)
            {
              if (priorNodePosition.Position.Depth != -1)
              {
                _CachedPositions.Push(priorNodePosition);
              }

              _Positions.AddToBack(new NodeTraversalStatus((0, InnerTreenumerator.Position.Depth), 0));
            }
            else
            {
              if (_CachedPositions.Count > 0)
              {
                priorNodePosition = _CachedPositions.Pop();
              }

              _Positions.AddToBack(new NodeTraversalStatus(priorNodePosition.Position + (1, 0), 0));
            }
          }
        }
        else if (InnerTreenumerator.VisitCount == 1)
        {
          _Positions.RemoveFromFront();

          while (_Positions[0].Skipped)
          {
            _Positions.RemoveFromFront();
          }
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
        var position =
          InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
          ? _Positions.Last()
          : _Positions[0];

        Node = InnerTreenumerator.Node;
        VisitCount = InnerTreenumerator.VisitCount;
        Position = position.Position;
      }
    }

    private readonly struct NodeTraversalStatus
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

      public NodePosition Position { get; }
      public int VisitCount { get; }
      public bool Skipped { get; }

      public NodeTraversalStatus IncrementVisitCount() =>
        new NodeTraversalStatus(Position, VisitCount + 1, Skipped);

      public NodeTraversalStatus Skip() =>
        new NodeTraversalStatus(Position, VisitCount, true);

      public override string ToString() => $"({Position}), {VisitCount}, {Skipped}";
    }
  }
}
