using Arborist.Core;
using Nito.Collections;
using System;

namespace Arborist.Linq.Treenumerators
{
  internal class StructuralMergeBreadthFirstTreenumerator<TLeft, TRight>
    : TreenumeratorBase<MergeNode<TLeft, TRight>>
  {
    public StructuralMergeBreadthFirstTreenumerator(
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

    private Deque<NodeVisit<MergeNode<TLeft, TRight>>> _NodeVisits = new Deque<NodeVisit<MergeNode<TLeft, TRight>>>();

    protected override bool OnMoveNext(NodeTraversalStrategy nodeTraversalStrategy)
    {
      if (Mode == TreenumeratorMode.SchedulingNode
        && (nodeTraversalStrategy == NodeTraversalStrategy.SkipNode
          || nodeTraversalStrategy == NodeTraversalStrategy.SkipSubtree))
      {
        _NodeVisits.RemoveFromBack();
      }

      HandleMoveNextForLeftAndRightTreenumerators(nodeTraversalStrategy);

      if (_BothTreenumeratorsFinished)
        return false;

      var nodeVisit = CreateNextNodeVisit();

      if (nodeVisit.Mode == TreenumeratorMode.SchedulingNode)
        _NodeVisits.AddToBack(nodeVisit);
      else
        _NodeVisits[0] = nodeVisit;

      UpdateStateFromNodeVisit(nodeVisit);

      return true;
    }

    private void HandleMoveNextForLeftAndRightTreenumerators(NodeTraversalStrategy nodeTraversalStrategy)
    {
      var callMoveNextOnLeftTreenumerator = false;
      var callMoveNextOnRightTreenumerator = false;

      var leftTreenumeratorModeIsScheduling = _LeftTreenumerator.Mode == TreenumeratorMode.SchedulingNode;
      var rightTreenumeratorModeIsScheduling = _RightTreenumerator.Mode == TreenumeratorMode.SchedulingNode;

      if (leftTreenumeratorModeIsScheduling && rightTreenumeratorModeIsScheduling)
      {
        if (_LeftTreenumerator.Position >= _RightTreenumerator.Position)
          callMoveNextOnLeftTreenumerator = true;

        if (_RightTreenumerator.Position >= _LeftTreenumerator.Position)
          callMoveNextOnRightTreenumerator = true;
      }
      else if (leftTreenumeratorModeIsScheduling && !rightTreenumeratorModeIsScheduling)
      {
        callMoveNextOnLeftTreenumerator = true;
      }
      else if (!leftTreenumeratorModeIsScheduling && rightTreenumeratorModeIsScheduling)
      {
        callMoveNextOnRightTreenumerator = true;
      }
      else
      {
        if (Node.HasLeft && _LeftTreenumerator.Position == _NodeVisits[0].Position)
          callMoveNextOnLeftTreenumerator = true;

        if (Node.HasRight && _RightTreenumerator.Position == _NodeVisits[0].Position)
          callMoveNextOnRightTreenumerator = true;
      }

      if (callMoveNextOnLeftTreenumerator
        && !_LeftTreenumerator.MoveNext(nodeTraversalStrategy))
      {
        _LeftTreenumeratorFinished = true;
      }

      if (callMoveNextOnRightTreenumerator
        && !_RightTreenumerator.MoveNext(nodeTraversalStrategy))
      {
        _RightTreenumeratorFinished = true;
      }
    }

    private NodeVisit<MergeNode<TLeft, TRight>> CreateNextNodeVisit()
    {
      var leftTreenumeratorModeIsScheduling =
        !_LeftTreenumeratorFinished
        && _LeftTreenumerator.Mode == TreenumeratorMode.SchedulingNode;

      var rightTreenumeratorModeIsScheduling =
        !_RightTreenumeratorFinished
        && _RightTreenumerator.Mode == TreenumeratorMode.SchedulingNode;

      var scheduleLeft = false;
      var scheduleRight = false;

      if (leftTreenumeratorModeIsScheduling && rightTreenumeratorModeIsScheduling)
      {
        if (_LeftTreenumerator.Position >= _RightTreenumerator.Position)
          scheduleLeft = true;

        if (_RightTreenumerator.Position >= _LeftTreenumerator.Position)
          scheduleRight = true;
      }
      else if (leftTreenumeratorModeIsScheduling && !rightTreenumeratorModeIsScheduling)
      {
        scheduleLeft = true;
      }
      else if (!leftTreenumeratorModeIsScheduling && rightTreenumeratorModeIsScheduling)
      {
        scheduleRight = true;
      }
      else
      {
        var hasLeftAndLeftVisitCountGreaterThanLeadNodeVisitCount =
          _NodeVisits[0].Node.HasLeft
          && _LeftTreenumerator.VisitCount > _NodeVisits[0].VisitCount;

        var hasRightAndRightVisitCountGreaterThanLeadNodeVisitCount =
          _NodeVisits[0].Node.HasRight
          && _RightTreenumerator.VisitCount > _NodeVisits[0].VisitCount;

        if (!hasLeftAndLeftVisitCountGreaterThanLeadNodeVisitCount
          && !hasRightAndRightVisitCountGreaterThanLeadNodeVisitCount)
        {
          _NodeVisits.RemoveFromFront();
        }
      }

      return
        scheduleLeft || scheduleRight
        ? CreateSchedulingNodeVisit(scheduleLeft, scheduleRight)
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
      var nodeVisit =
        new NodeVisit<MergeNode<TLeft, TRight>>(
          TreenumeratorMode.VisitingNode,
          _NodeVisits[0].Node,
          _NodeVisits[0].VisitCount + 1,
          _NodeVisits[0].Position);

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
