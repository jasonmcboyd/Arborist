﻿using Arborist.Core;
using Arborist.Virtualization;
using Nito.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Treenumerators
{
  internal sealed class BreadthFirstTreenumerator<TRootNode, TNode>
    : TreenumeratorBase<TNode>
  {
    public BreadthFirstTreenumerator(
      IEnumerable<TRootNode> rootNodes,
      Func<TRootNode, TNode> map,
      Func<TRootNode, IEnumerator<TRootNode>> childrenGetter)
    {
      _RootsEnumerator = rootNodes.GetEnumerator();
      _Map = map;
      _ChildrenGetter = childrenGetter;
    }

    private readonly IEnumerator<TRootNode> _RootsEnumerator;
    private readonly Func<TRootNode, TNode> _Map;
    private readonly Func<TRootNode, IEnumerator<TRootNode>> _ChildrenGetter;

    private readonly VirtualNodeVisitPool<TRootNode> _NodeVisitPool =
      new VirtualNodeVisitPool<TRootNode>();

    private readonly VirtualNodeVisitPool<IEnumerator<TRootNode>> _ChildrenVisitPool =
      new VirtualNodeVisitPool<IEnumerator<TRootNode>>();

    private Deque<VirtualNodeVisit<TRootNode>> _CurrentLevel =
      new Deque<VirtualNodeVisit<TRootNode>>();

    private Deque<VirtualNodeVisit<TRootNode>> _NextLevel =
      new Deque<VirtualNodeVisit<TRootNode>>();

    private Stack<VirtualNodeVisit<IEnumerator<TRootNode>>> _ChildrenStack =
      new Stack<VirtualNodeVisit<IEnumerator<TRootNode>>>();

    protected override bool OnMoveNext(TraversalStrategy traversalStrategy)
    {
      if (Mode == TreenumeratorMode.EnumerationFinished)
        return false;

      if (Mode == TreenumeratorMode.EnumerationNotStarted)
        return OnStarting();

      while (true)
      {
        if (_CurrentLevel.Count == 0)
          (_CurrentLevel, _NextLevel) = (_NextLevel, _CurrentLevel);

        if (_CurrentLevel.Count == 0)
        {
          Mode = TreenumeratorMode.EnumerationFinished;
          return false;
        }

        if (Mode == TreenumeratorMode.SchedulingNode)
        {
          var onScheduling = OnScheduling(traversalStrategy);

          if (onScheduling.HasValue)
            return onScheduling.Value;
        }

        if (_CurrentLevel.Count == 0)
          continue;

        var onVisiting = OnVisiting();

        if (onVisiting.HasValue)
          return onVisiting.Value;
      }
    }

    private bool? OnScheduling(TraversalStrategy traversalStrategy)
    {
      var scheduledVisit = _ChildrenStack.Peek();

      scheduledVisit.TraversalStrategy = traversalStrategy;

      var previousVisit = _CurrentLevel[0];

      if (traversalStrategy == TraversalStrategy.SkipNode)
      {
        if (MoveToFirstChild(scheduledVisit))
          return true;

        if (MoveToNextChild())
          return true;

        _CurrentLevel[0].VisitCount++;

        return null;
      }

      if (traversalStrategy == TraversalStrategy.SkipSubtree)
      {
        if (MoveToNextChild())
          return true;

        _CurrentLevel[0].VisitCount++;

        return null;
      }

      scheduledVisit.Mode = TreenumeratorMode.VisitingNode;

      _NextLevel.AddToBack(GetNodeVisitFromChildEnumeratorVisit(scheduledVisit));

      previousVisit.VisitCount++;

      if (previousVisit.OriginalPosition.Depth == -1)
        return null;

      UpdateStateFromVirtualNodeVisit(previousVisit);

      return true;
    }

    private bool? OnVisiting()
    {
      var previousVisit = _CurrentLevel[0];

      if (previousVisit.VisitCount == 0)
      {
        previousVisit.VisitCount++;
        UpdateStateFromVirtualNodeVisit(previousVisit);
        return true;
      }

      if (previousVisit.VisitCount == 1
        && previousVisit.TraversalStrategy != TraversalStrategy.SkipDescendants
        && MoveToFirstChild(previousVisit))
      {
        return true;
      }

      if (MoveToNextChild())
        return true;

      _NodeVisitPool.Return(_CurrentLevel[0]);

      _CurrentLevel.RemoveAt(0);

      if (_CurrentLevel.Count == 0)
      {
        Mode = TreenumeratorMode.VisitingNode;
        return null;
      }

      _CurrentLevel[0].VisitCount++;
      UpdateStateFromVirtualNodeVisit(_CurrentLevel[0]);

      return true;
    }

    private bool OnStarting()
    {
      if (!_RootsEnumerator.MoveNext())
      {
        OnEnumerationFinished();

        return false;
      }

      var sentinalNode = default(TRootNode);

      var sentinal =
        _NodeVisitPool
        .Lease(
          TreenumeratorMode.VisitingNode,
          sentinalNode,
          1,
          (0, -1),
          (0, -1),
          TraversalStrategy.TraverseSubtree);

      _CurrentLevel.AddToFront(sentinal);

      var children =
        _ChildrenVisitPool
        .Lease(
          TreenumeratorMode.SchedulingNode,
          _RootsEnumerator,
          0,
          (0, 0),
          (0, 0),
          TraversalStrategy.TraverseSubtree);

      _ChildrenStack.Push(children);

      var childVisit = GetNodeVisitFromChildEnumeratorVisit(children);

      UpdateStateFromVirtualNodeVisit(childVisit);

      return true;
    }

    private void OnEnumerationFinished()
    {
      Mode = TreenumeratorMode.EnumerationFinished;
    }

    private bool MoveToFirstChild(VirtualNodeVisit<TRootNode> visit) =>
      MoveToFirstChild(visit, x => x.Node);

    private bool MoveToFirstChild(VirtualNodeVisit<IEnumerator<TRootNode>> visit) =>
      MoveToFirstChild(visit, x => x.Node.Current);

    private bool MoveToFirstChild<T>(
      VirtualNodeVisit<T> visit,
      Func<VirtualNodeVisit<T>, TRootNode> map)
    {
      var children = GetChildren(visit, map);

      if (children?.Node.MoveNext() != true)
      {
        ReturnChildrenVirtualNodeVisit(children);
        return false;
      }

      var previousVisit = _CurrentLevel[0];

      children.Mode = TreenumeratorMode.SchedulingNode;
      children.VisitCount = 0;
      children.OriginalPosition = (0, visit.OriginalPosition.Depth + 1);
      children.Position = (previousVisit.VisitCount - 1, previousVisit.Position.Depth + 1);
      children.TraversalStrategy = TraversalStrategy.TraverseSubtree;

      _ChildrenStack.Push(children);

      UpdateStateFromVirtualNodeVisit(GetNodeVisitFromChildEnumeratorVisit(children));

      return true;
    }

    private bool MoveToNextChild()
    {
      while (_ChildrenStack.Count > 0
        && !_ChildrenStack.Peek().Node.MoveNext())
        ReturnChildrenVirtualNodeVisit(_ChildrenStack.Pop());

      if (_ChildrenStack.Count == 0)
        return false;

      var previousVisit = _CurrentLevel[0];

      var children = _ChildrenStack.Peek();

      children.Mode = TreenumeratorMode.SchedulingNode;
      children.VisitCount = 0;
      children.OriginalPosition = children.OriginalPosition.AddToSiblingIndex(1);
      children.Position = (previousVisit.VisitCount - 1, previousVisit.Position.Depth + 1);
      children.TraversalStrategy = TraversalStrategy.TraverseSubtree;

      UpdateStateFromVirtualNodeVisit(GetNodeVisitFromChildEnumeratorVisit(children));

      return true;
    }

    private void ReturnChildrenVirtualNodeVisit(VirtualNodeVisit<IEnumerator<TRootNode>> visit)
    {
      visit.Node.Dispose();
      _ChildrenVisitPool.Return(visit);
    }

    private VirtualNodeVisit<IEnumerator<TRootNode>> GetChildren<T>(
      VirtualNodeVisit<T> visit,
      Func<VirtualNodeVisit<T>, TRootNode> nodeMap)
    {
      IEnumerator<TRootNode> childrenEnumerator;

      if (visit.TraversalStrategy == TraversalStrategy.SkipDescendants)
        childrenEnumerator = Enumerable.Empty<TRootNode>().GetEnumerator();

      childrenEnumerator = _ChildrenGetter(nodeMap(visit));

      return
        _ChildrenVisitPool
        .Lease(
          TreenumeratorMode.VisitingNode,
          childrenEnumerator,
          0,
          (0, visit.OriginalPosition.Depth + 1),
          (0, visit.Position.Depth + 1),
          TraversalStrategy.TraverseSubtree);
    }

    private VirtualNodeVisit<TRootNode> GetNodeVisitFromChildEnumeratorVisit(VirtualNodeVisit<IEnumerator<TRootNode>> children)
    {
      return
        _NodeVisitPool
        .Lease(
          children.Mode,
          children.Node.Current,
          children.VisitCount,
          children.OriginalPosition,
          children.Position,
          children.TraversalStrategy);
    }

    private void UpdateStateFromVirtualNodeVisit(VirtualNodeVisit<TRootNode> visit)
    {
      Mode = visit.Mode;
      Node = _Map(visit.Node);
      VisitCount = visit.VisitCount;
      OriginalPosition = visit.OriginalPosition;
      Position = visit.Position;
      TraversalStrategy = visit.TraversalStrategy;
    }

    public override void Dispose()
    {
      _RootsEnumerator?.Dispose();

      while (_ChildrenStack.Count > 0)
        ReturnChildrenVirtualNodeVisit(_ChildrenStack.Pop());
    }
  }
}
