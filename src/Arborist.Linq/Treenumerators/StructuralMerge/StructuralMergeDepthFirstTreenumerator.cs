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

    private bool _LeftSiblingSkipPending = false;
    private bool _RightSiblingSkipPending = false;
    private int _SiblingSkipDepth = -1;

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
      // When SkipSiblings is received and the node is at "effective root" level
      // (all ancestors were skipped), propagate the skip to the other treenumerator.
      // This mirrors the _Stack.Count == 1 check in plain DepthFirstTreenumerator.
      // We check if there are any non-skipped ancestors by looking for nodes at a
      // shallower depth than the current node in the _NodeVisits stack.
      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipSiblings)
        && !HasAncestorInNodeVisits())
      {
        _SiblingSkipDepth = Position.Depth;

        if (!Node.HasLeft)
          _LeftSiblingSkipPending = true;

        if (!Node.HasRight)
          _RightSiblingSkipPending = true;
      }

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

      // Apply pending sibling skips to treenumerators that weren't called above
      if (_LeftSiblingSkipPending
        && !_LeftTreenumeratorFinished
        && _LeftTreenumerator.Position.Depth <= _SiblingSkipDepth)
      {
        if (!_LeftTreenumerator.MoveNext(NodeTraversalStrategies.SkipNodeAndDescendants | NodeTraversalStrategies.SkipSiblings))
          _LeftTreenumeratorFinished = true;

        _LeftSiblingSkipPending = false;
      }

      if (_RightSiblingSkipPending
        && !_RightTreenumeratorFinished
        && _RightTreenumerator.Position.Depth <= _SiblingSkipDepth)
      {
        if (!_RightTreenumerator.MoveNext(NodeTraversalStrategies.SkipNodeAndDescendants | NodeTraversalStrategies.SkipSiblings))
          _RightTreenumeratorFinished = true;

        _RightSiblingSkipPending = false;
      }
    }

    private void FixUpNodeVisitsStack(NodeTraversalStrategies nodeTraversalStrategies)
    {
      // Pop the current node if it was skipped
      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode))
        _NodeVisits.Pop();

      // Keep popping while the top of the stack is deeper than where the treenumerators are.
      // This handles cases where the treenumerators have backtracked multiple levels
      // (e.g., when a deep node is skipped and its ancestors have no more children).
      while (_NodeVisits.Count > 0)
      {
        var topDepth = _NodeVisits.Peek().Position.Depth;
        var leftDepth = _LeftTreenumeratorFinished ? -1 : _LeftTreenumerator.Position.Depth;
        var rightDepth = _RightTreenumeratorFinished ? -1 : _RightTreenumerator.Position.Depth;
        var maxTreenumeratorDepth = Math.Max(leftDepth, rightDepth);

        // If both treenumerators are at a shallower depth than the top of the stack,
        // the top node has been fully processed and should be popped.
        if (maxTreenumeratorDepth < topDepth)
          _NodeVisits.Pop();
        else
          break;
      }
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

    private bool HasAncestorInNodeVisits()
    {
      // Check if there are any nodes in _NodeVisits at a depth shallower than
      // the current node's depth. If not, all ancestors were skipped.
      foreach (var nodeVisit in _NodeVisits)
      {
        if (nodeVisit.Position.Depth < Position.Depth)
          return true;
      }
      return false;
    }

    protected override void OnDisposing()
    {
      base.OnDisposing();

      _LeftTreenumerator?.Dispose();
      _RightTreenumerator?.Dispose();
    }
  }
}
