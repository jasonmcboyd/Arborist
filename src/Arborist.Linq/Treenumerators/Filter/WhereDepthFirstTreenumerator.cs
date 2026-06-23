using Arborist.Core;
using Arborist.Linq.Extensions;
using System;
using System.Runtime.CompilerServices;

namespace Arborist.Linq.Treenumerators
{
  internal class WhereDepthFirstTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public WhereDepthFirstTreenumerator(
      Func<ITreenumerator<TNode>> innerTreenumeratorFactory,
      Func<NodeContext<TNode>, bool> predicate,
      NodeTraversalStrategies nodeTraversalStrategy)
      : base(innerTreenumeratorFactory)
    {
      _Predicate = predicate;
      _NodeTraversalStrategy = nodeTraversalStrategy;

      // Add a sentinel node to the stack.
      _NodeVisits.AddLast(new InternalNodeVisit(InnerTreenumerator));
      _NodeVisits.GetLast().VisitCount = 1;
    }

    private readonly Func<NodeContext<TNode>, bool> _Predicate;
    private readonly NodeTraversalStrategies _NodeTraversalStrategy;

    private readonly RefSemiDeque<InternalNodeVisit> _NodeVisits = new RefSemiDeque<InternalNodeVisit>();
    private readonly RefSemiDeque<InternalNodeVisit> _SkippedNodeVisits = new RefSemiDeque<InternalNodeVisit>();

    private int _DepthOfLastSeenNode = -1;
    private int _DepthOfLastVisitedNode = -1;
    private bool _HasCachedChild = false;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (_HasCachedChild)
      {
        _HasCachedChild = false;

        UpdateStateFromNodeVisit(ref _NodeVisits.GetLast());

        return true;
      }

