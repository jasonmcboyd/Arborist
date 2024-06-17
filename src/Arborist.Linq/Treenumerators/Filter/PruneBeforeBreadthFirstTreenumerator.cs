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
      ITreenumerator<TNode> innerTreenumerator,
      Func<NodeVisit<TNode>, bool> predicate)
      : base(innerTreenumerator)
    {
      _Predicate = predicate;
      _Positions.AddToBack(new NodePositionAndSkippedStatus((0, -1), false));
    }

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;

    private readonly Deque<NodePositionAndSkippedStatus> _Positions = new Deque<NodePositionAndSkippedStatus>();

    private readonly Stack<NodePositionAndSkippedStatus> _CachedPositions = new Stack<NodePositionAndSkippedStatus>();

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
      var previousNodeSkipped =
        InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && traversalStrategy == TraversalStrategy.SkipNode;

      var previousSubtreeSkipped =
        InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && traversalStrategy == TraversalStrategy.SkipSubtree;

      if (previousSubtreeSkipped || previousNodeSkipped)
        _Positions[_Positions.Count - 1] = _Positions.Last().Skip();

      //var previousDepth = InnerTreenumerator.Position.Depth;
      var previousInnerTreenumeratorVisit = InnerTreenumerator.ToNodeVisit();

      while (InnerTreenumerator.MoveNext(traversalStrategy))
      {
        if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        {
          var skipped = _Predicate(InnerTreenumerator.ToNodeVisit());

          if (skipped)
          {
            traversalStrategy = TraversalStrategy.SkipSubtree;

            continue;
          }
          else
          {
            var priorNodePosition = _Positions.Last();

            if (InnerTreenumerator.Position.Depth == priorNodePosition.Depth)
            {
              if (InnerTreenumerator.Position.SiblingIndex > priorNodePosition.SiblingIndex)
              {
                _Positions.AddToBack(new NodePositionAndSkippedStatus(priorNodePosition.SiblingIndex + 1, priorNodePosition.Depth));
              }
              else
              {
                _Positions.AddToBack(new NodePositionAndSkippedStatus(0, InnerTreenumerator.Position.Depth));
              }
            }
            else if (InnerTreenumerator.Position.Depth > priorNodePosition.Depth)
            {
              if (priorNodePosition.Depth != -1)
                _CachedPositions.Push(priorNodePosition);

              _Positions.AddToBack(new NodePositionAndSkippedStatus(0, InnerTreenumerator.Position.Depth));
            }
            else
            {
              if (_CachedPositions.Count > 0)
                priorNodePosition = _CachedPositions.Pop();

              _Positions.AddToBack(new NodePositionAndSkippedStatus(priorNodePosition.SiblingIndex + 1, priorNodePosition.Depth));
            }
          }
        }
        else if (InnerTreenumerator.VisitCount == 1)
        {
          _Positions.RemoveFromFront();

          while (_Positions[0].Skipped)
            _Positions.RemoveFromFront();
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
    
    private readonly struct NodePositionAndSkippedStatus
    {
      public NodePositionAndSkippedStatus(NodePosition nodePosition, bool skipped)
      {
        SiblingIndex = nodePosition.SiblingIndex;
        Depth = nodePosition.Depth;
        Skipped = skipped;
      }

      public NodePositionAndSkippedStatus(int siblingIndex, int depth) : this ((siblingIndex, depth), false)
      {
      }

      public NodePositionAndSkippedStatus(NodePosition nodePosition) : this (nodePosition, false)
      {
      }

      public int SiblingIndex { get; }
      public int Depth { get; }
      public bool Skipped { get; }
      public NodePosition Position => (SiblingIndex, Depth);

      public NodePositionAndSkippedStatus Skip() => new NodePositionAndSkippedStatus((SiblingIndex, Depth), true);

      public override string ToString()
      {
        return $"{SiblingIndex}, {Depth}, {(Skipped ? 'S' : 'N')}";
      }
    }
  }
}
