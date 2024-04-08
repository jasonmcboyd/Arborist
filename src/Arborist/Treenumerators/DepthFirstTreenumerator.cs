using Arborist.Core;
using Arborist.Virtualization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Treenumerators
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

    private readonly IEnumerator<TRootNode> _RootsEnumerator;
    private readonly Func<TRootNode, TNode> _Map;
    private readonly Func<TRootNode, IEnumerator<TRootNode>> _ChildrenGetter;

    private readonly VirtualNodeVisitPool<IEnumerator<TRootNode>> _NodeVisitPool =
      new VirtualNodeVisitPool<IEnumerator<TRootNode>>();

    private readonly Stack<VirtualNodeVisit<IEnumerator<TRootNode>>> _Stack =
      new Stack<VirtualNodeVisit<IEnumerator<TRootNode>>>();

    private readonly Stack<VirtualNodeVisit<IEnumerator<TRootNode>>> _SkippedStack =
      new Stack<VirtualNodeVisit<IEnumerator<TRootNode>>>();

    private bool _HasCachedChild = false;
    private int _MostRecentDepthTraversed = -1;

    public TraversalStrategy TraversalStrategy { get; private set; }
    public TNode Node { get; private set; }
    public int VisitCount { get; private set; }
    public TreenumeratorMode Mode { get; private set; }
    public NodePosition OriginalPosition { get; private set; }
    public NodePosition Position { get; private set; }

    public bool MoveNext(TraversalStrategy traversalStrategy)
    {
      if (Mode == TreenumeratorMode.EnumerationFinished)
        return false;

      if (Mode == TreenumeratorMode.EnumerationNotStarted)
        return OnStarting();

      while (true)
      {
        if (_HasCachedChild)
        {
          _HasCachedChild = false;

          UpdateStateFromVirtualNodeVisit(_Stack.Peek());

          return true;
        }

        if (Mode == TreenumeratorMode.SchedulingNode)
        {
          var onScheduling = OnScheduling(traversalStrategy);

          if (onScheduling.HasValue)
            return onScheduling.Value;
        }

        var onVisiting = OnVisiting();

        if (onVisiting.HasValue)
          return onVisiting.Value;
      }
    }

    private bool OnStarting()
    {
      if (!_RootsEnumerator.MoveNext())
      {
        OnEnumerationFinished();

        return false;
      }

      var sentinalNode = Enumerable.Range(0, 1).Select(_ => default(TRootNode)).GetEnumerator();

      sentinalNode.MoveNext();

      var sentinal =
        _NodeVisitPool
        .Lease(
          TreenumeratorMode.VisitingNode,
          sentinalNode,
          1,
          (0, -1),
          (0, -1),
          TraversalStrategy.TraverseSubtree);

      _Stack.Push(sentinal);

      var rootNode =
        _NodeVisitPool
        .Lease(
          TreenumeratorMode.SchedulingNode,
          _RootsEnumerator,
          0,
          (0, 0),
          (0, 0),
          TraversalStrategy.TraverseSubtree);

      _Stack.Push(rootNode);

      UpdateStateFromVirtualNodeVisit(rootNode);

      return true;
    }

    private bool? OnScheduling(TraversalStrategy traversalStrategy)
    {
      var previousVisit = _Stack.Pop();

      previousVisit.TraversalStrategy = traversalStrategy;

      if (traversalStrategy == TraversalStrategy.SkipNode)
      {
        var children = GetNodeChildren(previousVisit);

        if (children.MoveNext())
        {
          _SkippedStack.Push(previousVisit);

          var siblingIndex = 0;
          var depth = 0;

          var parentVisit = _Stack.Peek();
          siblingIndex = parentVisit.VisitCount - 1;
          depth = parentVisit.Position.Depth + 1;

          previousVisit =
            _NodeVisitPool
            .Lease(
              TreenumeratorMode.SchedulingNode,
              children,
              0,
              (0, previousVisit.OriginalPosition.Depth + 1),
              (siblingIndex, depth),
              TraversalStrategy.TraverseSubtree);

          _Stack.Push(previousVisit);

          UpdateStateFromVirtualNodeVisit(previousVisit);

          return true;
        }

        children.Dispose();

        if (previousVisit.Node.MoveNext())
        {
          previousVisit.Mode = TreenumeratorMode.SchedulingNode;
          previousVisit.OriginalPosition += (1, 0);

          _Stack.Push(previousVisit);

          UpdateStateFromVirtualNodeVisit(previousVisit);

          return true;
        }

        ReturnVirtualNodeVisit(previousVisit);

        return MoveUpTheTreeStack();
      }

      if (traversalStrategy == TraversalStrategy.SkipSubtree)
      {
        if (_Stack.Count == 1)
        {
          while (!previousVisit.Node.MoveNext())
          {
            if (_SkippedStack.Count == 0)
            {
              ReturnVirtualNodeVisit(previousVisit);

              OnEnumerationFinished();

              return false;
            }

            previousVisit = _SkippedStack.Pop();
          }

          previousVisit.Mode = TreenumeratorMode.SchedulingNode;
          previousVisit.OriginalPosition += (1, 0);
          previousVisit.TraversalStrategy = TraversalStrategy.TraverseSubtree;

          _Stack.Push(previousVisit);

          UpdateStateFromVirtualNodeVisit(previousVisit);

          return true;
        }

        if (previousVisit.Node.MoveNext())
        {
          var nextSiblingIndexIncrement = previousVisit.SkippingNode ? 0 : 1;

          previousVisit =
            _NodeVisitPool
            .Lease(
              TreenumeratorMode.SchedulingNode,
              previousVisit.Node,
              0,
              previousVisit.OriginalPosition.AddToSiblingIndex(1),
              previousVisit.Position.AddToSiblingIndex(nextSiblingIndexIncrement),
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

    private bool? OnVisiting()
    {
      while (true)
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
                (0, previousVisit.OriginalPosition.Depth + 1),
                (previousVisit.VisitCount - 1, previousVisit.Position.Depth + 1),
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
              previousVisit.OriginalPosition.AddToSiblingIndex(1),
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

        if (onMoveUpTheTreeStack.HasValue)
          return onMoveUpTheTreeStack.Value;
      }
    }

    private bool? MoveUpTheTreeStack()
    {
      VirtualNodeVisit<IEnumerator<TRootNode>> visit;

      while (true)
      {
        visit = GetParentVisit();

        if (visit.Position.Depth == -1)
        {
          OnEnumerationFinished();

          return false;
        }

        if (visit.TraversalStrategy == TraversalStrategy.SkipNode)
        {
          if (visit.Node.MoveNext())
          {
            var parentVisit = _Stack.Peek();

            if (_MostRecentDepthTraversed > parentVisit.OriginalPosition.Depth + 1)
              parentVisit.VisitCount++;

            visit.Mode = TreenumeratorMode.SchedulingNode;
            visit.OriginalPosition += (1, 0);
            visit.TraversalStrategy = TraversalStrategy.TraverseSubtree;
            visit.Position = (parentVisit.VisitCount - 1, parentVisit.Position.Depth + 1);

            _Stack.Push(visit);

            if (parentVisit.Position.Depth == -1
              || _MostRecentDepthTraversed <= parentVisit.OriginalPosition.Depth + 1)
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
          else
          {
            ReturnVirtualNodeVisit(visit);
            continue;
          }
        }

        if (_MostRecentDepthTraversed > visit.OriginalPosition.Depth)
        {
          visit.VisitCount++;

          _Stack.Push(visit);

          UpdateStateFromVirtualNodeVisit(visit);

          return true;
        }
        else
        {
          if (visit.Node.MoveNext())
          {
            var parentVisit = _Stack.Peek();

            parentVisit.VisitCount++;

            visit.Mode = TreenumeratorMode.SchedulingNode;
            visit.VisitCount = 0;
            visit.OriginalPosition += (1, 0);
            visit.TraversalStrategy = TraversalStrategy.TraverseSubtree;
            visit.Position = (parentVisit.VisitCount - 1, parentVisit.Position.Depth + 1);

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
          else
          {
            ReturnVirtualNodeVisit(visit);
            continue;
          }
        }
      }
    }

    private void OnEnumerationFinished()
    {
      Mode = TreenumeratorMode.EnumerationFinished;
    }

    private VirtualNodeVisit<IEnumerator<TRootNode>> GetParentVisit()
    {
      if (_Stack.Count == 0
        && _SkippedStack.Count == 0)
      {
        return null;
      }
      else if (_Stack.Count > 0
        && _SkippedStack.Count > 0)
      {
        return
          _Stack.Peek().OriginalPosition.Depth > _SkippedStack.Peek().OriginalPosition.Depth
          ? _Stack.Pop()
          : _SkippedStack.Pop();
      }
      else if (_SkippedStack.Count > 0)
      {
        return _SkippedStack.Pop();
      }
      else
      {
        return _Stack.Pop();
      }
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
      OriginalPosition = visit.OriginalPosition;
      Position = visit.Position;
      TraversalStrategy = visit.TraversalStrategy;

      if (Mode == TreenumeratorMode.VisitingNode
        && (TraversalStrategy == TraversalStrategy.SkipDescendants
          || TraversalStrategy == TraversalStrategy.TraverseSubtree))
        _MostRecentDepthTraversed = OriginalPosition.Depth;
    }

    private void ReturnVirtualNodeVisit(VirtualNodeVisit<IEnumerator<TRootNode>> virtualNodeVisit)
    {
      virtualNodeVisit.Node.Dispose();
      _NodeVisitPool.Return(virtualNodeVisit);
    }

    public void Dispose()
    {
      while (_Stack.Count > 0)
        ReturnVirtualNodeVisit(_Stack.Pop());

      while (_SkippedStack.Count > 0)
        ReturnVirtualNodeVisit(_SkippedStack.Pop());

      _RootsEnumerator?.Dispose();
    }
  }
}
