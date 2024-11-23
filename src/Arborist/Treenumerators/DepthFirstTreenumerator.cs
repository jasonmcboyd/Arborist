using Arborist.Core;
using System;
using System.Collections.Generic;

namespace Arborist.Treenumerators
{
  public sealed class DepthFirstTreenumerator<TValue, TNode, TChildEnumerator>
    : TreenumeratorBase<TValue>
    where TChildEnumerator : IChildEnumerator<TNode>
  {
    public DepthFirstTreenumerator(
      IEnumerable<TNode> rootNodes,
      Func<NodeContext<TNode>, TChildEnumerator> childEnumeratorFactory,
      Func<TNode, TValue> map)
    {
      _RootsEnumerator = rootNodes.GetEnumerator();
      _ChildEnumeratorFactory = childEnumeratorFactory;
      _Map = map;
    }

    private readonly IEnumerator<TNode> _RootsEnumerator;
    private readonly Func<NodeContext<TNode>, TChildEnumerator> _ChildEnumeratorFactory;
    private readonly Func<TNode, TValue> _Map;

    private readonly RefSemiDeque<InternalNodeVisitState> _Stack = new RefSemiDeque<InternalNodeVisitState>();
    private readonly RefSemiDeque<InternalNodeVisitState> _SkippedStack = new RefSemiDeque<InternalNodeVisitState>();
    private readonly RefSemiDeque<TChildEnumerator> _ChildEnumeratorStack = new RefSemiDeque<TChildEnumerator>();

    private bool _HasCachedChild = false;
    private int _RootNodesSeen = 0;
    private bool _RootsEnumeratorFinished = false;

    private int CurrentDepth => _ChildEnumeratorStack.Count - 1;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (_Disposed)
        return false;

      if (CurrentDepth == -1)
        return MoveToNextRootNode();

      if (_HasCachedChild)
      {
        _HasCachedChild = false;

        UpdateState(ref _Stack.GetLast());

        return true;
      }

      if (Mode == TreenumeratorMode.SchedulingNode)
        return OnScheduling(nodeTraversalStrategies);

      return OnVisiting();
    }

    private bool MoveToNextRootNode()
    {
      if (_RootsEnumeratorFinished || !_RootsEnumerator.MoveNext())
        return false;

      PushNewNodeVisit(_RootsEnumerator.Current, _RootNodesSeen);

      _RootNodesSeen++;

      UpdateState(ref _Stack.GetLast());

      return true;
    }

    private bool OnScheduling(NodeTraversalStrategies nodeTraversalStrategies)
    {
      ref var previousVisit = ref _Stack.GetLast();
      previousVisit.VisitCount++;

      if ((nodeTraversalStrategies & NodeTraversalStrategies.SkipSiblings) == NodeTraversalStrategies.SkipSiblings)
      {
        if (CurrentDepth == 0)
          _RootsEnumeratorFinished = true;
        else
          _ChildEnumeratorStack.GetFromBack(1).Dispose();
      }

      if ((nodeTraversalStrategies & NodeTraversalStrategies.SkipNodeAndDescendants) == NodeTraversalStrategies.SkipNodeAndDescendants)
        return Backtrack();

      if ((nodeTraversalStrategies & NodeTraversalStrategies.SkipNode) == NodeTraversalStrategies.SkipNode)
      {
        _SkippedStack.AddLast(_Stack.RemoveLast());

        if (TryPushNextChild())
          return true;

        return Backtrack();
      }

      if ((nodeTraversalStrategies & NodeTraversalStrategies.SkipDescendants) == NodeTraversalStrategies.SkipDescendants)
        _ChildEnumeratorStack.GetLast().Dispose();

      UpdateState(ref previousVisit);

      return true;
    }

    private bool OnVisiting()
    {
      if (TryPushNextChild())
        return true;

      return Backtrack();
    }

