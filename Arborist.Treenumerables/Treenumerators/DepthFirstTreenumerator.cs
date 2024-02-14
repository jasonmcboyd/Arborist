using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Treenumerables.Treenumerators
{
  internal sealed class DepthFirstTreenumerator<TNode>
    : ITreenumerator<TNode>
  {
    public DepthFirstTreenumerator(
      IEnumerable<TNode> rootNodes,
      Func<TNode, IEnumerator<TNode>> childrenGetter)
    {
      _RootsEnumerator = rootNodes.GetEnumerator();
      _ChildrenGetter = childrenGetter;
    }

    private readonly VirtualNodeVisitPool<IEnumerator<TNode>> _NodeVisitPool =
      new VirtualNodeVisitPool<IEnumerator<TNode>>();

    private readonly IEnumerator<TNode> _RootsEnumerator;
    private readonly Func<TNode, IEnumerator<TNode>> _ChildrenGetter;

    private int _RootsEnumeratorIndex = 0;

    private readonly Stack<VirtualNodeVisit<IEnumerator<TNode>>> _Stack =
      new Stack<VirtualNodeVisit<IEnumerator<TNode>>>();

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
        Node = previousVisit.Node.Current;
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

    private IEnumerator<TNode> GetNodeChildren(VirtualNodeVisit<IEnumerator<TNode>> visit)
    {
      if (visit.SchedulingStrategy == SchedulingStrategy.SkipDescendantSubtrees)
        return Enumerable.Empty<TNode>().GetEnumerator();

      return _ChildrenGetter(visit.Node.Current);
    }

    private void UpdateStateFromVirtualNodeVisit(VirtualNodeVisit<IEnumerator<TNode>> visit)
    {
      State = visit.TreenumeratorState;
      Node = visit.Node.Current;
      VisitCount = visit.VisitCount;
      OriginalPosition = visit.OriginalPosition;
      Position = visit.Position;
      SchedulingStrategy = visit.SchedulingStrategy;
    }

    private void ReturnVirtualNodeVisit(VirtualNodeVisit<IEnumerator<TNode>> virtualNodeVisit)
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
