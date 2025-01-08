using Arborist.Core;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Arborist.Treenumerators
{
  public sealed class BreadthFirstTreenumerator<TValue, TNode, TChildEnumerator>
    : TreenumeratorBase<TValue>
    where TChildEnumerator : IChildEnumerator<TNode>
  {
    public BreadthFirstTreenumerator(
      IEnumerable<TNode> rootNodes,
      Func<NodeContext<TNode>, TChildEnumerator> childEnumeratorFactory,
      Func<TNode, TValue> map)
    {
      _RootsEnumerator = rootNodes.GetEnumerator();
      _ChildEnumeratorFactory = childEnumeratorFactory;
      _Map = map;

      // Add sentinel node.
      _Queue.AddLast(new InternalNodeVisitState<TNode>(default, new NodePosition(0, -1)));
      _ChildEnumeratorsQueue.AddLast(default);
    }

    private readonly IEnumerator<TNode> _RootsEnumerator;
    private readonly Func<NodeContext<TNode>, TChildEnumerator> _ChildEnumeratorFactory;
    private readonly Func<TNode, TValue> _Map;

    private RefSemiDeque<InternalNodeVisitState<TNode>> _Queue = new RefSemiDeque<InternalNodeVisitState<TNode>>();
    private RefSemiDeque<TChildEnumerator> _ChildEnumeratorsQueue = new RefSemiDeque<TChildEnumerator>();

    private RefSemiDeque<TChildEnumerator> _SkippedChildEnumeratorsStack = new RefSemiDeque<TChildEnumerator>();

    private int _RootNodesSeen = 0;
    private int _DepthOfLastScheduledNode = -1;
    private bool _HasCachedChild = false;
    private bool _RootsEnumeratorFinished = false;

    private int CurrentDepth => _Queue.GetFirst().Position.Depth + _SkippedChildEnumeratorsStack.Count;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (_HasCachedChild)
      {
        _HasCachedChild = false;

        UpdateState(ref _Queue.GetLast());

        return true;
      }

      if (Mode == TreenumeratorMode.SchedulingNode)
      {
        if (!nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode))
          _DepthOfLastScheduledNode = Position.Depth;

        return OnScheduling(nodeTraversalStrategies);
      }

      return OnVisiting();
    }

    private bool MoveToNextRootNode()
    {
      if (_RootsEnumeratorFinished || !_RootsEnumerator.MoveNext())
      {
        if (_Queue.Count > 0 && _Queue.GetFirst().Position.Depth == -1)
        {
          _Queue.RemoveFirst();
          _ChildEnumeratorsQueue.RemoveFirst().Dispose();
        }

        return false;
      }

      PushNewNodeVisit(_RootsEnumerator.Current, new NodePosition(_RootNodesSeen, 0));

      _RootNodesSeen++;

      UpdateState(ref _Queue.GetLast());

      return true;
    }

    private bool OnScheduling(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipSiblings))
      {
        _RootsEnumeratorFinished = true;

        _ChildEnumeratorsQueue.GetFirst().Dispose();

        for (int i = 0; i < _SkippedChildEnumeratorsStack.Count; i++)
          _SkippedChildEnumeratorsStack.GetFromBack(i).Dispose();
      }

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNodeAndDescendants))
        return SkipSubtree();

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode))
        return SkipNode();

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipDescendants))
        _ChildEnumeratorsQueue.GetLast().Dispose();

      if (_SkippedChildEnumeratorsStack.Count > 0)
      {
        if (Backtrack())
          return true;
      }

      if (MoveToNextRootNode())
        return true;

      if (_ChildEnumeratorsQueue.Count == 0)
        return false;

      ref var previousVisit = ref _Queue.GetFirst();

      previousVisit.VisitCount++;

      UpdateState(ref previousVisit);

      return true;
    }

    private bool OnVisiting()
    {
      if (TryPushNextChild())
        return true;

      if (_SkippedChildEnumeratorsStack.Count > 0)
      {
        if (Backtrack())
          return true;
      }

      // If there are no nodes left in the queue, return false.
      // This check is necessary when the tree is empty.
      if (_Queue.Count == 0)
        return false;

      // We have exhausted all children of the current node. We can remove it.
      _Queue.RemoveFirst();
      _ChildEnumeratorsQueue.RemoveFirst().Dispose();

      // If there are no nodes left in the queue, return false.
      if (_Queue.Count == 0)
        return false;

      // Otherwise, 
      ref var previousVisit = ref _Queue.GetFirst();
      ref var previousVisitChildEnumerator = ref _ChildEnumeratorsQueue.GetFirst();

      previousVisit = ref _Queue.GetFirst();

      previousVisit.VisitCount++;

      UpdateState(ref previousVisit);

      return true;
    }

    private bool SkipNode()
    {
      _Queue.RemoveLast();
      _SkippedChildEnumeratorsStack.AddLast(_ChildEnumeratorsQueue.RemoveLast());

      if (TryPushNextChild())
        return true;

      if (_SkippedChildEnumeratorsStack.Count > 0)
      {
        if (Backtrack())
          return true;
      }

      if (MoveToNextRootNode())
        return true;

      if (_Queue.Count == 0)
        return false;

      ref var previousVisit = ref _Queue.GetFirst();

      previousVisit.VisitCount++;

      UpdateState(ref previousVisit);

      return true;
    }

    private bool SkipSubtree()
    {
      _Queue.RemoveLast();
      _ChildEnumeratorsQueue.RemoveLast().Dispose();

      // TODO:
      if (TryPushNextChild(cacheChild: true))
        return true;

      if (_SkippedChildEnumeratorsStack.Count > 0)
      {
        if (Backtrack())
          return true;
      }

      // TODO:
      if (TryPushNextChild(cacheChild: true))
        return true;

      if (_Queue.Count == 0)
        return false;

      ref var previousVisit = ref _Queue.GetFirst();

      if (previousVisit.Position.Depth != -1)
      {
        previousVisit.VisitCount++;

        UpdateState(ref previousVisit);

        return true;
      }

      return false;
    }

    private bool Backtrack()
    {
      while (_SkippedChildEnumeratorsStack.Count > 0)
      {
        if (TryPushNextChild(cacheChild: _DepthOfLastScheduledNode > CurrentDepth))
          return true;

        _SkippedChildEnumeratorsStack.RemoveLast().Dispose();
      }

      return false;
    }

    private bool TryPushNextChild(bool cacheChild = false)
    {
      var usingStack = _SkippedChildEnumeratorsStack.Count > 0;

      if (!usingStack)
      {
        if (MoveToNextRootNode())
          return true;

        if (_ChildEnumeratorsQueue.Count == 0)
          return false;
      }

      ref var childEnumerator =
        ref usingStack
        ? ref _SkippedChildEnumeratorsStack.GetLast()
        : ref _ChildEnumeratorsQueue.GetFirst();

      if (!childEnumerator.MoveNext(out var childNodeSiblingContext))
        return false;

      var depth = _Queue.GetFirst().Position.Depth + _SkippedChildEnumeratorsStack.Count;

      PushNewNodeVisit(childNodeSiblingContext.Node, new NodePosition(childNodeSiblingContext.SiblingIndex, depth + 1));

      if (cacheChild && _Queue.GetFirst().Position.Depth != -1)
      {
        _HasCachedChild = true;
        _Queue.GetFirst().VisitCount++;
        UpdateState(ref _Queue.GetFirst());
      }
      else
      {
        UpdateState(ref _Queue.GetLast());
      }

      return true;
    }

    private void PushNewNodeVisit(
      TNode node,
      NodePosition nodePosition)
    {
      var internalNodeVisitState = new InternalNodeVisitState<TNode>(node, nodePosition);
      var nodeChildEnumerator = _ChildEnumeratorFactory(new NodeContext<TNode>(internalNodeVisitState.Node, internalNodeVisitState.Position));

      _Queue.AddLast(internalNodeVisitState);
      _ChildEnumeratorsQueue.AddLast(nodeChildEnumerator);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void UpdateState(ref InternalNodeVisitState<TNode> nodeVisit)
    {
      Mode = nodeVisit.VisitCount == 0 ? TreenumeratorMode.SchedulingNode : TreenumeratorMode.VisitingNode;
      Node = _Map(nodeVisit.Node);
      VisitCount = nodeVisit.VisitCount;
      Position = nodeVisit.Position;
    }

    #region Dispose

    protected override void OnDisposing()
    {
      base.OnDisposing();

      _RootsEnumerator?.Dispose();

      DisposeStack(_ChildEnumeratorsQueue);
      DisposeStack(_SkippedChildEnumeratorsStack);
    }

    private void DisposeStack(RefSemiDeque<TChildEnumerator> childEnumerators)
    {
      if (childEnumerators == null)
        return;

      while (childEnumerators.Count > 0)
        childEnumerators.RemoveLast().Dispose();
    }
    
    #endregion Dispose
  }
}
