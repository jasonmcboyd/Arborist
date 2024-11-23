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

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (EnumerationFinished)
        return false;

      if (Mode == TreenumeratorMode.VisitingNode)
        nodeTraversalStrategies = NodeTraversalStrategies.TraverseAll;

      // Do not apply any traversal strategies to the sentinel node.
      if (InnerTreenumerator.Position.Depth == -1)
        nodeTraversalStrategies = NodeTraversalStrategies.TraverseAll;

      // If the node was skipped, move it to the skipped stack.
      if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode
        && nodeTraversalStrategies == NodeTraversalStrategies.SkipNode)
      {
        _SkippedNodeVisits.AddLast(_NodeVisits.RemoveLast());
        _SkippedNodeVisits.GetLast().VisitCount++;
      }

      // Enumerate until we yield something or exhaust the inner enumerator.
      while (InnerTreenumerator.MoveNext(nodeTraversalStrategies))
      {
        // Reset the traversal strategy.
        nodeTraversalStrategies = NodeTraversalStrategies.TraverseAll;

        if (InnerTreenumerator.Mode == TreenumeratorMode.SchedulingNode)
        {
          if (OnScheduling())
          {
            return true;
          }
          else
          {
            nodeTraversalStrategies = NodeTraversalStrategies.SkipNode;
            continue;
          }
        }

        if (OnVisiting())
          return true;
      }

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

        if (stackWithDeepestNodeVisit == _SkippedNodeVisits)
          _SkippedNodeVisits.GetLast().VisitCount++;
        else if (_NodeVisits.Count == 1)
          _NodeVisits.GetLast().VisitCount++;
      }

      // Check if the current node visit should be skipped.
      if (!_Predicate(InnerTreenumerator.ToNodeContext()))
        return false;

      var siblingIndex = stackWithDeepestNodeVisit.GetLast().VisitCount - 1;
      var depth = GetEffectiveDepth();

      var nodeVisit =
        new InternalNodeVisit(
          siblingIndex,
          depth,
          InnerTreenumerator.Position.Depth,
          0);

      _NodeVisits.AddLast(nodeVisit);

      UpdateStateFromNodeVisit(ref _NodeVisits.GetLast());

      return true;
    }

    private bool OnVisiting()
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

      UpdateStateFromNodeVisit(ref _NodeVisits.GetLast());

      return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int GetEffectiveDepth() => _NodeVisits.Count + _SkippedNodeVisits.Count - 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RefSemiDeque<InternalNodeVisit> GetStackWithDeepestNodeVisit()
    {
      return
        _SkippedNodeVisits.Count == 0 || _NodeVisits.GetLast().Depth > _SkippedNodeVisits.GetLast().Depth
        ? _NodeVisits
        : _SkippedNodeVisits;
    }

    private void UpdateStateFromNodeVisit(ref InternalNodeVisit nodeVisit)
    {
      Mode = InnerTreenumerator.Mode;
      _DepthOfLastSeenNode = InnerTreenumerator.Position.Depth;

      if (!EnumerationFinished)
      {
        Node = InnerTreenumerator.Node;
        VisitCount = nodeVisit.VisitCount;
        Position = new NodePosition(nodeVisit.SiblingIndex, nodeVisit.Depth);
      }
    }

    private struct InternalNodeVisit
    {
      public InternalNodeVisit(
        int siblingIndex,
        int depth,
        int originalDepth,
        int visitCount)
      {
        SiblingIndex = siblingIndex;
        Depth = depth;
        OriginalDepth = originalDepth;
        VisitCount = visitCount;
      }

      public InternalNodeVisit(ITreenumerator<TNode> treenumerator)
        : this(
        treenumerator.Position.SiblingIndex,
        treenumerator.Position.Depth,
        treenumerator.Position.Depth,
        treenumerator.VisitCount)
      { }

      public readonly int SiblingIndex;
      public readonly int Depth;
      public readonly int OriginalDepth;
      public int VisitCount;

      public override string ToString()
      {
        return $"({SiblingIndex}, {Depth}),  {OriginalDepth},  {VisitCount}";
      }
    }
  }
}
