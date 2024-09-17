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

    private readonly Stack<VirtualNodeVisit<IEnumerator<TRootNode>>> _SkippedStack =
      new Stack<VirtualNodeVisit<IEnumerator<TRootNode>>>();

    private bool _HasCachedChild = false;
    private bool _EnumerationStarted => Position.Depth != -1;

    protected override bool OnMoveNext(NodeTraversalStrategy nodeTraversalStrategy)
    {
      if (_Disposed)
        return false;

      if (!_EnumerationStarted)
        return OnStarting();

      if (_HasCachedChild)
      {
        _HasCachedChild = false;

        UpdateStateFromVirtualNodeVisit(_Stack.Peek());

        return true;
      }

      if (Mode == TreenumeratorMode.SchedulingNode)
      {
        var onScheduling = OnScheduling(nodeTraversalStrategy);

        if (onScheduling.HasValue)
          return onScheduling.Value;
      }

      var onVisiting = OnVisiting();

      return onVisiting;
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
          NodeTraversalStrategy.TraverseSubtree);

      _Stack.Push(sentinel);

      var rootNode =
        _NodeVisitPool
        .Lease(
          TreenumeratorMode.SchedulingNode,
          _RootsEnumerator,
          0,
          (0, 0),
          NodeTraversalStrategy.TraverseSubtree);

      _Stack.Push(rootNode);

      UpdateStateFromVirtualNodeVisit(rootNode);

      return true;
    }

    private bool? OnScheduling(NodeTraversalStrategy nodeTraversalStrategy)
    {
      var previousVisit = _Stack.Pop();

      previousVisit.TraversalStrategy = nodeTraversalStrategy;

      if (nodeTraversalStrategy == NodeTraversalStrategy.SkipNode)
      {
        var children = GetNodeChildren(previousVisit);

        if (children.MoveNext())
        {
          _SkippedStack.Push(previousVisit);

          previousVisit =
            _NodeVisitPool
            .Lease(
              TreenumeratorMode.SchedulingNode,
              children,
              0,
              (0, previousVisit.Position.Depth + 1),
              NodeTraversalStrategy.TraverseSubtree);

          _Stack.Push(previousVisit);

          UpdateStateFromVirtualNodeVisit(previousVisit);

          return true;
        }

        children.Dispose();

        if (previousVisit.Node.MoveNext())
        {
          var parentVisit = _Stack.Peek();

          parentVisit.VisitCount++;

          previousVisit.Mode = TreenumeratorMode.SchedulingNode;
          previousVisit.VisitCount = 0;
          previousVisit.Position += (1, 0);
          previousVisit.TraversalStrategy = NodeTraversalStrategy.TraverseSubtree;

          if (parentVisit.Position.Depth == -1)
          {
            UpdateStateFromVirtualNodeVisit(previousVisit);
          }
          else
          {
            _HasCachedChild = true;

            UpdateStateFromVirtualNodeVisit(parentVisit);
          }

          _Stack.Push(previousVisit);

          return true;
        }

        ReturnVirtualNodeVisit(previousVisit);

        return MoveUpTheTreeStack();
      }

      if (nodeTraversalStrategy == NodeTraversalStrategy.SkipSubtree)
      {
        if (_Stack.Count == 1)
        {
          while (!previousVisit.Node.MoveNext())
          {
            if (_SkippedStack.Count == 0)
            {
              ReturnVirtualNodeVisit(previousVisit);

              EnumerationFinished = true;

              return false;
            }

            ReturnVirtualNodeVisit(previousVisit);
            previousVisit = _SkippedStack.Pop();
          }

          previousVisit.Mode = TreenumeratorMode.SchedulingNode;
          previousVisit.Position += (1, 0);
          previousVisit.TraversalStrategy = NodeTraversalStrategy.TraverseSubtree;

          _Stack.Push(previousVisit);

          UpdateStateFromVirtualNodeVisit(previousVisit);

          return true;
        }

        if (previousVisit.Node.MoveNext())
        {
          var parentVisit = _Stack.Peek();

          parentVisit.VisitCount++;

          _HasCachedChild = true;

          UpdateStateFromVirtualNodeVisit(parentVisit);

          previousVisit.Mode = TreenumeratorMode.SchedulingNode;
          previousVisit.VisitCount = 0;
          previousVisit.Position += (1, 0);
          previousVisit.TraversalStrategy = NodeTraversalStrategy.TraverseSubtree;

          _Stack.Push(previousVisit);

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
        && previousVisit.TraversalStrategy != NodeTraversalStrategy.SkipDescendants)
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
              NodeTraversalStrategy.TraverseSubtree);

          _Stack.Push(previousVisit);
          _Stack.Push(childrenNode);

          UpdateStateFromVirtualNodeVisit(childrenNode);

          return true;
        }
      }

      if (previousVisit.Node.MoveNext())
      {
        previousVisit.Mode = TreenumeratorMode.SchedulingNode;
        previousVisit.VisitCount = 0;
        previousVisit.Position += (1, 0);
        previousVisit.TraversalStrategy = NodeTraversalStrategy.TraverseSubtree;

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

      return onMoveUpTheTreeStack.Value;
    }

    private bool? MoveUpTheTreeStack()
    {
      VirtualNodeVisit<IEnumerator<TRootNode>> visit;

      while (true)
      {
        visit = GetParentVisit();

        if (visit.Position.Depth == -1)
        {
          EnumerationFinished = true;

          ReturnVirtualNodeVisit(visit);

          return false;
        }

        if (visit.TraversalStrategy == NodeTraversalStrategy.SkipNode)
        {
          if (visit.Node.MoveNext())
          {
            var parentVisit = _Stack.Peek();

            parentVisit.VisitCount++;

            visit.Mode = TreenumeratorMode.SchedulingNode;
            visit.Position += (1, 0);
            visit.TraversalStrategy = NodeTraversalStrategy.TraverseSubtree;

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

        visit.VisitCount++;

        _Stack.Push(visit);

        UpdateStateFromVirtualNodeVisit(visit);

        return true;
      }
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
          _Stack.Peek().Position.Depth > _SkippedStack.Peek().Position.Depth
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
      if (visit.TraversalStrategy == NodeTraversalStrategy.SkipDescendants)
        return Enumerable.Empty<TRootNode>().GetEnumerator();

      return _ChildrenGetter(visit.Node.Current);
    }

    private void UpdateStateFromVirtualNodeVisit(VirtualNodeVisit<IEnumerator<TRootNode>> visit)
    {
      Mode = visit.Mode;
      Node = _Map(visit.Node.Current);
      VisitCount = visit.VisitCount;
      Position = visit.Position;
    }

    private void ReturnVirtualNodeVisit(VirtualNodeVisit<IEnumerator<TRootNode>> virtualNodeVisit)
    {
      virtualNodeVisit.Node.Dispose();
      _NodeVisitPool.Return(virtualNodeVisit);
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
        while (_Stack.Count > 0)
          ReturnVirtualNodeVisit(_Stack.Pop());

        while (_SkippedStack.Count > 0)
          ReturnVirtualNodeVisit(_SkippedStack.Pop());

        _RootsEnumerator?.Dispose();
      }
    }

    ~DepthFirstTreenumerator()
    {
      Dispose(false);
    }

    #endregion Dispose
  }
}
