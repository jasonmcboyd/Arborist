using Arborist.Core;
using Arborist.Linq.Extensions;
using System;

namespace Arborist.Linq.Treenumerators
{
  internal class WhereBreadthFirstTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public WhereBreadthFirstTreenumerator(
      Func<ITreenumerator<TNode>> innerTreenumeratorFactory,
      Func<NodeContext<TNode>, bool> predicate,
      NodeTraversalStrategies nodeTraversalStrategy)
      : base(innerTreenumeratorFactory)
    {
      _Predicate = predicate;
      _NodeTraversalStrategy = nodeTraversalStrategy;
      _NodePositionAndVisitCounts.AddLast(new NodeTraversalStatus(InnerTreenumerator.Node, InnerTreenumerator.Position, 0));
    }

    private readonly Func<NodeContext<TNode>, bool> _Predicate;
    private readonly NodeTraversalStrategies _NodeTraversalStrategy;

    private readonly RefSemiDeque<NodeTraversalStatus> _NodePositionAndVisitCounts = new RefSemiDeque<NodeTraversalStatus>();
    private readonly RefSemiDeque<NodeVisit<TNode>> _SkippedStack = new RefSemiDeque<NodeVisit<TNode>>();

    private int _SeenRootNodesCount = 0;

    // Track the position of the last skipped node that was removed, for sibling index calculation
    private NodePosition? _LastRemovedSkippedNodePosition = null;

    // Track if we need to emit a parent visit before the next inner event
    private bool _PendingParentVisit = false;

    // Track how many extra parent visits we've emitted via _PendingParentVisit
    // These correspond to inner parent visits that we should skip
    private int _ExtraParentVisitsEmitted = 0;

    // Track the depth of the last removed skipped parent, for sibling index calculation
    // When we remove a skipped parent and schedule its child, the sibling index should reset to 0
    private int? _DepthOfLastRemovedSkippedParent = null;

    // Track the last visited node's depth and visit count for sibling index calculation
    // In BFT, after visiting a node at depth D, scheduled nodes at D+1 are its children
    private int _LastVisitedNodeDepth = -1;
    private int _LastVisitedNodeVisitCount = 0;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (Mode == TreenumeratorMode.VisitingNode)
        nodeTraversalStrategies = NodeTraversalStrategies.TraverseAll;

