using Arborist.Core;
using Arborist.Linq.Extensions;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class PruneBeforeDepthFirstTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public PruneBeforeDepthFirstTreenumerator(
      Func<ITreenumerator<TNode>> innerTreenumeratorFactory,
      Func<NodeContext<TNode>, bool> predicate)
      : base(innerTreenumeratorFactory)
    {
      _Predicate = predicate;
    }

    private readonly Func<NodeContext<TNode>, bool> _Predicate;

    private readonly List<int> _SkippedSiblingsCounts = new List<int>();

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
      var previousInnerTreenumeratorVisit = InnerTreenumerator.ToNodeVisit();

      while (InnerTreenumerator.MoveNext(nodeTraversalStrategy))
      {
        while (_SkippedSiblingsCounts.Count > InnerTreenumerator.Position.Depth + 2
          || (InnerTreenumerator.Position.Depth == 0 && InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode && _SkippedSiblingsCounts.Count > 1))
        {
          _SkippedSiblingsCounts.RemoveAt(_SkippedSiblingsCounts.Count - 1);
        }

        if (InnerTreenumerator.Position.Depth > previousInnerTreenumeratorVisit.Position.Depth)
        {
          _SkippedSiblingsCounts.Add(0);
        }
        else if (InnerTreenumerator.Position.Depth == previousInnerTreenumeratorVisit.Position.Depth
          && InnerTreenumerator.Position.SiblingIndex > previousInnerTreenumeratorVisit.Position.SiblingIndex)
        {
          while (_SkippedSiblingsCounts.Count > InnerTreenumerator.Position.Depth + 1)
          {
            _SkippedSiblingsCounts.RemoveAt(_SkippedSiblingsCounts.Count - 1);
          }
        }

        if (InnerTreenumerator.Mode == TreenumeratorMode.VisitingNode
          && InnerTreenumerator.VisitCount > 1
          && InnerTreenumerator.Position.Depth == Position.Depth)
        {
          continue;
        }

        if (InnerTreenumerator.Mode == TreenumeratorMode.VisitingNode
          || !_Predicate(InnerTreenumerator.ToNodeContext()))
        {
          UpdateState();

          return true;
        }

        _SkippedSiblingsCounts[InnerTreenumerator.Position.Depth]++;

        nodeTraversalStrategy = NodeTraversalStrategy.SkipSubtree;
      }

      UpdateState();

      _EnumerationFinished = true;

      return false;
    }

    private void UpdateState()
    {
      Mode = InnerTreenumerator.Mode;

      var positionDelta =
        _SkippedSiblingsCounts.Count > 0
        ? new NodePosition(-_SkippedSiblingsCounts[InnerTreenumerator.Position.Depth], 0)
        : new NodePosition(0, 0);

      var visitCountDelta =
        _SkippedSiblingsCounts.Count - 2 == InnerTreenumerator.Position.Depth
        ? _SkippedSiblingsCounts[InnerTreenumerator.Position.Depth + 1]
        : 0;

      if (!_EnumerationFinished)
      {
        Node = InnerTreenumerator.Node;
        VisitCount = InnerTreenumerator.VisitCount - visitCountDelta;
        Position = InnerTreenumerator.Position + positionDelta;
      }
    }
  }
}
