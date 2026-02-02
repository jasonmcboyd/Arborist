using Arborist.Core;
using Arborist.Linq.Extensions;
using Nito.Collections;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class RootfixScanBreadthFirstTreenumerator<TNode, TAccumulate>
    : TreenumeratorWrapper<TNode, TAccumulate>
  {
    public RootfixScanBreadthFirstTreenumerator(
      Func<ITreenumerator<TNode>> innerTreenumeratorFactory,
      Func<NodeContext<TAccumulate>, NodeContext<TNode>, TAccumulate> accumulator,
      TAccumulate seed) : base(innerTreenumeratorFactory)
    {
      _Accumulator = accumulator;

      var seedVisit =
        new NodeVisit<TAccumulate>(
          TreenumeratorMode.VisitingNode,
          seed,
          1,
          new NodePosition(0, -1));

      _CurrentLevel.AddToBack(seedVisit);
    }

    private readonly Func<NodeContext<TAccumulate>, NodeContext<TNode>, TAccumulate> _Accumulator;

    private Deque<NodeVisit<TAccumulate>> _CurrentLevel = new Deque<NodeVisit<TAccumulate>>();
    private Deque<NodeVisit<TAccumulate>> _NextLevel = new Deque<NodeVisit<TAccumulate>>();

    private Stack<NodeVisit<TAccumulate>> _SkippedStack = new Stack<NodeVisit<TAccumulate>>();

    // Tracks whether we've scheduled any children since the last node was pushed to _SkippedStack.
    // This helps detect when we've moved to scheduling children of a different parent.
    private bool _ScheduledChildrenSinceSkip = false;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (Mode == TreenumeratorMode.SchedulingNode)
      {
        if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNodeAndDescendants))
          _NextLevel.RemoveFromBack();
        else if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode))
        {
          _SkippedStack.Push(_NextLevel.RemoveFromBack());
          _ScheduledChildrenSinceSkip = false;
        }
      }

      if (!InnerTreenumerator.MoveNext(nodeTraversalStrategies))
        return false;

      if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        OnSchedulingNode();
      else
        OnVisitingNode();

      return true;
    }

    private void OnSchedulingNode()
    {
      var parentDepth = InnerTreenumerator.Position.Depth - 1;

      // Pop skipped items that are not the immediate parent
      while (_SkippedStack.Count > 0
        && _SkippedStack.Peek().Position.Depth != parentDepth)
      {
        _SkippedStack.Pop();
      }

      // When sibling index is 0 and we've already scheduled some children from the skipped node,
      // we've moved to a different parent's children. Pop from the skipped stack.
      if (InnerTreenumerator.Position.SiblingIndex == 0
        && _ScheduledChildrenSinceSkip
        && _SkippedStack.Count > 0)
      {
        _SkippedStack.Pop();
        _ScheduledChildrenSinceSkip = false;
      }

      // Find the parent node visit:
      // 1. If _CurrentLevel[0] has parent depth, use it (we're currently visiting the parent)
      // 2. Else if skipped stack has an item at parent depth, use it (parent was skipped)
      // 3. Else if _NextLevel has items at parent depth, use the first one
      //    (this happens when grandparent was skipped but parent wasn't yet visited)
      // 4. Else use _CurrentLevel[0] (for root nodes, parent is seed at depth -1)
      NodeVisit<TAccumulate> accumulateNodeVisit;
      if (_CurrentLevel[0].Position.Depth == parentDepth)
        accumulateNodeVisit = _CurrentLevel[0];
      else if (_SkippedStack.Count > 0)
        accumulateNodeVisit = _SkippedStack.Peek();
      else if (_NextLevel.Count > 0 && _NextLevel[0].Position.Depth == parentDepth)
        accumulateNodeVisit = _NextLevel[0];
      else
        accumulateNodeVisit = _CurrentLevel[0];

      var node = _Accumulator(accumulateNodeVisit.ToNodeContext(), InnerTreenumerator.ToNodeContext());

      var visit = InnerTreenumerator.ToNodeVisit().WithNode(node);

      _NextLevel.AddToBack(visit);

      UpdateStateFromNodeVisit(visit);

      _ScheduledChildrenSinceSkip = true;
    }

    private void OnVisitingNode()
    {
      if (InnerTreenumerator.VisitCount == 1)
      {
        _SkippedStack.Clear();
        _CurrentLevel.RemoveFromFront();

        if (_CurrentLevel.Count == 0)
          (_CurrentLevel, _NextLevel) = (_NextLevel, _CurrentLevel);

        // Remove items that were skipped by the inner treenumerator
        // (e.g., due to SkipSiblings affecting earlier siblings)
        while (_CurrentLevel.Count > 0
          && _CurrentLevel[0].Position != InnerTreenumerator.Position)
        {
          _CurrentLevel.RemoveFromFront();
        }
      }

      var visit = _CurrentLevel[0];

      var newVisit =
        new NodeVisit<TAccumulate>(
          InnerTreenumerator.Mode,
          visit.Node,
          InnerTreenumerator.VisitCount,
          InnerTreenumerator.Position);

      _CurrentLevel[0] = newVisit;

      UpdateStateFromNodeVisit(newVisit);
    }

    private void UpdateStateFromNodeVisit(NodeVisit<TAccumulate> nodeVisit)
    {
      Mode = nodeVisit.Mode;
      Node = nodeVisit.Node;
      VisitCount = nodeVisit.VisitCount;
      Position = nodeVisit.Position;
    }
  }
}
