using Arborist.Core;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class StructuralMergeDepthFirstTreenumerator<TLeft, TRight>
    : TreenumeratorBase<MergeNode<TLeft, TRight>>
  {
    public StructuralMergeDepthFirstTreenumerator(
      Func<ITreenumerator<TLeft>> leftTreenumeratorFactory,
      Func<ITreenumerator<TRight>> rightTreenumeratorFactory)
    {
      _LeftTreenumerator = leftTreenumeratorFactory();
      _RightTreenumerator = rightTreenumeratorFactory();
    }

    private readonly ITreenumerator<TLeft> _LeftTreenumerator;
    private readonly ITreenumerator<TRight> _RightTreenumerator;

    private bool _LeftTreenumeratorFinished;
    private bool _RightTreenumeratorFinished;

    private bool _BothTreenumeratorsFinished => _LeftTreenumeratorFinished && _RightTreenumeratorFinished;

    private Stack<NodeVisit<MergeNode<TLeft, TRight>>> _NodeVisits = new Stack<NodeVisit<MergeNode<TLeft, TRight>>>();

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      HandleMoveNextForLeftAndRightTreenumerators(nodeTraversalStrategies);

      if (_BothTreenumeratorsFinished)
        return false;

      FixUpNodeVisitsStack(nodeTraversalStrategies);

      var nodeVisit = CreateNextNodeVisit();

      _NodeVisits.Push(nodeVisit);

      UpdateStateFromNodeVisit(nodeVisit);

      return true;
    }

    private void HandleMoveNextForLeftAndRightTreenumerators(NodeTraversalStrategies nodeTraversalStrategies)
    {
      var callMoveNextOnLeftTreenumerator =
        !_LeftTreenumeratorFinished
        && (_NodeVisits.Count == 0
          || (Node.HasLeft && _LeftTreenumerator.Position == Position));

      var callMoveNextOnRightTreenumerator =
        !_RightTreenumeratorFinished
        && (_NodeVisits.Count == 0
          || (Node.HasRight && _RightTreenumerator.Position == Position));

      if (callMoveNextOnLeftTreenumerator
        && !_LeftTreenumerator.MoveNext(nodeTraversalStrategies))
      {
        _LeftTreenumeratorFinished = true;
      }

      if (callMoveNextOnRightTreenumerator
        && !_RightTreenumerator.MoveNext(nodeTraversalStrategies))
      {
        _RightTreenumeratorFinished = true;
      }
    }

    private void FixUpNodeVisitsStack(NodeTraversalStrategies nodeTraversalStrategies)
    {
      var hasLeft = Node.HasLeft && !_LeftTreenumeratorFinished;
      var hasRight = Node.HasRight && !_RightTreenumeratorFinished;
      var leftDepthIsLessThanCurrent = _LeftTreenumerator.Position.Depth < Position.Depth;
      var rightDepthIsLessThanCurrent = _RightTreenumerator.Position.Depth < Position.Depth;

      var mustPopLeadingNodeVisitOnStack =
        nodeTraversalStrategies.HasFlag(NodeTraversalStrategies.SkipNode)
        || (hasLeft && hasRight && leftDepthIsLessThanCurrent && rightDepthIsLessThanCurrent)
        || (!hasLeft && _RightTreenumerator.Position.Depth < Position.Depth)
        || (!hasRight && _LeftTreenumerator.Position.Depth < Position.Depth);

      if (mustPopLeadingNodeVisitOnStack)
        _NodeVisits.Pop();
    }

    private NodeVisit<MergeNode<TLeft, TRight>> CreateNextNodeVisit()
    {
      var nextNodeVisitHasLeft =
        !_LeftTreenumeratorFinished
          && (_RightTreenumeratorFinished
            || _LeftTreenumerator.Position.Depth > _RightTreenumerator.Position.Depth
            || (_LeftTreenumerator.Position.Depth == _RightTreenumerator.Position.Depth
              && _LeftTreenumerator.Position.SiblingIndex <= _RightTreenumerator.Position.SiblingIndex));

      var nextNodeVisitHasRight =
        !_RightTreenumeratorFinished
          && (_LeftTreenumeratorFinished
            || _RightTreenumerator.Position.Depth > _LeftTreenumerator.Position.Depth
            || (_RightTreenumerator.Position.Depth == _LeftTreenumerator.Position.Depth
              && _RightTreenumerator.Position.SiblingIndex <= _LeftTreenumerator.Position.SiblingIndex));

      var createSchedulingNodeVisit =
        (nextNodeVisitHasLeft && _LeftTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        || (nextNodeVisitHasRight && _RightTreenumerator.Mode == TreenumeratorMode.SchedulingNode);

      return
        createSchedulingNodeVisit
        ? CreateSchedulingNodeVisit(nextNodeVisitHasLeft, nextNodeVisitHasRight)
        : RevisitLeadNodeVisitInQueue();
    }

    private NodeVisit<MergeNode<TLeft, TRight>> CreateSchedulingNodeVisit(
      bool includeLeft,
      bool includeRight)
    {
      var node =
        new MergeNode<TLeft, TRight>(
          includeLeft ? _LeftTreenumerator.Node : default,
          includeRight ? _RightTreenumerator.Node : default,
          includeLeft,
          includeRight);

      var nodeVisit =
        new NodeVisit<MergeNode<TLeft, TRight>>(
          TreenumeratorMode.SchedulingNode,
          node,
          includeLeft ? _LeftTreenumerator.VisitCount : _RightTreenumerator.VisitCount,
          includeLeft ? _LeftTreenumerator.Position : _RightTreenumerator.Position);

      return nodeVisit;
    }

    private NodeVisit<MergeNode<TLeft, TRight>> RevisitLeadNodeVisitInQueue()
    {
      var nodeVisit = _NodeVisits.Pop();

      nodeVisit =
        new NodeVisit<MergeNode<TLeft, TRight>>(
          TreenumeratorMode.VisitingNode,
          nodeVisit.Node,
          nodeVisit.VisitCount + 1,
          nodeVisit.Position);

      return nodeVisit;
    }

    private void UpdateStateFromNodeVisit(NodeVisit<MergeNode<TLeft, TRight>> nodeVisit)
    {
      Node = nodeVisit.Node;
      VisitCount = nodeVisit.VisitCount;
      Mode = nodeVisit.Mode;
      Position = nodeVisit.Position;
    }

    public override void Dispose()
    {
      _LeftTreenumerator?.Dispose();
      _RightTreenumerator?.Dispose();
    }
  }
}