      var previouslySeenNodeWasScheduledAndSkipped =
        Position != new NodePosition(0, -1)
        && InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode);

      if (previouslySeenNodeWasScheduledAndSkipped)
        _NodePositionAndVisitCounts.GetLast().TraversalStrategy = nodeTraversalStrategies;

      // If we have a pending parent visit, emit it now
      // But only if:
      // 1. The previously scheduled node was NOT skipped by the caller
      // 2. The parent is not the sentinel (position != (0,-1))
      if (_PendingParentVisit && !previouslySeenNodeWasScheduledAndSkipped)
      {
        ref var parentStatus = ref _NodePositionAndVisitCounts.GetFirst();

        // Only emit if parent is not the sentinel
        if (parentStatus.Position.Depth >= 0)
        {
          _PendingParentVisit = false;
          parentStatus.VisitCount++;
          _ExtraParentVisitsEmitted++;

          // Track the visited node's depth and visit count for sibling index calculation
          _LastVisitedNodeDepth = parentStatus.Position.Depth;
          _LastVisitedNodeVisitCount = parentStatus.VisitCount;

          Mode = TreenumeratorMode.VisitingNode;
          Node = parentStatus.Node;
          Position = parentStatus.Position;
          VisitCount = parentStatus.VisitCount;
          return true;
        }
      }

      // Clear pending flag if we're not emitting
      _PendingParentVisit = false;

      var previousModeWasVisitingNode = Mode == TreenumeratorMode.VisitingNode;

      while (InnerTreenumerator.MoveNext(nodeTraversalStrategies))
      {
        if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        {
          // Only pop skipped nodes when scheduling at the same or shallower depth
          // This indicates we've moved past the skipped node's subtree
          while (_SkippedStack.Count > 0 && _SkippedStack.GetLast().Position.Depth >= InnerTreenumerator.Position.Depth)
            _SkippedStack.RemoveLast();

          var skipped = _Predicate(InnerTreenumerator.ToNodeContext());

          if (skipped)
          {
            _SkippedStack.AddLast(InnerTreenumerator.ToNodeVisit());

            nodeTraversalStrategies = _NodeTraversalStrategy;

            continue;
          }
          else
          {
            // Remove skipped nodes from the queue BEFORE calculating the effective position
            // This ensures we don't use positions from a different parent's skipped children
            var lastScheduleNodeVisitWasSkipped = _NodePositionAndVisitCounts.GetLast().Skipped;

            if (lastScheduleNodeVisitWasSkipped)
            {
              // Track the depth of the skipped parent for sibling index calculation
              _DepthOfLastRemovedSkippedParent = _NodePositionAndVisitCounts.GetLast().Position.Depth;

              // Save the position of the skipped node for sibling index calculation
              // Save if:
              // 1. Previous output was not a visit (consecutive schedules), OR
              // 2. Parent's VC > 1 (we've scheduled siblings before, so this is a sibling being removed)
              // Don't save if we just visited a NEW parent (VC == 1) - those are from different parent
              if (!previousModeWasVisitingNode || _LastVisitedNodeVisitCount > 1)
              {
                _LastRemovedSkippedNodePosition = _NodePositionAndVisitCounts.GetLast().Position;
              }
              _NodePositionAndVisitCounts.RemoveLast();
            }
            else
            {
              _DepthOfLastRemovedSkippedParent = null;
            }

            var effectivePosition = GetEffectivePosition();

            // Check if this is a promoted child (child of a filtered node)
            // If so, set pending parent visit flag - but only if the promoted child
            // is NOT becoming a root (effectiveDepth > 0)
            if (_SkippedStack.Count > 0
              && InnerTreenumerator.Position.Depth == _SkippedStack.GetLast().Position.Depth + 1
              && effectivePosition.Depth > 0)
            {
              _PendingParentVisit = true;
            }

            _NodePositionAndVisitCounts.AddLast(new NodeTraversalStatus(InnerTreenumerator.Node, effectivePosition, 0));
          }
        }
        else // VisitingNode
        {
          // When visiting a node, clear saved positions - we're entering a new subtree
          // Note: _LastRemovedSkippedNodePosition needs special handling (see saving condition)
          // but _DepthOfLastRemovedSkippedParent is cleared after use in GetEffectivePosition
          _LastRemovedSkippedNodePosition = null;


          // When visiting a node at depth D, pop skipped nodes at depth D (siblings that were skipped)
          // These are not ancestors of this node's children, so they shouldn't affect depth calculation
          while (_SkippedStack.Count > 0 && _SkippedStack.GetLast().Position.Depth == InnerTreenumerator.Position.Depth)
            _SkippedStack.RemoveLast();

          // Check if we're visiting a filtered node
          var isVisitingFilteredNode = _SkippedStack.Count > 0
            && _SkippedStack.GetLast().Position == InnerTreenumerator.Position;

          if (isVisitingFilteredNode)
          {
            // Skip all visits to filtered nodes - the parent visits are handled via _PendingParentVisit
            continue;
          }

          // Check if this is a parent visit that we've already emitted via _PendingParentVisit
          // This happens when visiting the parent of a filtered node
          // We need to check if the inner position is the parent of ANY filtered node in the stack
          if (_ExtraParentVisitsEmitted > 0 && _SkippedStack.Count > 0)
          {
            var isParentOfAnyFilteredNode = false;
            for (int i = 0; i < _SkippedStack.Count; i++)
            {
              if (InnerTreenumerator.Position.Depth == _SkippedStack.GetFromBack(i).Position.Depth - 1)
              {
                isParentOfAnyFilteredNode = true;
                break;
              }
            }

            if (isParentOfAnyFilteredNode)
            {
              _ExtraParentVisitsEmitted--;
              continue;
            }
          }

          // Normal visiting logic
          if (InnerTreenumerator.VisitCount == 1)
            _NodePositionAndVisitCounts.RemoveFirst();
          else if (previousModeWasVisitingNode)
            continue;

          _NodePositionAndVisitCounts.GetFirst().VisitCount++;

          // Track the visited node's effective depth and visit count for sibling index calculation
          // In BFT, after visiting a node at depth D, scheduled nodes at D+1 are its children
          _LastVisitedNodeDepth = _NodePositionAndVisitCounts.GetFirst().Position.Depth;
          _LastVisitedNodeVisitCount = _NodePositionAndVisitCounts.GetFirst().VisitCount;
        }

        UpdateState();

        return true;
      }

      UpdateState();

      return false;
    }

    private NodePosition GetEffectivePosition()
    {
      var effectiveDepth = InnerTreenumerator.Position.Depth - _SkippedStack.Count;

      int effectiveSiblingIndex;

      if (effectiveDepth == 0)
      {
        effectiveSiblingIndex = _SeenRootNodesCount;
      }
      else
      {
        // If we just removed a skipped parent and are scheduling its child,
        // this is the first child of that parent, so sibling index is 0
        if (_DepthOfLastRemovedSkippedParent.HasValue
          && _DepthOfLastRemovedSkippedParent.Value + 1 == effectiveDepth)
        {
          effectiveSiblingIndex = 0;
          _DepthOfLastRemovedSkippedParent = null;  // Clear after use - only affects first child
        }
        else
        {
          var previousNodePosition = _NodePositionAndVisitCounts.GetLast().Position;

          // If previous output was a visit at depth-1, we need to determine the sibling index
          if (Mode == TreenumeratorMode.VisitingNode
            && _LastVisitedNodeDepth + 1 == effectiveDepth)
          {
            var siblingIndexFromVC = _LastVisitedNodeVisitCount - 1;

            // Only check for existing siblings if this is not the first child (VC > 1)
            // For the first child (VC == 1), any existing nodes at this depth are from a previous parent
            if (_LastVisitedNodeVisitCount > 1
              && previousNodePosition.Depth == effectiveDepth
              && previousNodePosition.SiblingIndex >= siblingIndexFromVC)
            {
              // Continue from the previous sibling's position
              effectiveSiblingIndex = previousNodePosition.SiblingIndex + 1;
            }
            else
            {
              // No sibling at this depth yet (after this visit), use parent's VisitCount
              effectiveSiblingIndex = siblingIndexFromVC;
            }
          }
          // If previous output was a schedule and there's a node at the same depth, it's a sibling
          else if (previousNodePosition.Depth == effectiveDepth)
          {
            effectiveSiblingIndex = previousNodePosition.SiblingIndex + 1;
          }
          // Check saved position from a recently removed skipped node
          else if (_LastRemovedSkippedNodePosition.HasValue
            && _LastRemovedSkippedNodePosition.Value.Depth == effectiveDepth)
          {
            effectiveSiblingIndex = _LastRemovedSkippedNodePosition.Value.SiblingIndex + 1;
          }
          else
          {
            effectiveSiblingIndex = 0;
          }
        }
      }

      return new NodePosition(effectiveSiblingIndex, effectiveDepth);
    }

    private ref NodeTraversalStatus GetNodeTraversalStatusToUpdateState()
    {
      if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        return ref _NodePositionAndVisitCounts.GetLast();

      return ref _NodePositionAndVisitCounts.GetFirst();
    }

    private void UpdateState()
    {
      Mode = InnerTreenumerator.Mode;

      ref var nodePositionAndVisitCount = ref GetNodeTraversalStatusToUpdateState();

      if (nodePositionAndVisitCount.Position.Depth == 0
        && InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
      {
        _SeenRootNodesCount++;
      }

      Node = nodePositionAndVisitCount.Node;
      VisitCount = nodePositionAndVisitCount.VisitCount;
      Position = nodePositionAndVisitCount.Position;
    }

    private struct NodeTraversalStatus
    {
      public NodeTraversalStatus(
        TNode node,
        NodePosition position,
        int visitCount,
        NodeTraversalStrategies nodeTraversalStrategies = NodeTraversalStrategies.TraverseAll)
      {
        Node = node;
        Position = position;
        VisitCount = visitCount;
        TraversalStrategy = nodeTraversalStrategies;
      }

      public TNode Node { get; set; }
      public NodePosition Position { get; set; }
      public int VisitCount { get; set; }
      public NodeTraversalStrategies TraversalStrategy { get; set; }
      public bool Skipped => TraversalStrategy != NodeTraversalStrategies.TraverseAll;

      public override string ToString() => $"({Position}), {VisitCount}, {TraversalStrategy}";
    }
  }
}
