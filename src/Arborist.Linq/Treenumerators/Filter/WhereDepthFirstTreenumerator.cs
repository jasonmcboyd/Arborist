using Arborist.Core;
using Arborist.Linq.Extensions;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class WhereDepthFirstTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public WhereDepthFirstTreenumerator(
      ITreenumerator<TNode> innerTreenumerator,
      Func<NodeVisit<TNode>, bool> predicate)
      : base(innerTreenumerator)
    {
      _Predicate = predicate;

      // Add a sentinel node to the stack.
      _NodeVisits.Push(InnerTreenumerator.ToNodeVisit().IncrementVisitCount());
    }

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;

    private readonly Stack<NodeVisit<TNode>> _NodeVisits = new Stack<NodeVisit<TNode>>();
    private readonly Stack<NodeVisit<TNode>> _SkippedNodeVisits = new Stack<NodeVisit<TNode>>();

    private bool _EnumerationFinished = false;

    private int _DepthOfLastYieldedNode = -1;

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
      var previousNodeVisit = InnerTreenumerator.ToNodeVisit();

      // Do not apply any traversal strategies to the sentinel node.
      if (InnerTreenumerator.Position.Depth == -1)
        traversalStrategy = TraversalStrategy.TraverseSubtree;

      // Enumerate until we yield something or exhaust the inner enumerator.
      while (InnerTreenumerator.MoveNext(traversalStrategy))
      {
        var currentNodeVisit = InnerTreenumerator.ToNodeVisit();

        if (currentNodeVisit.Mode == TreenumeratorMode.SchedulingNode)
        {
          if (OnScheduling(currentNodeVisit, previousNodeVisit))
            return true;
        }
        else
        {
          if (OnTraversing(currentNodeVisit, previousNodeVisit))
            return true;
        }

        previousNodeVisit = currentNodeVisit;
      }

      // If we are here we have exhausted the inner enumerator.
      UpdateState();

      _EnumerationFinished = true;

      return false;
    }

    private bool OnScheduling(
      NodeVisit<TNode> currentNodeVisit,
      NodeVisit<TNode> previousNodeVisit)
    {
      var isCurrentNodeSiblingOfPreviousNode = previousNodeVisit.Position.Depth == currentNodeVisit.Position.Depth;

      // Check if the current node visit should be skipped.
      if (!_Predicate(currentNodeVisit))
      {
        // If the current node is a sibling of the previous node visited we need
        // to pop the previous node visit off one of the stacks.
        if (isCurrentNodeSiblingOfPreviousNode)
          PopMostRecentNodeVisitOnStacks();

        // Push the current node visit onto the skipped node visits stack.
        _SkippedNodeVisits.Push(currentNodeVisit);

        return false;
      }

      // Need to calculate the sibling index of the current node visit.
      // Start by assuming the sibling index is one less than the visit
      // count of the parent index.
      var siblingIndex = _NodeVisits.Peek().VisitCount - 1;

      // If the current node is a sibling of the previous node visited we need
      // to figure out which stack the previous visit was pushed to and pop it off.
      // We can use the _SkippedNodeVisists stack to figure out which stack it was
      // pushed to because node visits in this stack to not have their positions
      // altered.
      //
      // We may also need to use the previous sibling to calculate the sibling index
      // of the current node visit.
      if (isCurrentNodeSiblingOfPreviousNode)
      {
        if (IsNodeSkipped(previousNodeVisit))
        {
          _SkippedNodeVisits.Pop();
        }
        else
        {
          siblingIndex = _NodeVisits.Pop().Position.SiblingIndex + 1;
        }
      }

      // The effective depth is the depth of the current node visits on the stack
      // minus the sentinel node.
      var effectiveDepth = _NodeVisits.Count - 1;

      // If the effective depth is 0, we need to increment the visit count of the
      // sentinel node visit.
      if (effectiveDepth == 0)
        _NodeVisits.Push(_NodeVisits.Pop().IncrementVisitCount());

      // Push the current node visit onto the stack.
      _NodeVisits.Push(currentNodeVisit.WithSiblingIndex(siblingIndex));

      UpdateState();

      return true;
    }

    private bool OnTraversing(
      NodeVisit<TNode> currentNodeVisit,
      NodeVisit<TNode> previousNodeVisit)
    {
      if (currentNodeVisit.Position.Depth < previousNodeVisit.Position.Depth)
      {
        // Pop the previous node of the stacks.
        PopMostRecentNodeVisitOnStacks();

        // Do not yield if the current node was skipped.
        if (IsNodeSkipped(currentNodeVisit)
          || _DepthOfLastYieldedNode <= currentNodeVisit.Position.Depth)
        {
          return false;
        }

        // Increment the visit count of the current node visit.
        _NodeVisits.Push(_NodeVisits.Pop().IncrementVisitCount());
      }
      else if (currentNodeVisit.Position.Depth > previousNodeVisit.Position.Depth)
      {
        if (IsNodeSkipped(currentNodeVisit))
        {
          _SkippedNodeVisits.Push(currentNodeVisit);

          return false;
        }

        _NodeVisits.Push(currentNodeVisit);
      }
      else
      {
        if (IsNodeSkipped(currentNodeVisit))
          return false;

        _NodeVisits.Push(_NodeVisits.Pop().IncrementVisitCount());
      }

      UpdateState();

      return true;
    }

    private bool IsNodeSkipped(NodeVisit<TNode> nodeVisit)
    {
      return
        _SkippedNodeVisits.Count > 0
        && _SkippedNodeVisits.Peek().Position.Depth == nodeVisit.Position.Depth;
    }

    private void PopMostRecentNodeVisitOnStacks()
    {
      if (_SkippedNodeVisits.Count > 0
        && _SkippedNodeVisits.Peek().Position.Depth > _NodeVisits.Peek().Position.Depth)
      {
        _SkippedNodeVisits.Pop();
      }
      else
      {
        _NodeVisits.Pop();
      }
    }

    private void UpdateState()
    {
      Mode = InnerTreenumerator.Mode;

      _DepthOfLastYieldedNode = InnerTreenumerator.Position.Depth;

      var depthDelta = -1 * _SkippedNodeVisits.Count;

      var visit = _NodeVisits.Peek();

      if (!_EnumerationFinished)
      {
        Node = InnerTreenumerator.Node;
        VisitCount = visit.VisitCount;
        Position = (visit.Position.SiblingIndex, visit.Position.Depth + depthDelta);
      }
    }
  }
}
