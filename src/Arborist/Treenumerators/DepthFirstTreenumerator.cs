using Arborist.Core;
using Arborist.Virtualization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Treenumerators
{
  internal sealed class DepthFirstTreenumerator<TRootNode, TNode>
    : TreenumeratorBase<TNode>
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

    private readonly IEnumerator<TRootNode> _RootsEnumerator;
    private readonly Func<TRootNode, TNode> _Map;
    private readonly Func<TRootNode, IEnumerator<TRootNode>> _ChildrenGetter;

    private readonly VirtualNodeVisitPool<IEnumerator<TRootNode>> _NodeVisitPool =
      new VirtualNodeVisitPool<IEnumerator<TRootNode>>();

    private readonly Stack<VirtualNodeVisit<IEnumerator<TRootNode>>> _Stack =
      new Stack<VirtualNodeVisit<IEnumerator<TRootNode>>>();

    private bool _HasCachedChild = false;
    private int _MostRecentDepthTraversed = -1;

    private bool EnumerationStarted => Position.Depth != -1;

    protected override bool OnMoveNext(TraversalStrategy traversalStrategy)
    {
      if (!EnumerationStarted)
        return OnStarting();

      if (_HasCachedChild)
      {
        _HasCachedChild = false;

        UpdateStateFromVirtualNodeVisit(_Stack.Peek());

        return true;
      }

      if (Mode == TreenumeratorMode.SchedulingNode)
        return OnScheduling(traversalStrategy);

      return OnVisiting();
    }

    private bool OnStarting()
    {
      if (!_RootsEnumerator.MoveNext())
      {
        EnumerationFinished = true;

        return false;
      }

      var sentinelNode = Enumerable.Range(0, 1).Select(_ => default(TRootNode)).GetEnumerator();

      sentinelNode.MoveNext();

      var sentinel =
        _NodeVisitPool
        .Lease(
          TreenumeratorMode.VisitingNode,
          sentinelNode,
          1,
          (0, -1),
          TraversalStrategy.TraverseSubtree);

      _Stack.Push(sentinel);

      var rootNode =
        _NodeVisitPool
        .Lease(
          TreenumeratorMode.SchedulingNode,
          _RootsEnumerator,
          0,
          (0, 0),
          TraversalStrategy.TraverseSubtree);

      _Stack.Push(rootNode);

      UpdateStateFromVirtualNodeVisit(rootNode);

      return true;
    }

    private bool OnScheduling(TraversalStrategy traversalStrategy)
    {
      var previousVisit = _Stack.Pop();

      previousVisit.TraversalStrategy = traversalStrategy;

      if (traversalStrategy == TraversalStrategy.SkipSubtree)
      {
        if (_Stack.Count == 1)
        {
          if (!previousVisit.Node.MoveNext())
          {
            ReturnVirtualNodeVisit(previousVisit);

            EnumerationFinished = true;

            return false;
          }

          previousVisit.Mode = TreenumeratorMode.SchedulingNode;
          previousVisit.Position += (1, 0);
          previousVisit.TraversalStrategy = TraversalStrategy.TraverseSubtree;

          _Stack.Push(previousVisit);

          UpdateStateFromVirtualNodeVisit(previousVisit);

          return true;
        }

        if (previousVisit.Node.MoveNext())
        {
          previousVisit =
            _NodeVisitPool
            .Lease(
              TreenumeratorMode.SchedulingNode,
              previousVisit.Node,
              0,
              previousVisit.Position.AddToSiblingIndex(1),
              TraversalStrategy.TraverseSubtree);

          _Stack.Push(previousVisit);

          UpdateStateFromVirtualNodeVisit(previousVisit);

          return true;
        }

        ReturnVirtualNodeVisit(previousVisit);

        return MoveUpTheTreeStack();
      }

      previousVisit.VisitCount++;
      previousVisit.Mode = TreenumeratorMode.VisitingNode;

      _Stack.Push(previousVisit);

      UpdateStateFromVirtualNodeVisit(previousVisit);

      return true;
    }

    private bool OnVisiting()
    {
      var previousVisit = _Stack.Pop();

      if (previousVisit.VisitCount == 1
        && previousVisit.TraversalStrategy != TraversalStrategy.SkipDescendants)
      {
        var children = GetNodeChildren(previousVisit);

        if (children?.MoveNext() == true)
        {
          var childrenNode =
            _NodeVisitPool
            .Lease(
              TreenumeratorMode.SchedulingNode,
              children,
              0,
              (0, previousVisit.Position.Depth + 1),
              TraversalStrategy.TraverseSubtree);

          _Stack.Push(previousVisit);
          _Stack.Push(childrenNode);

          UpdateStateFromVirtualNodeVisit(childrenNode);

          return true;
        }
      }

      if (previousVisit.Node.MoveNext())
      {
        previousVisit =
          _NodeVisitPool
          .Lease(
            TreenumeratorMode.SchedulingNode,
            previousVisit.Node,
            0,
            previousVisit.Position.AddToSiblingIndex(1),
            TraversalStrategy.TraverseSubtree);

        var parentVisit = _Stack.Peek();

        _Stack.Push(previousVisit);

        parentVisit.VisitCount++;

        if (parentVisit.Position.Depth == -1)
        {
          UpdateStateFromVirtualNodeVisit(previousVisit);
        }
        else
        {
          _HasCachedChild = true;
          UpdateStateFromVirtualNodeVisit(parentVisit);
        }

        return true;
      }

      ReturnVirtualNodeVisit(previousVisit);

      var onMoveUpTheTreeStack = MoveUpTheTreeStack();

      return onMoveUpTheTreeStack;
    }

    private bool MoveUpTheTreeStack()
    {
      VirtualNodeVisit<IEnumerator<TRootNode>> visit;

      visit = _Stack.Pop();

      while (visit.Position.Depth != -1)
      {
        if (_MostRecentDepthTraversed > visit.Position.Depth)
        {
          visit.VisitCount++;

          _Stack.Push(visit);

          UpdateStateFromVirtualNodeVisit(visit);

          return true;
        }

        if (visit.Node.MoveNext())
        {
          var parentVisit = _Stack.Peek();

          parentVisit.VisitCount++;

          visit.Mode = TreenumeratorMode.SchedulingNode;
          visit.VisitCount = 0;
          visit.Position += (1, 0);
          visit.TraversalStrategy = TraversalStrategy.TraverseSubtree;

          _Stack.Push(visit);

          if (parentVisit.Position.Depth == -1)
          {
            UpdateStateFromVirtualNodeVisit(visit);
          }
          else
          {
            _HasCachedChild = true;
            UpdateStateFromVirtualNodeVisit(parentVisit);
          }

          return true;
        }

        ReturnVirtualNodeVisit(visit);

        visit = _Stack.Pop();
      }

      EnumerationFinished = true;

      return false;
    }

    private IEnumerator<TRootNode> GetNodeChildren(VirtualNodeVisit<IEnumerator<TRootNode>> visit)
    {
      if (visit.TraversalStrategy == TraversalStrategy.SkipDescendants)
        return Enumerable.Empty<TRootNode>().GetEnumerator();

      return _ChildrenGetter(visit.Node.Current);
    }

    private void UpdateStateFromVirtualNodeVisit(VirtualNodeVisit<IEnumerator<TRootNode>> visit)
    {
      Mode = visit.Mode;
      Node = _Map(visit.Node.Current);
      VisitCount = visit.VisitCount;
      Position = visit.Position;
      TraversalStrategy = visit.TraversalStrategy;

      if (Mode == TreenumeratorMode.VisitingNode
        && (TraversalStrategy == TraversalStrategy.SkipDescendants
          || TraversalStrategy == TraversalStrategy.TraverseSubtree))
        _MostRecentDepthTraversed = Position.Depth;
    }

    private void ReturnVirtualNodeVisit(VirtualNodeVisit<IEnumerator<TRootNode>> virtualNodeVisit)
    {
      virtualNodeVisit.Node.Dispose();
      _NodeVisitPool.Return(virtualNodeVisit);
    }

    public override void Dispose()
    {
      while (_Stack.Count > 0)
        ReturnVirtualNodeVisit(_Stack.Pop());

      _RootsEnumerator?.Dispose();
    }
  }
}
