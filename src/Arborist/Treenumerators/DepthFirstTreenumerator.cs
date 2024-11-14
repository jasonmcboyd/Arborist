using Arborist.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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

    private readonly RefSemiDeque<NodeVisit<TNode>> _Stack = new RefSemiDeque<NodeVisit<TNode>>();
    private readonly RefSemiDeque<TChildEnumerator> _StackChildEnumerators = new RefSemiDeque<TChildEnumerator>();

    private readonly RefSemiDeque<NodeVisit<TNode>> _SkippedStack = new RefSemiDeque<NodeVisit<TNode>>();
    private readonly RefSemiDeque<TChildEnumerator> _SkippedStackChildEnumerators = new RefSemiDeque<TChildEnumerator>();

    private int CurrentDepth => _Stack.Count + _SkippedStack.Count - 1;

    private bool _HasCachedChild = false;
    private int _RootNodesSeen = 0;
    private bool _SkipRemainingRootNodes = false;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (_Disposed)
        return false;

      if (CurrentDepth == -1)
        return MoveToNextRootNode();

      if (_HasCachedChild)
      {
        _HasCachedChild = false;

        UpdateStateFromVirtualNodeVisit(ref _Stack.GetLast());

        return true;
      }

      if (Mode == TreenumeratorMode.SchedulingNode)
        return OnScheduling(nodeTraversalStrategies);

      return OnVisiting();
    }

    private bool MoveToNextRootNode()
    {
      if (_SkipRemainingRootNodes || !_RootsEnumerator.MoveNext())
        return false;

      PushNewNodeVisit(_RootsEnumerator.Current, _RootNodesSeen);

      _RootNodesSeen++;

      UpdateStateFromVirtualNodeVisit(ref _Stack.GetLast());

      return true;
    }

    private bool OnScheduling(NodeTraversalStrategies nodeTraversalStrategies)
    {
      ref var previousVisit = ref _Stack.GetLast();
      ref var previousVisitChildEnumerator = ref _StackChildEnumerators.GetLast();
      previousVisit.VisitCount++;

      previousVisit.NodeTraversalStrategies = nodeTraversalStrategies;

      if ((nodeTraversalStrategies & NodeTraversalStrategies.SkipSiblings) == NodeTraversalStrategies.SkipSiblings)
      {
        if (CurrentDepth == 0)
        {
          _SkipRemainingRootNodes = true;
        }
        else
        {
          if (_SkippedStack.Count > 0)
          {
            ref var skippedNode = ref _SkippedStack.GetLast();
            if (skippedNode.Position.Depth == previousVisit.Position.Depth - 1)
              skippedNode.NodeTraversalStrategies |= NodeTraversalStrategies.SkipDescendants;
            else
              _Stack.GetFromBack(1).NodeTraversalStrategies |= NodeTraversalStrategies.SkipDescendants;
          }
          else
          {
            _Stack.GetFromBack(1).NodeTraversalStrategies |= NodeTraversalStrategies.SkipDescendants;
          }
        }
      }

      if ((nodeTraversalStrategies & NodeTraversalStrategies.SkipNodeAndDescendants) == NodeTraversalStrategies.SkipNodeAndDescendants)
        return MoveUpTheTreeStack();

      if ((nodeTraversalStrategies & NodeTraversalStrategies.SkipNode) == NodeTraversalStrategies.SkipNode)
      {
        if (TryPushNextChild(ref previousVisitChildEnumerator, popMainStacksOntoSkippedStacks: true))
          return true;

        return MoveUpTheTreeStack();
      }

      previousVisit.Mode = TreenumeratorMode.VisitingNode;

      UpdateStateFromVirtualNodeVisit(ref previousVisit);

      return true;
    }

    private bool OnVisiting()
    {
      ref var previousVisit = ref _Stack.GetLast();
      ref var previousVisitChildEnumerator = ref _StackChildEnumerators.GetLast();

      if ((previousVisit.NodeTraversalStrategies & NodeTraversalStrategies.SkipDescendants) == 0
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

        if ((nodeVisit.NodeTraversalStrategies & NodeTraversalStrategies.SkipNode) == NodeTraversalStrategies.SkipNode)
        {
          if ((nodeVisit.NodeTraversalStrategies & NodeTraversalStrategies.SkipDescendants) == NodeTraversalStrategies.SkipDescendants)
          {
            PopStacks(stack, stackChildEnumerator);
            continue;
          }

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
      if (!nodeVisitChildEnumerator.MoveNext(out var childNodeAndSiblingIndex))
        return false;

      if (popMainStacksOntoSkippedStacks)
        PopMainStacksOntoSkippedStacks();

      PushNewNodeVisit(childNodeAndSiblingIndex.Node, childNodeAndSiblingIndex.SiblingIndex);

      if (cacheChild && _Stack.Count > 1)
      {
        ref var parent = ref _Stack.GetFromBack(1);

        _HasCachedChild = true;
        parent.VisitCount++;
        UpdateStateFromVirtualNodeVisit(ref parent);
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
          new NodePosition(childIndex, CurrentDepth + 1),
          NodeTraversalStrategies.TraverseAll);
      var nodeChildEnumerator = _ChildEnumeratorFactory(new NodeContext<TNode>(nodeVisit.Node, nodeVisit.Position));

      _Stack.AddLast(nodeVisit);
      _StackChildEnumerators.AddLast(nodeChildEnumerator);
    }

    private void PopMainStacks() => PopStacks(_Stack, _StackChildEnumerators);

    private void PopStacks(RefSemiDeque<NodeVisit<TNode>> stack, RefSemiDeque<TChildEnumerator> stackChildEnumerator)
    {
      stack.RemoveLast();
      stackChildEnumerator.RemoveLast().Dispose();
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
        stackChildEnumerators.RemoveLast().Dispose();
      }
    }

    ~DepthFirstTreenumerator()
    {
      Dispose(false);
    }

    #endregion Dispose
  }
}
