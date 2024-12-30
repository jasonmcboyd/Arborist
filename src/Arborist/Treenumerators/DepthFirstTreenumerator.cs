using Arborist.Core;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
    private readonly RefSemiDeque<TChildEnumerator> _ChildEnumeratorStack = new RefSemiDeque<TChildEnumerator>();

    private int _RootNodesSeen = 0;
    private bool _HasCachedChild = false;
    private bool _RootsEnumeratorFinished = false;

    private int CurrentDepth => _ChildEnumeratorStack.Count - 1;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
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
      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipSiblings))
      {
        if (_Stack.Count == 1)
          _RootsEnumeratorFinished = true;

        var depthDelta = _ChildEnumeratorStack.Count - (_Stack.Count == 1 ? 0 : _Stack.GetFromBack(1).Position.Depth);

        for (int i = 1; i < depthDelta; i++)
          _ChildEnumeratorStack.GetFromBack(i).Dispose();
      }

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNodeAndDescendants))
        return Backtrack();

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode))
      {
        _Stack.RemoveLast();

        if (TryPushNextChild())
          return true;

        return Backtrack();
      }

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipDescendants))
        _ChildEnumeratorStack.GetLast().Dispose();

      ref var previousVisit = ref _Stack.GetLast();

      previousVisit.VisitCount++;

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
      while (true)
      {
        PopStacks();

        if (_ChildEnumeratorStack.Count == 0)
          return MoveToNextRootNode();

        if (_Stack.Count == 0)
        {
          if (TryPushNextChild())
            return true;

          continue;
        }

        ref var nodeVisit = ref _Stack.GetLast();

        var nodeSkipped = nodeVisit.Position.Depth < CurrentDepth;

        if (nodeSkipped)
        {
          if (TryPushNextChild(cacheChild: true))
            return true;

          continue;
        }

        nodeVisit.VisitCount++;

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

    private void PopStacks()
    {
      if (_Stack.Count > 0 && _Stack.GetLast().Position.Depth == CurrentDepth)
        _Stack.RemoveLast();

      _ChildEnumeratorStack.RemoveLast().Dispose();
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
      DisposeChildEnumeratorsStack();
    }

    private void DisposeChildEnumeratorsStack()
    {
      if (_ChildEnumeratorStack == null)
        return;

      while (_ChildEnumeratorStack.Count > 0)
        _ChildEnumeratorStack.RemoveLast().Dispose();
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
