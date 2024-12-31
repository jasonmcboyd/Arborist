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
    }

    private readonly IEnumerator<TNode> _RootsEnumerator;
    private readonly Func<NodeContext<TNode>, TChildEnumerator> _ChildEnumeratorFactory;
    private readonly Func<TNode, TValue> _Map;

    private RefSemiDeque<InternalNodeVisitState> _Queue = new RefSemiDeque<InternalNodeVisitState>();
    private RefSemiDeque<TChildEnumerator> _ChildEnumeratorsQueue = new RefSemiDeque<TChildEnumerator>();

    private RefSemiDeque<InternalNodeVisitState> _Stack = new RefSemiDeque<InternalNodeVisitState>();
    private RefSemiDeque<TChildEnumerator> _ChildEnumeratorsStack = new RefSemiDeque<TChildEnumerator>();

    private int _RootNodesSeen = 0;
    private bool _HasCachedChild = false;
    private bool _RootsEnumeratorFinished = false;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (_Queue.Count == 0 && _Stack.Count == 0)
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

      PushNewNodeVisit(_RootsEnumerator.Current, new NodePosition(_RootNodesSeen, 0));

      _RootNodesSeen++;

      UpdateState(ref _Stack.GetLast());

      return true;
    }

    private bool OnScheduling(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipSiblings))
      {
        // TODO:
        //if (_Stack.Count <= 1)
        //  _RootsEnumeratorFinished = true;

        if (Position.Depth == 0)
          _RootsEnumeratorFinished = true;
        else if (_Stack.Count > 1)
          _ChildEnumeratorsStack.GetFromBack(1).Dispose();
        else
          _ChildEnumeratorsQueue.GetFirst().Dispose();
      }

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNodeAndDescendants))
        return SkipSubtree();

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode))
        return SkipNode();

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipDescendants))
        _ChildEnumeratorsStack.GetLast().Dispose();

      _Queue.AddLast(_Stack.RemoveLast());
      _ChildEnumeratorsQueue.AddLast(_ChildEnumeratorsStack.RemoveLast());

      if (Backtrack())
        return true;

      ref var previousVisit = ref _Queue.GetFirst();

      previousVisit.VisitCount++;

      UpdateState(ref previousVisit);

      return true;
    }

    private bool OnVisiting()
    {
      ref var previousVisit = ref _Queue.GetFirst();
      ref var previousVisitChildEnumerator = ref _ChildEnumeratorsQueue.GetFirst();

      if (TryPushNextChild(ref previousVisit, ref previousVisitChildEnumerator))
        return true;

      // We have exhausted all children of the current node. We can remove it.
      _Queue.RemoveFirst();
      _ChildEnumeratorsQueue.RemoveFirst().Dispose();

      // If there are no nodes left in the queue, return false.
      if (_Queue.Count == 0)
        return false;

      // Otherwise, 
      previousVisit = ref _Queue.GetFirst();

      previousVisit.VisitCount++;

      UpdateState(ref previousVisit);

      return true;
    }

    private bool SkipNode()
    {
      // TODO:
      //_Stack.RemoveLast();

      if (TryPushNextChild(ref _Stack.GetLast(), ref _ChildEnumeratorsStack.GetLast()))
        return true;

      // TODO:
      PopStacks();
      //_ChildEnumeratorsStack.RemoveLast().Dispose();

      if (Backtrack())
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
      PopStacks();

      if (Backtrack())
        return true;

      if (_Queue.Count == 0)
        return false;

      ref var previousVisit = ref _Queue.GetFirst();

      previousVisit.VisitCount++;

      UpdateState(ref previousVisit);

      return true;
    }

    private bool Backtrack()
    {
      while (_Stack.Count > 0)
      {
        ref var nodeVisit = ref _Stack.GetLast();
        ref var nodeVisitChildEnumerator = ref _ChildEnumeratorsStack.GetLast();

        if (TryPushNextChild(ref nodeVisit, ref _ChildEnumeratorsStack.GetLast(), nodeVisit.Position.Depth > _Stack.Count - 1))
          return true;

        PopStacks();
      }

      return MoveToNextRootNode();
    }

    private bool TryPushNextChild(
      ref InternalNodeVisitState nodeVisit,
      ref TChildEnumerator childEnumerator,
      bool cacheChild = false)
    {
      if (!childEnumerator.MoveNext(out var childNodeSiblingContext))
        return false;

      PushNewNodeVisit(childNodeSiblingContext.Node, new NodePosition(childNodeSiblingContext.SiblingIndex, nodeVisit.Position.Depth + 1));

      if (cacheChild && _Queue.Count > 0)
      {
        _HasCachedChild = true;
        _Queue.GetFirst().VisitCount++;
        UpdateState(ref _Queue.GetFirst());
      }
      else
      {
        UpdateState(ref _Stack.GetLast());
      }

      return true;
    }

    private void PushNewNodeVisit(
      TNode node,
      NodePosition nodePosition)
    {
      var internalNodeVisitState = new InternalNodeVisitState(node, nodePosition);
      var nodeChildEnumerator = _ChildEnumeratorFactory(new NodeContext<TNode>(internalNodeVisitState.Node, internalNodeVisitState.Position));

      _Stack.AddLast(internalNodeVisitState);
      _ChildEnumeratorsStack.AddLast(nodeChildEnumerator);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void PopStacks()
    {
      _Stack.RemoveLast();
      _ChildEnumeratorsStack.RemoveLast().Dispose();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void UpdateState(ref InternalNodeVisitState nodeVisit)
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
      DisposeStack(_ChildEnumeratorsStack);
    }

    private void DisposeStack(RefSemiDeque<TChildEnumerator> stackChildEnumerators)
    {
      if (stackChildEnumerators == null)
        return;

      while (stackChildEnumerators.Count > 0)
        stackChildEnumerators.RemoveLast().Dispose();
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

      public TNode Node;
      public int VisitCount;
      public NodePosition Position;
    }
  }
}
