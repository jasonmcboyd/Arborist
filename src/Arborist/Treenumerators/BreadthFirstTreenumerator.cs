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

    private RefSemiDeque<InternalNodeVisitState<TNode>> _Queue = new RefSemiDeque<InternalNodeVisitState<TNode>>();
    private RefSemiDeque<TChildEnumerator> _ChildEnumeratorsQueue = new RefSemiDeque<TChildEnumerator>();

    private RefSemiDeque<InternalNodeVisitState<TNode>> _Stack = new RefSemiDeque<InternalNodeVisitState<TNode>>();
    private RefSemiDeque<TChildEnumerator> _ChildEnumeratorsStack = new RefSemiDeque<TChildEnumerator>();

    private int _RootNodesSeen = 0;
    private bool _RootsEnumeratorFinished = false;
    private int _DepthOfLastActedOnNode = -1;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (Mode == TreenumeratorMode.VisitingNode || !nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode))
        _DepthOfLastActedOnNode = Position.Depth;

      if (_Queue.Count == 0 && _Stack.Count == 0)
        return MoveToNextRootNode();

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
        // TODO: I could probably avoid having to eagerly dispose of all of the
        // skipped node's child enumerators, but it would require storing more
        // state in the stack. I would have to benchmark it to see how it performed.
        for (int i = 1; i < _ChildEnumeratorsStack.Count; i++)
          _ChildEnumeratorsStack.GetFromBack(i).Dispose();

        if (Position.Depth == 0 || _ChildEnumeratorsQueue.Count == 0)
          _RootsEnumeratorFinished = true;
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
      if (TryPushNextChild(ref _Stack.GetLast(), ref _ChildEnumeratorsStack.GetLast()))
        return true;

      PopStacks();

      if (Backtrack())
        return true;

      if (_Queue.Count == 0)
        return false;

      ref var previousVisit = ref _Queue.GetFirst();

      if (_ChildEnumeratorsStack.Count == 0 && previousVisit.VisitCount != 0)
      {
        if (TryPushNextChild(ref _Queue.GetFirst(), ref _ChildEnumeratorsQueue.GetFirst()))
          return true;

        if (_DepthOfLastActedOnNode <= previousVisit.Position.Depth)
        {
          _Queue.RemoveFirst();
          _ChildEnumeratorsQueue.RemoveFirst().Dispose();

          if (_Queue.Count == 0)
            return false;
          else
            previousVisit = ref _Queue.GetFirst();
        }
      }

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

      if (Position.Depth != 0)
      {
        if (TryPushNextChild(ref _Queue.GetFirst(), ref _ChildEnumeratorsQueue.GetFirst()))
          return true;

        // Only remove from queue if the node has already been visited and we've
        // finished processing its descendants (depth check ensures we've backtracked
        // to the same level or higher).
        // If VisitCount == 0, the node hasn't been visited yet and should still be visited.
        if (_Queue.GetFirst().VisitCount != 0 && _DepthOfLastActedOnNode <= _Queue.GetFirst().Position.Depth)
        {
          _Queue.RemoveFirst();
          _ChildEnumeratorsQueue.RemoveFirst().Dispose();

          if (_Queue.Count == 0)
            return false;
        }
      }

      ref var previousVisit = ref _Queue.GetFirst();

      previousVisit.VisitCount++;

      UpdateState(ref previousVisit);

      return true;
    }

    private bool Backtrack()
    {
      var depthOfScheduleAncestor = _Queue.Count == 0 ? -1 : _Queue.GetFirst().Position.Depth;

      while (_Stack.Count > 0)
      {
        ref var nodeVisit = ref _Stack.GetLast();
        ref var nodeVisitChildEnumerator = ref _ChildEnumeratorsStack.GetLast();

        var cacheChild =
          nodeVisit.Position.Depth != 0
          && depthOfScheduleAncestor > -1
          && depthOfScheduleAncestor < _DepthOfLastActedOnNode;

        if (TryPushNextChild(ref nodeVisit, ref nodeVisitChildEnumerator))
          return true;

        PopStacks();
      }

      return MoveToNextRootNode();
    }

    private bool TryPushNextChild(
      ref InternalNodeVisitState<TNode> nodeVisit,
      ref TChildEnumerator childEnumerator)
    {
      if (!childEnumerator.MoveNext(out var childNodeSiblingContext))
        return false;

      PushNewNodeVisit(childNodeSiblingContext.Node, new NodePosition(childNodeSiblingContext.SiblingIndex, nodeVisit.Position.Depth + 1));

      UpdateState(ref _Stack.GetLast());

      return true;
    }

    private void PushNewNodeVisit(
      TNode node,
      NodePosition nodePosition)
    {
      var internalNodeVisitState = new InternalNodeVisitState<TNode>(node, nodePosition);
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
  }
}
