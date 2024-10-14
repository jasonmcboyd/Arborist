using Arborist.Common;
using Arborist.Core;
using Arborist.Treenumerables;
using System;
using System.Collections.Generic;

namespace Arborist.Treenumerators
{
  internal sealed class DepthFirstTreenumerator<TValue, TNode, TChildEnumerator>
    : TreenumeratorBase<TValue>
  {
    public DepthFirstTreenumerator(
      IEnumerable<TNode> rootNodes,
      Func<TNode, TChildEnumerator> childEnumeratorFactory,
      MoveNextChildDelegate<TChildEnumerator, TNode> tryMoveNextChildDelegate,
      DisposeChildEnumeratorDelegate<TChildEnumerator> disposeChildEnumeratorDelegate,
      Func<TNode, TValue> map)
    {
      _RootsEnumerator = rootNodes.GetEnumerator();
      _ChildEnumeratorFactory = childEnumeratorFactory;
      _MoveNextChildDelegate = tryMoveNextChildDelegate;
      _DisposeChildEnumeratorDelegate = disposeChildEnumeratorDelegate;
      _Map = map;
    }

    private readonly IEnumerator<TNode> _RootsEnumerator;
    private readonly Func<TNode, TChildEnumerator> _ChildEnumeratorFactory;
    private readonly MoveNextChildDelegate<TChildEnumerator, TNode> _MoveNextChildDelegate;
    private readonly DisposeChildEnumeratorDelegate<TChildEnumerator> _DisposeChildEnumeratorDelegate;
    private readonly Func<TNode, TValue> _Map;

    private readonly RefSemiDeque<NodeVisit<TNode>> _Stack = new RefSemiDeque<NodeVisit<TNode>>();
    private readonly RefSemiDeque<TChildEnumerator> _StackChildEnumerators = new RefSemiDeque<TChildEnumerator>();

    private readonly RefSemiDeque<NodeVisit<TNode>> _SkippedStack = new RefSemiDeque<NodeVisit<TNode>>();
    private readonly RefSemiDeque<TChildEnumerator> _SkippedStackChildEnumerators = new RefSemiDeque<TChildEnumerator>();

    private int CurrentDepth => _Stack.Count + _SkippedStack.Count;

    private bool _HasCachedChild = false;
    private int _RootNodesSeen = 0;

    protected override bool OnMoveNext(NodeTraversalStrategy nodeTraversalStrategy)
    {
      if (_Disposed)
        return false;

      if (CurrentDepth == 0)
        return MoveToNextRootNode();

      if (_HasCachedChild)
      {
        _HasCachedChild = false;

        UpdateStateFromVirtualNodeVisit(ref _Stack.GetLast());

        return true;
      }

      if (Mode == TreenumeratorMode.SchedulingNode)
        return OnScheduling(nodeTraversalStrategy);

      return OnVisiting();
    }

    private bool MoveToNextRootNode()
    {
      if (!_RootsEnumerator.MoveNext())
        return false;

      PushNewNodeVisit(_RootsEnumerator.Current, _RootNodesSeen);

      _RootNodesSeen++;

      UpdateStateFromVirtualNodeVisit(ref _Stack.GetLast());

      return true;
    }

    private bool OnScheduling(NodeTraversalStrategy nodeTraversalStrategy)
    {
      ref var previousVisit = ref _Stack.GetLast();
      ref var previousVisitChildEnumerator = ref _StackChildEnumerators.GetLast();
      previousVisit.VisitCount++;

      previousVisit.TraversalStrategy = nodeTraversalStrategy;

      if (nodeTraversalStrategy == NodeTraversalStrategy.SkipNode)
      {
        if (TryPushNextChild(ref previousVisitChildEnumerator, popMainStacksOntoSkippedStacks: true))
          return true;

        return MoveUpTheTreeStack();
      }

      if (nodeTraversalStrategy == NodeTraversalStrategy.SkipSubtree)
        return MoveUpTheTreeStack();

      previousVisit.Mode = TreenumeratorMode.VisitingNode;

      UpdateStateFromVirtualNodeVisit(ref previousVisit);

      return true;
    }

    private bool OnVisiting()
    {
      ref var previousVisit = ref _Stack.GetLast();
      ref var previousVisitChildEnumerator = ref _StackChildEnumerators.GetLast();

      if (previousVisit.TraversalStrategy != NodeTraversalStrategy.SkipDescendants
        && TryPushNextChild(ref previousVisitChildEnumerator))
      {
        return true;
      }

      return MoveUpTheTreeStack();
    }

    private bool MoveUpTheTreeStack()
    {
      PopMainStacks();

      RefSemiDeque<NodeVisit<TNode>> stack;
      RefSemiDeque<TChildEnumerator> stackChildEnumerator;

      while (true)
      {
        if (!GetStacksWithDeepestSeenNode(out stack, out stackChildEnumerator))
          return MoveToNextRootNode();

        ref var nodeVisit = ref stack.GetLast();
        ref var nodeVisitChildEnumerator = ref stackChildEnumerator.GetLast();

        nodeVisit.VisitCount++;

        if (nodeVisit.TraversalStrategy == NodeTraversalStrategy.SkipNode)
        {
          if (TryPushNextChild(ref nodeVisitChildEnumerator, cacheChild: true))
            return true;

          PopStacks(stack, stackChildEnumerator);

          continue;
        }

        UpdateStateFromVirtualNodeVisit(ref nodeVisit);

        return true;
      }
    }

    private bool TryPushNextChild(
      ref TChildEnumerator nodeVisitChildEnumerator,
      bool cacheChild = false,
      bool popMainStacksOntoSkippedStacks = false)
    {
      if (!_MoveNextChildDelegate(ref nodeVisitChildEnumerator, out var childNodeContext))
        return false;

      if (popMainStacksOntoSkippedStacks)
        PopMainStacksOntoSkippedStacks();

      PushNewNodeVisit(childNodeContext.Node, childNodeContext.SiblingIndex);

      if (cacheChild && _Stack.Count > 1)
      {
        _HasCachedChild = true;
        _Stack.GetFromBack(1).VisitCount++;
        UpdateStateFromVirtualNodeVisit(ref _Stack.GetFromBack(1));
      }
      else
      {
        UpdateStateFromVirtualNodeVisit(ref _Stack.GetLast());
      }

      return true;
    }

    private void PopMainStacksOntoSkippedStacks()
    {
      _SkippedStack.AddLast(_Stack.RemoveLast());
      _SkippedStackChildEnumerators.AddLast(_StackChildEnumerators.RemoveLast());
    }

    private void PushNewNodeVisit(
      TNode node,
      int childIndex)
    {
      var nodeVisit =
        new NodeVisit<TNode>(
          TreenumeratorMode.SchedulingNode,
          node,
          0,
          (childIndex, CurrentDepth),
          NodeTraversalStrategy.TraverseSubtree);
      var nodeChildEnumerator = _ChildEnumeratorFactory(node);

      _Stack.AddLast(nodeVisit);
      _StackChildEnumerators.AddLast(nodeChildEnumerator);
    }

    private void PopMainStacks() => PopStacks(_Stack, _StackChildEnumerators);

    private void PopStacks(RefSemiDeque<NodeVisit<TNode>> stack, RefSemiDeque<TChildEnumerator> stackChildEnumerator)
    {
      stack.RemoveLast();
      _DisposeChildEnumeratorDelegate(ref stackChildEnumerator.GetLast());
      stackChildEnumerator.RemoveLast();
    }

    private bool GetStacksWithDeepestSeenNode(
      out RefSemiDeque<NodeVisit<TNode>> stack,
      out RefSemiDeque<TChildEnumerator> stackChildEnumerators)
    {
      if (_Stack.Count == 0 && _SkippedStack.Count == 0)
      {
        stack = null;
        stackChildEnumerators = null;
        return false;
      }

      bool useMainStack = _Stack.Count > 0 && (_SkippedStack.Count == 0 || _Stack.GetLast().Position.Depth > _SkippedStack.GetLast().Position.Depth);
      stack = useMainStack ? _Stack : _SkippedStack;
      stackChildEnumerators = useMainStack ? _StackChildEnumerators : _SkippedStackChildEnumerators;
      return true;
    }

    private void UpdateStateFromVirtualNodeVisit(ref NodeVisit<TNode> nodeVisit)
    {
      Mode = nodeVisit.Mode;
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

        DisposeStack(_StackChildEnumerators);
        DisposeStack(_SkippedStackChildEnumerators);
      }
    }

    private void DisposeStack(RefSemiDeque<TChildEnumerator> stackChildEnumerators)
    {
      if (stackChildEnumerators == null)
        return;

      while (stackChildEnumerators.Count > 0)
      {
        _DisposeChildEnumeratorDelegate(ref stackChildEnumerators.GetLast());
        stackChildEnumerators.RemoveLast();
      }
    }

    ~DepthFirstTreenumerator()
    {
      Dispose(false);
    }

    #endregion Dispose
  }
}
