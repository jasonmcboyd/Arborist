using Arborist.Core;
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

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (State == TreenumeratorState.EnumerationFinished)
        return false;

      if (State == TreenumeratorState.EnumerationNotStarted)
        return OnStarting();

      while (true)
      {
        if (_CurrentLevel.Count == 0)
          (_CurrentLevel, _NextLevel) = (_NextLevel, _CurrentLevel);

        if (_CurrentLevel.Count == 0)
        {
          State = TreenumeratorState.EnumerationFinished;
          return false;
        }

        if (State == TreenumeratorState.SchedulingNode)
        {
          var onScheduling = OnScheduling(schedulingStrategy);

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

    private bool? OnScheduling(SchedulingStrategy schedulingStrategy)
    {
      var scheduledVisit = _ChildrenStack.Peek();

      scheduledVisit.SchedulingStrategy = schedulingStrategy;

      var previousVisit = _CurrentLevel[0];

      if (schedulingStrategy == SchedulingStrategy.SkipNode)
      {
        if (MoveToFirstChild(scheduledVisit))
          return true;

        if (MoveToNextChild())
          return true;

        _CurrentLevel[0].VisitCount++;

        if (_CurrentLevel[0].VisitCount != 2)
          return null;

        if (_CurrentLevel[0].OriginalPosition.Depth == -1)
          return null;

        UpdateStateFromVirtualNodeVisit(_CurrentLevel[0]);

        return true;
      }

      if (schedulingStrategy == SchedulingStrategy.SkipSubtree)
      {
        if (MoveToNextChild())
          return true;

        _CurrentLevel[0].VisitCount++;

        if (_ChildrenStack.Count == 0
          && _CurrentLevel[0].VisitCount != 2)
          return null;

        if (_CurrentLevel[0].OriginalPosition.Depth == -1)
          return null;

        UpdateStateFromVirtualNodeVisit(_CurrentLevel[0]);

        return true;
      }

      scheduledVisit.TreenumeratorState = TreenumeratorState.VisitingNode;

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
        && previousVisit.SchedulingStrategy != SchedulingStrategy.SkipDescendants
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
        State = TreenumeratorState.VisitingNode;
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
          TreenumeratorState.VisitingNode,
          sentinalNode,
          1,
          (0, -1),
          (0, -1),
          SchedulingStrategy.TraverseSubtree);

      _CurrentLevel.AddToFront(sentinal);

      var children =
        _ChildrenVisitPool
        .Lease(
          TreenumeratorState.SchedulingNode,
          _RootsEnumerator,
          0,
          (0, 0),
          (0, 0),
          SchedulingStrategy.TraverseSubtree);

      _ChildrenStack.Push(children);

      var childVisit = GetNodeVisitFromChildEnumeratorVisit(children);

      UpdateStateFromVirtualNodeVisit(childVisit);

      return true;
    }

    private void OnEnumerationFinished()
    {
      State = TreenumeratorState.EnumerationFinished;
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

      children.TreenumeratorState = TreenumeratorState.SchedulingNode;
      children.VisitCount = 0;
      children.OriginalPosition = (0, visit.OriginalPosition.Depth + 1);
      children.Position = (previousVisit.VisitCount - 1, previousVisit.Position.Depth + 1);
      children.SchedulingStrategy = SchedulingStrategy.TraverseSubtree;

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

      children.TreenumeratorState = TreenumeratorState.SchedulingNode;
      children.VisitCount = 0;
      children.OriginalPosition = children.OriginalPosition.AddToSiblingIndex(1);
      children.Position = (previousVisit.VisitCount - 1, previousVisit.Position.Depth + 1);
      children.SchedulingStrategy = SchedulingStrategy.TraverseSubtree;

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

      if (visit.SchedulingStrategy == SchedulingStrategy.SkipDescendants)
        childrenEnumerator = Enumerable.Empty<TRootNode>().GetEnumerator();

      childrenEnumerator = _ChildrenGetter(nodeMap(visit));

      return
        _ChildrenVisitPool
        .Lease(
          TreenumeratorState.VisitingNode,
          childrenEnumerator,
          0,
          (0, visit.OriginalPosition.Depth + 1),
          (0, visit.Position.Depth + 1),
          SchedulingStrategy.TraverseSubtree);
    }

    private VirtualNodeVisit<TRootNode> GetNodeVisitFromChildEnumeratorVisit(VirtualNodeVisit<IEnumerator<TRootNode>> children)
    {
      return
        _NodeVisitPool
        .Lease(
          children.TreenumeratorState,
          children.Node.Current,
          children.VisitCount,
          children.OriginalPosition,
          children.Position,
          children.SchedulingStrategy);
    }

    private void UpdateStateFromVirtualNodeVisit(VirtualNodeVisit<TRootNode> visit)
    {
      State = visit.TreenumeratorState;
      Node = _Map(visit.Node);
      VisitCount = visit.VisitCount;
      OriginalPosition = visit.OriginalPosition;
      Position = visit.Position;
      SchedulingStrategy = visit.SchedulingStrategy;
    }

    public override void Dispose()
    {
      _RootsEnumerator?.Dispose();

      while (_ChildrenStack.Count > 0)
        ReturnChildrenVirtualNodeVisit(_ChildrenStack.Pop());
    }
  }
}