      // If the consumer skipped the node we just scheduled, move it to the
      // skipped stack so its descendants get promoted. The most recently
      // scheduled node is the top of _NodeVisits. We must never move the
      // sentinel (the only node present when _NodeVisits.Count == 1): it is a
      // virtual parent, not a node we ever yielded, so an external skip cannot
      // apply to it.
      if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && _NodeVisits.Count > 1
        && nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode))
      {
        _SkippedNodeVisits.AddLast(_NodeVisits.RemoveLast());
      }

      if (Mode == TreenumeratorMode.VisitingNode)
        nodeTraversalStrategies = NodeTraversalStrategies.TraverseAll;

      // Do not apply any traversal strategies to the sentinel node.
      if (InnerTreenumerator.Position.Depth == -1)
        nodeTraversalStrategies = NodeTraversalStrategies.TraverseAll;

      // Enumerate until we yield something or exhaust the inner enumerator.
      while (InnerTreenumerator.MoveNext(nodeTraversalStrategies))
      {
        // Reset the traversal strategy.
        nodeTraversalStrategies = NodeTraversalStrategies.TraverseAll;

        if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        {
          if (!OnScheduling())
          {
            nodeTraversalStrategies = _NodeTraversalStrategy;
            continue;
          }

          return true;
        }

        if (OnVisiting())
          return true;
      }

      return false;
    }

    private bool OnScheduling()
    {
      var stackWithDeepestNodeVisit = GetStackWithDeepestNodeVisit();

      var traversingAwayFromRootNode = InnerTreenumerator.Position.Depth > stackWithDeepestNodeVisit.GetLast().OriginalPosition.Depth;

      if (!traversingAwayFromRootNode)
      {
        while (_SkippedNodeVisits.Count > 0
          && _SkippedNodeVisits.GetLast().OriginalPosition.Depth >= InnerTreenumerator.Position.Depth)
        {
          _SkippedNodeVisits.RemoveLast();
        }

        while (_NodeVisits.GetLast().OriginalPosition.Depth >= InnerTreenumerator.Position.Depth)
         _NodeVisits.RemoveLast();

        stackWithDeepestNodeVisit = GetStackWithDeepestNodeVisit();

        if (stackWithDeepestNodeVisit == _SkippedNodeVisits)
        {
          // Increment the skipped node's visit count to ensure
          // the sibling index calculation is correct.
          _SkippedNodeVisits.GetLast().VisitCount++;
        }
        else if (_NodeVisits.Count == 1)
        {
          // Increment the sentinel node's visit count to ensure
          // the sibling index calculation is correct.
          _NodeVisits.GetLast().VisitCount++;
        }
      }

      // Check if the current node visit should be skipped.
      if (!_Predicate(InnerTreenumerator.ToNodeContext()))
        return false;

      ref var previousVisit = ref stackWithDeepestNodeVisit.GetLast();

      var depth = _NodeVisits.Count + _SkippedNodeVisits.Count - 1;
      var siblingIndex = previousVisit.CurrentChildIndex;

      previousVisit.CurrentChildIndex++;

      // TODO: When should I cache?
      var cacheChild =
        stackWithDeepestNodeVisit != _SkippedNodeVisits
        && _NodeVisits.Count > 1
        && _DepthOfLastVisitedNode > _NodeVisits.GetLast().OriginalPosition.Depth;

      var nodeVisit =
        new InternalNodeVisit(
          InnerTreenumerator.Node,
          InnerTreenumerator.Position,
          new NodePosition(siblingIndex, depth),
          0,
          0);

      _NodeVisits.AddLast(nodeVisit);

      if (cacheChild)
      {
        ref var parentNodeVisit = ref _NodeVisits.GetFromBack(1);
        _HasCachedChild = true;
        parentNodeVisit.VisitCount++;
        UpdateStateFromNodeVisit(ref parentNodeVisit);
      }
      else
      {
        UpdateStateFromNodeVisit(ref _NodeVisits.GetLast());
      }

      return true;
    }

    private bool OnVisiting()
    {
      var removedVisitedNodes = false;
      var removedSkippedNodes = false;

      while (_NodeVisits.GetLast().OriginalPosition.Depth > InnerTreenumerator.Position.Depth)
      {
        ref var removedNode = ref _NodeVisits.RemoveLast();
        removedVisitedNodes |= removedNode.VisitCount > 0;
      }

      while (_SkippedNodeVisits.Count > 0 && _SkippedNodeVisits.GetLast().OriginalPosition.Depth > InnerTreenumerator.Position.Depth)
      {
        _SkippedNodeVisits.RemoveLast();
        removedSkippedNodes = true;
      }

      ref var nodeVisit = ref _NodeVisits.GetLast();

      if (nodeVisit.OriginalPosition == InnerTreenumerator.Position
        && nodeVisit.OriginalPosition.Depth >= _DepthOfLastVisitedNode
        && !removedVisitedNodes)
      {
        if (removedSkippedNodes)
          return false;

        if (_DepthOfLastSeenNode > nodeVisit.OriginalPosition.Depth)
          return false;
      }

      nodeVisit.VisitCount++;

      UpdateStateFromNodeVisit(ref _NodeVisits.GetLast());

      return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RefSemiDeque<InternalNodeVisit> GetStackWithDeepestNodeVisit()
    {
      return
        _SkippedNodeVisits.Count == 0 || _NodeVisits.GetLast().OriginalPosition.Depth > _SkippedNodeVisits.GetLast().OriginalPosition.Depth
        ? _NodeVisits
        : _SkippedNodeVisits;
    }

    private void UpdateStateFromNodeVisit(ref InternalNodeVisit nodeVisit)
    {
      Mode = nodeVisit.VisitCount == 0 ? TreenumeratorMode.SchedulingNode : TreenumeratorMode.VisitingNode;

      _DepthOfLastSeenNode = nodeVisit.OriginalPosition.Depth;

      if (Mode == TreenumeratorMode.VisitingNode)
        _DepthOfLastVisitedNode = nodeVisit.OriginalPosition.Depth;

      Node = nodeVisit.Node;
      VisitCount = nodeVisit.VisitCount;
      Position = nodeVisit.Position;
    }

    private struct InternalNodeVisit
    {
      public InternalNodeVisit(
        TNode node,
        NodePosition originalPosition,
        NodePosition position,
        int visitCount,
        int currentChildIndex)
      {
        Node = node;
        OriginalPosition = originalPosition;
        Position = position;
        VisitCount = visitCount;
        CurrentChildIndex = currentChildIndex;
      }

      public InternalNodeVisit(ITreenumerator<TNode> treenumerator)
        : this(
        treenumerator.Node,
        treenumerator.Position,
        treenumerator.Position,
        treenumerator.VisitCount,
        0)
      { }

      public TNode Node;
      public NodePosition OriginalPosition;
      public NodePosition Position;
      public int VisitCount;
      public int CurrentChildIndex;

      public override string ToString()
      {
        return $"N:{Node},  OP:{OriginalPosition},  P:{Position}  VC:{VisitCount},  CI:{CurrentChildIndex}";
      }
    }
  }
}
