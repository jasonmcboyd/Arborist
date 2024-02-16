using Arborist.Treenumerables.Virtualization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Treenumerables.Treenumerators
{
  internal sealed class DepthFirstTreenumerator<TRootNode, TNode>
    : ITreenumerator<TNode>
  {
    public DepthFirstTreenumerator(
      IEnumerable<TRootNode> rootNodes,
      Func<TRootNode, TNode> map,
      Func<TRootNode, IEnumerator<TRootNode>> childrenGetter)
    {
      _RootsEnumerator = rootNodes.GetEnumerator();
      _Map = map;
      _ChildrenGetter = childrenGetter;
    }

    private int _RootsEnumeratorIndex = 0;

    private readonly IEnumerator<TRootNode> _RootsEnumerator;
    private readonly Func<TRootNode, TNode> _Map;
    private readonly Func<TRootNode, IEnumerator<TRootNode>> _ChildrenGetter;

    private readonly VirtualNodeVisitPool<IEnumerator<TRootNode>> _NodeVisitPool =
      new VirtualNodeVisitPool<IEnumerator<TRootNode>>();

    private readonly Stack<VirtualNodeVisit<IEnumerator<TRootNode>>> _Stack =
      new Stack<VirtualNodeVisit<IEnumerator<TRootNode>>>();

    private bool _SkippingDescendants = false;
    private bool _HasCachedChild = false;

    public SchedulingStrategy SchedulingStrategy { get; private set; }

    public TNode Node { get; private set; }

    public int VisitCount { get; private set; }

    public TreenumeratorState State { get; private set; }

    public NodePosition OriginalPosition { get; private set; }

    public NodePosition Position { get; private set; }

    public bool MoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (_HasCachedChild)
      {
        _HasCachedChild = false;

        UpdateStateFromVirtualNodeVisit(_Stack.Peek());

        return true;
      }

      if (State == TreenumeratorState.EnumerationFinished)
        return false;

      if (State == TreenumeratorState.EnumerationNotStarted)
        return OnStarting();

      if (State == TreenumeratorState.SchedulingNode)
        return OnScheduling(schedulingStrategy);

      return OnVisiting();
    }

    private bool OnStarting()
    {
      if (!_RootsEnumerator.MoveNext())
      {
        State = TreenumeratorState.EnumerationFinished;
        return false;
      }

      State = TreenumeratorState.SchedulingNode;

      var rootNode =
        _NodeVisitPool
        .Lease(
          State,
          _RootsEnumerator,
          0,
          (_RootsEnumeratorIndex++, 0),
          default,
          SchedulingStrategy.ScheduleForTraversal);

      _Stack.Push(rootNode);

      UpdateStateFromVirtualNodeVisit(rootNode);

      return true;
    }

    private bool OnScheduling(SchedulingStrategy schedulingStrategy)
    {
      var previousVisit = _Stack.Pop();

      if (schedulingStrategy == SchedulingStrategy.SkipSubtree)
      {
        if (_Stack.Count == 0)
        {
          if (!previousVisit.Node.MoveNext())
          {
            ReturnVirtualNodeVisit(previousVisit);
            State = TreenumeratorState.EnumerationFinished;
            return false;
          }

          previousVisit =
            _NodeVisitPool
            .Lease(
              TreenumeratorState.SchedulingNode,
              _RootsEnumerator,
              0,
              (_RootsEnumeratorIndex++, 0),
              default,
              SchedulingStrategy.ScheduleForTraversal);

          _Stack.Push(previousVisit);

          UpdateStateFromVirtualNodeVisit(previousVisit);

          return true;
        }

        var parentVisit = _Stack.Pop();

        parentVisit.VisitCount++;

        _Stack.Push(parentVisit);

        if (previousVisit.Node.MoveNext())
        {
          previousVisit =
            _NodeVisitPool
            .Lease(
              TreenumeratorState.SchedulingNode,
              previousVisit.Node,
              0,
              previousVisit.OriginalPosition.AddToSiblingIndex(1),
              default,
              SchedulingStrategy.ScheduleForTraversal);

          _Stack.Push(previousVisit);

          _HasCachedChild = true;
        }
        else
        {
          ReturnVirtualNodeVisit(previousVisit);
        }

        UpdateStateFromVirtualNodeVisit(parentVisit);

        return true;
      }

      previousVisit.SchedulingStrategy = schedulingStrategy;
      previousVisit.VisitCount++;
      previousVisit.TreenumeratorState = TreenumeratorState.VisitingNode;

      _Stack.Push(previousVisit);

      UpdateStateFromVirtualNodeVisit(previousVisit);

      return true;
    }

    private bool OnVisiting()
    {
      var previousVisit = _Stack.Pop();

      if (previousVisit.VisitCount == 0)
      {
        previousVisit.VisitCount++;
        _Stack.Push(previousVisit);
        // TODO:
        UpdateStateFromVirtualNodeVisit(previousVisit);
        return true;
      }

      if (previousVisit.VisitCount == 1)
      {
        var children = GetNodeChildren(previousVisit);

        if (children?.MoveNext() != true || _SkippingDescendants)
        {
          previousVisit.VisitCount++;

          _Stack.Push(previousVisit);

          UpdateStateFromVirtualNodeVisit(previousVisit);

          return true;
        }

        var childrenNode =
          _NodeVisitPool
          .Lease(
            TreenumeratorState.SchedulingNode,
            children,
            0,
            (0, previousVisit.OriginalPosition.Depth + 1),
            default,
            SchedulingStrategy.ScheduleForTraversal);

        _Stack.Push(previousVisit);
        _Stack.Push(childrenNode);

        UpdateStateFromVirtualNodeVisit(childrenNode);

        return true;
      }

      if (_Stack.Count == 0)
      {
        if (!previousVisit.Node.MoveNext())
        {
          ReturnVirtualNodeVisit(previousVisit);
          return false;
        }

        State = TreenumeratorState.SchedulingNode;

        previousVisit =
          _NodeVisitPool
          .Lease(
            State,
            _RootsEnumerator,
            0,
            (_RootsEnumeratorIndex++, 0),
            default,
            SchedulingStrategy.ScheduleForTraversal);

        _Stack.Push(previousVisit);

        UpdateStateFromVirtualNodeVisit(previousVisit);

        return true;
      }

      var parentVisit = _Stack.Pop();
      parentVisit.VisitCount++;
      _Stack.Push(parentVisit);

      UpdateStateFromVirtualNodeVisit(parentVisit);

      if (previousVisit.Node.MoveNext())
      {
        previousVisit =
          _NodeVisitPool
          .Lease(
            TreenumeratorState.SchedulingNode,
            previousVisit.Node,
            0,
            previousVisit.OriginalPosition.AddToSiblingIndex(1),
            default,
            SchedulingStrategy.ScheduleForTraversal);

        _Stack.Push(previousVisit);

        _HasCachedChild = true;
      }
      else
      {
        ReturnVirtualNodeVisit(previousVisit);
      }

      return true;
    }

    private IEnumerator<TRootNode> GetNodeChildren(VirtualNodeVisit<IEnumerator<TRootNode>> visit)
    {
      if (visit.SchedulingStrategy == SchedulingStrategy.SkipDescendantSubtrees)
        return Enumerable.Empty<TRootNode>().GetEnumerator();

      return _ChildrenGetter(visit.Node.Current);
    }

    private void UpdateStateFromVirtualNodeVisit(VirtualNodeVisit<IEnumerator<TRootNode>> visit)
    {
      State = visit.TreenumeratorState;
      Node = _Map(visit.Node.Current);
      VisitCount = visit.VisitCount;
      OriginalPosition = visit.OriginalPosition;
      Position = visit.Position;
      SchedulingStrategy = visit.SchedulingStrategy;
    }

    private void ReturnVirtualNodeVisit(VirtualNodeVisit<IEnumerator<TRootNode>> virtualNodeVisit)
    {
      virtualNodeVisit.Node.Dispose();
      _NodeVisitPool.Return(virtualNodeVisit);
    }

    public void Dispose()
    {
      _RootsEnumerator?.Dispose();
    }
  }
}
