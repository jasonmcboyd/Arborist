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
    private RefSemiDeque<TChildEnumerator> _ChildEnumeratorQueue = new RefSemiDeque<TChildEnumerator>();

    private RefSemiDeque<InternalNodeVisitState> _Stack = new RefSemiDeque<InternalNodeVisitState>();
    private RefSemiDeque<TChildEnumerator> _ChildEnumeratorsStack = new RefSemiDeque<TChildEnumerator>();

    private int _RootNodesSeen = 0;
    private bool _HasCachedChild = false;
    private bool _RootsEnumeratorFinished = false;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      while (true)
      {
        if (_HasCachedChild)
        {
          _HasCachedChild = false;

          UpdateState(ref _Stack.GetLast());
          return true;
        }

        if (_Queue.Count == 0 && _Stack.Count == 0)
          return MoveToNextRootNode();

        if (Mode == TreenumeratorMode.SchedulingNode)
        {
          var onScheduling = OnScheduling(nodeTraversalStrategies);

          if (onScheduling.HasValue)
            return onScheduling.Value;
        }

        if (_Queue.Count == 0 && _Stack.Count == 0)
          return MoveToNextRootNode();

        var onVisiting = OnVisiting();

        if (onVisiting.HasValue)
          return onVisiting.Value;
      }
    }

    private bool? OnScheduling(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipSiblings))
      {
        if (Position.Depth == 0)
          _RootsEnumeratorFinished = true;
        else if (_Stack.Count > 1)
          _ChildEnumeratorsStack.GetFromBack(1).Dispose();
        else
          _ChildEnumeratorQueue.GetFirst().Dispose();
      }

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNodeAndDescendants))
        return SkipSubtree();

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode))
        return SkipNode();

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipDescendants))
        _ChildEnumeratorsStack.GetLast().Dispose();

      _Queue.AddLast(_Stack.RemoveLast());
      _ChildEnumeratorQueue.AddLast(_ChildEnumeratorsStack.RemoveLast());

      if (Backtrack())
        return true;

      ref var previousVisit = ref _Queue.GetFirst();
      previousVisit.VisitCount++;

      UpdateState(ref previousVisit);

      return true;
    }

    private bool? OnVisiting()
    {
      ref var previousVisit = ref _Queue.GetFirst();
      ref var previousVisitChildEnumerator = ref _ChildEnumeratorQueue.GetFirst();

      if (previousVisit.VisitCount == 0)
      {
        previousVisit.VisitCount++;
        UpdateState(ref previousVisit);
        return true;
      }

      if (TryPushNextChild(ref previousVisit, ref previousVisitChildEnumerator))
        return true;

      DisposeFirstItemInQueue();

      if (_Queue.Count == 0)
        return null;

      previousVisit = ref _Queue.GetFirst();
      previousVisit.VisitCount++;

      UpdateState(ref previousVisit);

      return true;
    }

    private bool MoveToNextRootNode()
    {
      if (_RootsEnumeratorFinished || !_RootsEnumerator.MoveNext())
      {
        _RootsEnumeratorFinished = true;
        return false;
      }

      var nodeVisit =
        new InternalNodeVisitState(
          _RootsEnumerator.Current,
          new NodePosition(_RootNodesSeen, 0));

      _RootNodesSeen++;

      _Stack.AddLast(nodeVisit);
      _ChildEnumeratorsStack.AddLast(_ChildEnumeratorFactory(new NodeContext<TNode>(nodeVisit.Node, nodeVisit.Position)));

      UpdateState(ref _Stack.GetLast());

      return true;
    }

    private bool? SkipNode()
    {
      if (TryPushNextChild(ref _Stack.GetLast(), ref _ChildEnumeratorsStack.GetLast()))
        return true;

      DisposeLastItemInChildrenStack();

      if (Backtrack())
        return true;

      if (_Queue.Count == 0)
        return null;

      ref var previousVisit = ref _Queue.GetFirst();

      previousVisit.VisitCount++;

      UpdateState(ref previousVisit);

      return true;
    }

    private bool? SkipSubtree()
    {
      DisposeLastItemInChildrenStack();

      if (Backtrack())
        return true;

      if (_Queue.Count == 0)
        return null;

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

        DisposeLastItemInChildrenStack();
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

      var childNodeVisit =
        new InternalNodeVisitState(
          childNodeSiblingContext.Node,
          new NodePosition(childNodeSiblingContext.SiblingIndex, nodeVisit.Position.Depth + 1));

      _Stack.AddLast(childNodeVisit);
      _ChildEnumeratorsStack.AddLast(_ChildEnumeratorFactory(new NodeContext<TNode>(childNodeVisit.Node, childNodeVisit.Position)));

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void DisposeFirstItemInQueue()
    {
      DisposeFirstItemInDeques(_Queue, _ChildEnumeratorQueue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void DisposeLastItemInChildrenStack()
    {
      DisposeLastItemsInDeques(_Stack, _ChildEnumeratorsStack);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void DisposeFirstItemInDeques(
      RefSemiDeque<InternalNodeVisitState> deque,
      RefSemiDeque<TChildEnumerator> dequeChildEnumerator)
    {
      deque.RemoveFirst();
      dequeChildEnumerator.RemoveFirst().Dispose();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void DisposeLastItemsInDeques(
      RefSemiDeque<InternalNodeVisitState> deque,
      RefSemiDeque<TChildEnumerator> dequeChildEnumerator)
    {
      deque.RemoveLast();
      dequeChildEnumerator.RemoveLast().Dispose();
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

        DisposeStack(_ChildEnumeratorQueue);
        DisposeStack(_ChildEnumeratorsStack);
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
    
    ~BreadthFirstTreenumerator()
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
