using Arborist.Core;
using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace Arborist.Treenumerators
{
  internal sealed class BreadthFirstTreenumerator<TValue, TNode, TChildEnumerator>
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

    private RefSemiDeque<NodeVisit<TNode>> _CurrentLevel = new RefSemiDeque<NodeVisit<TNode>>();
    private RefSemiDeque<TChildEnumerator> _CurrentLevelChildEnumerators = new RefSemiDeque<TChildEnumerator>();

    private RefSemiDeque<NodeVisit<TNode>> _NextLevel = new RefSemiDeque<NodeVisit<TNode>>();
    private RefSemiDeque<TChildEnumerator> _NextLevelChildEnumerators = new RefSemiDeque<TChildEnumerator>();

    private RefSemiDeque<NodeVisit<TNode>> _ChildrenStack = new RefSemiDeque<NodeVisit<TNode>>();
    private RefSemiDeque<TChildEnumerator> _ChildrenStackChildEnumerators = new RefSemiDeque<TChildEnumerator>();

    private int _RootNodesSeen = 0;
    private bool _HasCachedChild = false;
    private bool _SkipRemainingRootNodes = false;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      while (true)
      {
        if (_HasCachedChild)
        {
          OnHasCachedChild();
          return true;
        }

        if (_CurrentLevel.Count == 0 && _ChildrenStack.Count == 0)
        {
          var onCurrentLevelEmpty = OnCurrentLevelEmpty();

          if (onCurrentLevelEmpty.HasValue)
            return onCurrentLevelEmpty.Value;
        }

        if (Mode == TreenumeratorMode.SchedulingNode)
        {
          var onScheduling = OnScheduling(nodeTraversalStrategies);

          if (onScheduling.HasValue)
            return onScheduling.Value;
        }

        if (_CurrentLevel.Count == 0 && _ChildrenStack.Count == 0)
        {
          var onCurrentLevelEmpty = OnCurrentLevelEmpty();

          if (onCurrentLevelEmpty.HasValue)
            return onCurrentLevelEmpty.Value;
        }

        var onVisiting = OnVisiting();

        if (onVisiting.HasValue)
          return onVisiting.Value;
      }
    }

    private void OnHasCachedChild()
    {
      _HasCachedChild = false;

      UpdateStateFromVirtualNodeVisit(ref _ChildrenStack.GetLast());
    }

    private bool? OnCurrentLevelEmpty()
    {
      if (MoveToNextRootNode())
        return true;

      SwapCurrentLevelAndNextLevel();

      if (_CurrentLevel.Count == 0)
        return false;

      Mode = TreenumeratorMode.VisitingNode;

      return null;
    }

    private bool? OnScheduling(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if ((nodeTraversalStrategies & NodeTraversalStrategies.SkipSiblings) == NodeTraversalStrategies.SkipSiblings)
      {
        if (Position.Depth == 0)
        {
          _SkipRemainingRootNodes = true;
        }
        else
        {
          if (_ChildrenStack.Count > 1)
            _ChildrenStack.GetFromBack(1).NodeTraversalStrategies |= NodeTraversalStrategies.SkipDescendants;
          else
            _CurrentLevel.GetFirst().NodeTraversalStrategies |= NodeTraversalStrategies.SkipDescendants;
        }
      }

      if ((nodeTraversalStrategies & NodeTraversalStrategies.SkipNodeAndDescendants) == NodeTraversalStrategies.SkipNodeAndDescendants)
        return SkipSubtree();

      if ((nodeTraversalStrategies & NodeTraversalStrategies.SkipNode) == NodeTraversalStrategies.SkipNode)
        return SkipNode();

      ref var scheduledVisit = ref _ChildrenStack.GetLast();
      ref var scheduledVisitChildEnumerator = ref _ChildrenStackChildEnumerators.GetLast();

      scheduledVisit.NodeTraversalStrategies = nodeTraversalStrategies;
      scheduledVisit.Mode = TreenumeratorMode.VisitingNode;

      PopChildrenStackOntoNextLevel();

      if (MoveUpTheChildrenStack())
        return true;

      if (_CurrentLevel.Count == 0)
        return null;

      ref var previousVisit = ref _CurrentLevel.GetFirst();
      previousVisit.VisitCount++;

      UpdateStateFromVirtualNodeVisit(ref previousVisit);

      return true;
    }

    private bool? OnVisiting()
    {
      ref var previousVisit = ref _CurrentLevel.GetFirst();
      ref var previousVisitChildEnumerator = ref _CurrentLevelChildEnumerators.GetFirst();

      if (previousVisit.VisitCount == 0)
      {
        previousVisit.VisitCount++;
        UpdateStateFromVirtualNodeVisit(ref previousVisit);
        return true;
      }

      if ((previousVisit.NodeTraversalStrategies & NodeTraversalStrategies.SkipDescendants) == 0
        && TryPushNextChild(ref previousVisit, ref previousVisitChildEnumerator))
      {
        return true;
      }

      DisposeFirstItemInCurrentLevel();

      if (_CurrentLevel.Count == 0)
        return null;

      previousVisit = ref _CurrentLevel.GetFirst();
      previousVisit.VisitCount++;
      UpdateStateFromVirtualNodeVisit(ref previousVisit);

      return true;
    }

    private bool MoveToNextRootNode()
    {
      if (_SkipRemainingRootNodes || !_RootsEnumerator.MoveNext())
        return false;

      var nodeVisit =
        new NodeVisit<TNode>(
          TreenumeratorMode.SchedulingNode,
          _RootsEnumerator.Current,
          0,
          new NodePosition(_RootNodesSeen, 0),
          NodeTraversalStrategies.TraverseAll);

      _RootNodesSeen++;

      _ChildrenStack.AddLast(nodeVisit);
      _ChildrenStackChildEnumerators.AddLast(_ChildEnumeratorFactory(new NodeContext<TNode>(nodeVisit.Node, nodeVisit.Position)));

      UpdateStateFromVirtualNodeVisit(ref _ChildrenStack.GetLast());

      return true;
    }

    private bool? SkipNode()
    {
      ref var scheduledVisit = ref _ChildrenStack.GetLast();
      ref var scheduledVisitChildEnumerator = ref _ChildrenStackChildEnumerators.GetLast();

      if (TryPushNextChild(ref scheduledVisit, ref scheduledVisitChildEnumerator))
        return true;

      DisposeLastItemInChildrenStack();

      if (MoveUpTheChildrenStack())
        return true;

      if (_CurrentLevel.Count == 0)
        return null;

      ref var previousVisit = ref _CurrentLevel.GetFirst();

      previousVisit.VisitCount++;

      UpdateStateFromVirtualNodeVisit(ref previousVisit);

      return true;

    }

    private bool? SkipSubtree()
    {
      DisposeLastItemInChildrenStack();

      if (MoveUpTheChildrenStack())
        return true;

      if (_CurrentLevel.Count == 0)
        return null;

      ref var previousVisit = ref _CurrentLevel.GetFirst();

      previousVisit.VisitCount++;

      UpdateStateFromVirtualNodeVisit(ref previousVisit);

      return true;
    }

    private bool MoveUpTheChildrenStack()
    {
      while (_ChildrenStack.Count > 0)
      {
        ref var nodeVisit = ref _ChildrenStack.GetLast();
        ref var nodeVisitChildEnumerator = ref _ChildrenStackChildEnumerators.GetLast();

        if ((nodeVisit.NodeTraversalStrategies & NodeTraversalStrategies.SkipDescendants) == NodeTraversalStrategies.SkipDescendants)
        {
          DisposeLastItemInChildrenStack();
          continue;
        }

        if (TryPushNextChild(ref nodeVisit, ref nodeVisitChildEnumerator, true))
          return true;

        DisposeLastItemInChildrenStack();
      }

      // TODO: Should I return false here?
      return false;
    }

    private void PopChildrenStackOntoNextLevel()
    {
      _NextLevel.AddLast(_ChildrenStack.RemoveLast());
      _NextLevelChildEnumerators.AddLast(_ChildrenStackChildEnumerators.RemoveLast());
    }

    private void SwapCurrentLevelAndNextLevel()
    {
      (_CurrentLevel, _NextLevel) = (_NextLevel, _CurrentLevel);
      (_CurrentLevelChildEnumerators, _NextLevelChildEnumerators) = (_NextLevelChildEnumerators, _CurrentLevelChildEnumerators);
    }

    private bool TryPushNextChild(
      ref NodeVisit<TNode> nodeVisit,
      ref TChildEnumerator childEnumerator,
      bool cacheChild = false)
    {
      if (!childEnumerator.MoveNext(out var childNodeSiblingContext))
        return false;

      var childNodeVisit =
        new NodeVisit<TNode>(
          TreenumeratorMode.SchedulingNode,
          childNodeSiblingContext.Node,
          0,
          new NodePosition(childNodeSiblingContext.SiblingIndex, nodeVisit.Position.Depth + 1),
          NodeTraversalStrategies.TraverseAll);

      _ChildrenStack.AddLast(childNodeVisit);
      _ChildrenStackChildEnumerators.AddLast(_ChildEnumeratorFactory(new NodeContext<TNode>(childNodeVisit.Node, childNodeVisit.Position)));

      if (cacheChild && _CurrentLevel.Count > 0)
      {
        _HasCachedChild = true;
        _CurrentLevel.GetFirst().VisitCount++;
        UpdateStateFromVirtualNodeVisit(ref _CurrentLevel.GetFirst());
      }
      else
      {
        UpdateStateFromVirtualNodeVisit(ref _ChildrenStack.GetLast());
      }

      return true;
    }

    private void DisposeFirstItemInCurrentLevel()
    {
      DisposeFirstItemInDeques(_CurrentLevel, _CurrentLevelChildEnumerators);
    }

    private void DisposeLastItemInChildrenStack()
    {
      DisposeLastItemsInDeques(_ChildrenStack, _ChildrenStackChildEnumerators);
    }

    private void DisposeFirstItemInDeques(
      RefSemiDeque<NodeVisit<TNode>> deque,
      RefSemiDeque<TChildEnumerator> dequeChildEnumerator)
    {
      deque.RemoveFirst();
      dequeChildEnumerator.RemoveFirst().Dispose();
    }

    private void DisposeLastItemsInDeques(
      RefSemiDeque<NodeVisit<TNode>> deque,
      RefSemiDeque<TChildEnumerator> dequeChildEnumerator)
    {
      deque.RemoveLast();
      dequeChildEnumerator.RemoveLast().Dispose();
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

        DisposeStack(_CurrentLevelChildEnumerators);
        DisposeStack(_NextLevelChildEnumerators);
        DisposeStack(_ChildrenStackChildEnumerators);
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
  }
}
