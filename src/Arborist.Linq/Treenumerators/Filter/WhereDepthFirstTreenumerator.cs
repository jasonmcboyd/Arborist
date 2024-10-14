using Arborist.Common;
using Arborist.Core;
using Arborist.Linq.Extensions;
using System;

namespace Arborist.Linq.Treenumerators
{
  internal class WhereDepthFirstTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public WhereDepthFirstTreenumerator(
      Func<ITreenumerator<TNode>> innerTreenumeratorFactory,
      Func<NodeContext<TNode>, bool> predicate)
      : base(innerTreenumeratorFactory)
    {
      _Predicate = predicate;

      // Add a sentinel node to the stack.
      _NodeVisits.AddLast(new InternalNodeVisit(InnerTreenumerator));
      _NodeVisits.GetLast().VisitCount = 1;
    }

    private readonly Func<NodeContext<TNode>, bool> _Predicate;

    private readonly RefSemiDeque<InternalNodeVisit> _NodeVisits = new RefSemiDeque<InternalNodeVisit>();
    private readonly RefSemiDeque<InternalNodeVisit> _SkippedNodeVisits = new RefSemiDeque<InternalNodeVisit>();

    private int _DepthOfLastSeenNode = -1;

    private bool _EnumerationFinished = false;

    protected override bool OnMoveNext(NodeTraversalStrategy nodeTraversalStrategy)
    {
      if (_EnumerationFinished)
        return false;

      if (Mode == TreenumeratorMode.VisitingNode)
        nodeTraversalStrategy = NodeTraversalStrategy.TraverseSubtree;

      return InnerTreenumeratorMoveNext(nodeTraversalStrategy);
    }

    private bool InnerTreenumeratorMoveNext(NodeTraversalStrategy nodeTraversalStrategy)
    {
      // Do not apply any traversal strategies to the sentinel node.
      if (InnerTreenumerator.Position.Depth == -1)
        nodeTraversalStrategy = NodeTraversalStrategy.TraverseSubtree;

      // If the node was skipped, move it to the skipped stack.
      if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && nodeTraversalStrategy == NodeTraversalStrategy.SkipNode)
      {
        _SkippedNodeVisits.AddLast(_NodeVisits.RemoveLast());
        _SkippedNodeVisits.GetLast().VisitCount++;
      }

      // Enumerate until we yield something or exhaust the inner enumerator.
      while (InnerTreenumerator.MoveNext(nodeTraversalStrategy))
      {
        // Reset the traversal strategy.
        nodeTraversalStrategy = NodeTraversalStrategy.TraverseSubtree;

        if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        {
          if (OnScheduling())
          {
            return true;
          }
          else
          {
            nodeTraversalStrategy = NodeTraversalStrategy.SkipNode;
            continue;
          }
        }

        if (OnTraversing())
          return true;
      }

      // If we are here we have exhausted the inner enumerator.
      _EnumerationFinished = true;

      return false;
    }

    private bool OnScheduling()
    {
      var stackWithDeepestNodeVisit = GetStackWithDeepestNodeVisit();

      var traversingAwayFromRootNode = InnerTreenumerator.Position.Depth > stackWithDeepestNodeVisit.GetLast().OriginalDepth;

      if (!traversingAwayFromRootNode)
      {
        while (_SkippedNodeVisits.Count > 0
          && _SkippedNodeVisits.GetLast().OriginalDepth >= InnerTreenumerator.Position.Depth)
        {
          _SkippedNodeVisits.RemoveLast();
        }

        while (_NodeVisits.GetLast().OriginalDepth >= InnerTreenumerator.Position.Depth)
          _NodeVisits.RemoveLast();

        stackWithDeepestNodeVisit = GetStackWithDeepestNodeVisit();

        if (stackWithDeepestNodeVisit == _SkippedNodeVisits
          || stackWithDeepestNodeVisit.Count == 1)
        {
          stackWithDeepestNodeVisit.GetLast().VisitCount++;
        }
      }

      // Check if the current node visit should be skipped.
      if (!_Predicate(InnerTreenumerator.ToNodeContext()))
        return false;

      var siblingIndex = stackWithDeepestNodeVisit.GetLast().VisitCount - 1;
      var depth = GetEffectiveDepth();

      var nodeVisit =
        new InternalNodeVisit(
          InnerTreenumerator.Node,
          TreenumeratorMode.SchedulingNode,
          siblingIndex,
          depth,
          InnerTreenumerator.Position.Depth,
          0);

      _NodeVisits.AddLast(nodeVisit);

      UpdateStateFromNodeVisit(ref _NodeVisits.GetLast());

      return true;
    }

    private bool OnTraversing()
    {
      if (InnerTreenumerator.VisitCount > 1
        && _DepthOfLastSeenNode <= InnerTreenumerator.Position.Depth)
      {
        return false;
      }

      if (InnerTreenumerator.Position.Depth < _NodeVisits.GetLast().OriginalDepth)
        _NodeVisits.RemoveLast();

      ref var nodeVisit = ref _NodeVisits.GetLast();

      nodeVisit.VisitCount++;
      nodeVisit.Mode = TreenumeratorMode.VisitingNode;

      UpdateStateFromNodeVisit(ref _NodeVisits.GetLast());

      return true;
    }

    private int GetEffectiveDepth()
    {
      var depth = _NodeVisits.Count + _SkippedNodeVisits.Count - 1;

      return Math.Max(depth, 0);
    }

    private RefSemiDeque<InternalNodeVisit> GetStackWithDeepestNodeVisit()
    {
      return
        _SkippedNodeVisits.Count == 0 || _NodeVisits.GetLast().Depth > _SkippedNodeVisits.GetLast().Depth
        ? _NodeVisits
        : _SkippedNodeVisits;
    }

    private void UpdateStateFromNodeVisit(ref InternalNodeVisit nodeVisit)
    {
      Mode = nodeVisit.Mode;
      _DepthOfLastSeenNode = InnerTreenumerator.Position.Depth;

      if (!_EnumerationFinished)
      {
        Node = nodeVisit.Node;
        VisitCount = nodeVisit.VisitCount;
        Position = new NodePosition(nodeVisit.SiblingIndex, nodeVisit.Depth);
      }
    }

    private struct InternalNodeVisit
    {
      public InternalNodeVisit(
        TNode node,
        TreenumeratorMode mode,
        int siblingIndex,
        int depth,
        int originalDepth,
        int visitCount)
      {
        Node = node;
        Mode = mode;
        SiblingIndex = siblingIndex;
        Depth = depth;
        OriginalDepth = originalDepth;
        VisitCount = visitCount;
      }

      public InternalNodeVisit(ITreenumerator<TNode> treenumerator)
        : this(
        treenumerator.Node,
        treenumerator.Mode,
        treenumerator.Position.SiblingIndex,
        treenumerator.Position.Depth,
        treenumerator.Position.Depth,
        treenumerator.VisitCount)
      { }

      public TNode Node { get; set; }
      public TreenumeratorMode Mode { get; set; }
      public int SiblingIndex { get; set; }
      public int Depth { get; set; }
      public int OriginalDepth { get; set; }
      public int VisitCount { get; set; }

      public override string ToString()
      {
        return $"({SiblingIndex}, {Depth}),  {OriginalDepth},  {ModeToChar()}  {VisitCount}  {Node}";
      }

      private char ModeToChar()
      {
        switch (Mode)
        {
          case TreenumeratorMode.SchedulingNode:
            return 'S';
          case TreenumeratorMode.VisitingNode:
            return 'V';
          default:
            throw new NotImplementedException();
        }
      }
    }
  }
}
