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
      Func<NodeContext<TNode>, bool> predicate)
      : base(innerTreenumerator)
    {
      _Predicate = predicate;

      // Add a sentinel node to the stack.
      _NodeVisits.Push(new InternalNodeVisit(InnerTreenumerator).ToFirstVisit());
    }

    private readonly Func<NodeContext<TNode>, bool> _Predicate;

    private readonly Stack<InternalNodeVisit> _NodeVisits = new Stack<InternalNodeVisit>();
    private readonly Stack<InternalNodeVisit> _SkippedNodeVisits = new Stack<InternalNodeVisit>();

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
        _SkippedNodeVisits.Push(_NodeVisits.Pop().IncrementVisitCount());
      }

      // Enumerate until we yield something or exhaust the inner enumerator.
      while (InnerTreenumerator.MoveNext(nodeTraversalStrategy))
      {
        // Reset the traversal strategy.
        nodeTraversalStrategy = NodeTraversalStrategy.TraverseSubtree;

        var currentNodeVisit = InnerTreenumerator.ToNodeVisit();

        if (currentNodeVisit.Mode == TreenumeratorMode.SchedulingNode)
        {
          if (OnScheduling(currentNodeVisit))
          {
            return true;
          }
          else
          {
            nodeTraversalStrategy = NodeTraversalStrategy.SkipNode;
            continue;
          }
        }

        if (OnTraversing(currentNodeVisit))
          return true;
      }

      // If we are here we have exhausted the inner enumerator.
      _EnumerationFinished = true;

      return false;
    }

    private bool OnScheduling(NodeVisit<TNode> currentNodeVisit)
    {
      var stackWithDeepestNodeVisit = GetStackWithDeepestNodeVisit();

      var traversingAwayFromRootNode = currentNodeVisit.Position.Depth > stackWithDeepestNodeVisit.Peek().OriginalDepth;

      if (!traversingAwayFromRootNode)
      {
        while (_SkippedNodeVisits.Count > 0
          && _SkippedNodeVisits.Peek().OriginalDepth >= currentNodeVisit.Position.Depth)
          _SkippedNodeVisits.Pop();

        while (_NodeVisits.Peek().OriginalDepth >= currentNodeVisit.Position.Depth)
          _NodeVisits.Pop();

        stackWithDeepestNodeVisit = GetStackWithDeepestNodeVisit();

        if (stackWithDeepestNodeVisit == _SkippedNodeVisits
          || stackWithDeepestNodeVisit.Count == 1)
          IncrementVisitCountOfVisitOnTopOfStack(stackWithDeepestNodeVisit);
      }

      // Check if the current node visit should be skipped.
      if (!_Predicate(currentNodeVisit.ToNodeContext()))
        return false;

      var siblingIndex = stackWithDeepestNodeVisit.Peek().VisitCount - 1;
      var depth = GetEffectiveDepth();

      var nodeVisit = InternalNodeVisit.Scheduled(InnerTreenumerator, siblingIndex, depth);

      _NodeVisits.Push(nodeVisit);

      UpdateStateFromNodeVisit(nodeVisit);

      return true;
    }

    private bool OnTraversing(NodeVisit<TNode> currentNodeVisit)
    {
      if (currentNodeVisit.VisitCount > 1
        && _DepthOfLastSeenNode <= currentNodeVisit.Position.Depth)
        return false;

      if (currentNodeVisit.Position.Depth < _NodeVisits.Peek().OriginalDepth)
        _NodeVisits.Pop();

      var previousNodeVisit = _NodeVisits.Pop();

      if (previousNodeVisit.Mode == TreenumeratorMode.SchedulingNode)
        previousNodeVisit = previousNodeVisit.ToFirstVisit();
      else
        previousNodeVisit = previousNodeVisit.IncrementVisitCount();

      _NodeVisits.Push(previousNodeVisit);

      UpdateStateFromNodeVisit(previousNodeVisit);

      return true;
    }

    private int GetEffectiveDepth()
    {
      var depth = _NodeVisits.Count + _SkippedNodeVisits.Count - 1;

      return Math.Max(depth, 0);
    }

    private void IncrementVisitCountOfVisitOnTopOfStack(Stack<InternalNodeVisit> stack)
    {
      stack.Push(stack.Pop().IncrementVisitCount());
    }

    private Stack<InternalNodeVisit> GetStackWithDeepestNodeVisit()
    {
      return
        _SkippedNodeVisits.Count == 0 || _NodeVisits.Peek().Depth > _SkippedNodeVisits.Peek().Depth
        ? _NodeVisits
        : _SkippedNodeVisits;
    }

    private void UpdateStateFromNodeVisit(InternalNodeVisit nodeVisit)
    {
      Mode = nodeVisit.Mode;
      _DepthOfLastSeenNode = InnerTreenumerator.Position.Depth;

      if (!_EnumerationFinished)
      {
        Node = nodeVisit.Node;
        VisitCount = nodeVisit.VisitCount;
        Position = (nodeVisit.SiblingIndex, nodeVisit.Depth);
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

      public TNode Node { get; }
      public TreenumeratorMode Mode { get; }
      public int SiblingIndex { get; }
      public int Depth { get; }
      public int OriginalDepth { get; }
      public int VisitCount { get; }

      public InternalNodeVisit IncrementVisitCount()
      {
        return new InternalNodeVisit(Node, Mode, SiblingIndex, Depth, OriginalDepth, VisitCount + 1);
      }

      public InternalNodeVisit ToFirstVisit()
      {
        return new InternalNodeVisit(Node, TreenumeratorMode.VisitingNode, SiblingIndex, Depth, OriginalDepth, 1);
      }

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

      public static InternalNodeVisit Scheduled(
        ITreenumerator<TNode> treenumerator,
        int siblingIndex,
        int depth)
      {
        return new InternalNodeVisit(treenumerator.Node, TreenumeratorMode.SchedulingNode, siblingIndex, depth, treenumerator.Position.Depth, 0);
      }
    }
  }
}