    private bool Backtrack()
    {
      RefSemiDeque<InternalNodeVisitState> stack = GetStackWithDeepestSeenNode();

      PopStacks(stack);

      while (true)
      {
        stack = GetStackWithDeepestSeenNode();

        var nodeSkipped = stack == _SkippedStack;

        if (stack == null)
          return MoveToNextRootNode();

        ref var nodeVisit = ref stack.GetLast();

        nodeVisit.VisitCount++;

        if (nodeSkipped)
        {
          if (TryPushNextChild(cacheChild: true))
            return true;

          PopStacks(stack);

          continue;
        }

        UpdateState(ref nodeVisit);

        return true;
      }
    }

    private bool TryPushNextChild(bool cacheChild = false)
    {
      if (!_ChildEnumeratorStack.GetLast().MoveNext(out var childNodeAndSiblingIndex))
        return false;

      PushNewNodeVisit(childNodeAndSiblingIndex.Node, childNodeAndSiblingIndex.SiblingIndex);

      if (cacheChild && _Stack.Count > 1)
      {
        ref var parent = ref _Stack.GetFromBack(1);

        _HasCachedChild = true;
        parent.VisitCount++;
        UpdateState(ref parent);
      }
      else
      {
        UpdateState(ref _Stack.GetLast());
      }

      return true;
    }

    private void PushNewNodeVisit(
      TNode node,
      int childIndex)
    {
      var internalNodeVisitState = new InternalNodeVisitState(node, new NodePosition(childIndex, CurrentDepth + 1));
      var nodeChildEnumerator = _ChildEnumeratorFactory(new NodeContext<TNode>(internalNodeVisitState.Node, internalNodeVisitState.Position));

      _Stack.AddLast(internalNodeVisitState);
      _ChildEnumeratorStack.AddLast(nodeChildEnumerator);
    }

    private void PopStacks(RefSemiDeque<InternalNodeVisitState> stack)
    {
      stack.RemoveLast();
      _ChildEnumeratorStack.RemoveLast().Dispose();
    }

    private RefSemiDeque<InternalNodeVisitState> GetStackWithDeepestSeenNode()
    {
      if (_Stack.Count == 0 && _SkippedStack.Count == 0)
        return null;

      return
        _SkippedStack.Count == 0
        ? _Stack
        : _Stack.Count == 0
        ? _SkippedStack
        : _Stack.GetLast().Position.Depth > _SkippedStack.GetLast().Position.Depth
        ? _Stack
        : _SkippedStack;
    }

    private void UpdateState(ref InternalNodeVisitState nodeVisit)
    {
      Mode = nodeVisit.VisitCount == 0 ? TreenumeratorMode.SchedulingNode : TreenumeratorMode.VisitingNode;
      Node = _Map(nodeVisit.Node);
      VisitCount = nodeVisit.VisitCount;
      Position = nodeVisit.Position;
    }

    #region Dispose

    private bool _Disposed = false;

    public override void Dispose()
    {
      // Call the private Dispose method with disposing = true.
      Dispose(true);
      // Suppress finalization to prevent the garbage collector from calling the finalizer.
      GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
      if (_Disposed)
        return;

      _Disposed = true;

      if (disposing)
      {
        _RootsEnumerator?.Dispose();

        DisposeStack(_ChildEnumeratorStack);
      }
    }

    private void DisposeStack(RefSemiDeque<TChildEnumerator> stackChildEnumerators)
    {
      if (stackChildEnumerators == null)
        return;

      while (stackChildEnumerators.Count > 0)
        stackChildEnumerators.RemoveLast().Dispose();
    }

    ~DepthFirstTreenumerator()
    {
      Dispose(false);
    }

    #endregion Dispose

    private struct InternalNodeVisitState
    {
      public InternalNodeVisitState(
        TNode node,
        NodePosition position)
      {
        Node = node;
        VisitCount = 0;
        Position = position;
      }

      public readonly TNode Node;
      public int VisitCount;
      public NodePosition Position;
    }
  }
}
