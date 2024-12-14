using Arborist.Core;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators.Enumerator
{
  internal class EnumerableAsTreeBreadthFirstTreenumerator<TNode> : ITreenumerator<TNode>
  {
    public EnumerableAsTreeBreadthFirstTreenumerator(
      IEnumerable<TNode> enumerable)
    {
      _Enumerator = enumerable.GetEnumerator();
    }

    private IEnumerator<TNode> _Enumerator;
    private readonly RefSemiDeque<NodeAndDepth> _Queue = new RefSemiDeque<NodeAndDepth>();
    private bool _EnumerationFinished = false;

    public TNode Node { get; private set; } = default;
    public int VisitCount { get; private set; } = 0;
    public NodePosition Position { get; private set; } = new NodePosition(0, -1);
    public TreenumeratorMode Mode { get; private set; } = default;

    public bool MoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (_EnumerationFinished)
        return false;

      if (_Queue.Count == 0)
        return OnEnumerationStarting();

      if (Mode == TreenumeratorMode.VisitingNode)
        return OnVisiting();

      return OnScheduling(nodeTraversalStrategies);
    }

    private bool OnEnumerationStarting()
    {
      if (EnumeratorMoveNext())
        return AddScheduledNode();
      else
        return FinishEnumeration();
    }

    private bool OnVisiting()
    {
      if (VisitCount == 1 && EnumeratorMoveNext())
        return AddScheduledNode();

      _Queue.RemoveFirst();

      if (_Queue.Count == 0)
        return FinishEnumeration();

      return VisitLeadNode();
    }

    private bool OnScheduling(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipDescendants))
      {
        if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode))
        {
          _Queue.RemoveLast();

          if (_Queue.Count > 0)
            return VisitLeadNode();

          return false;
        }

        VisitLeadNode();

        DisposeEnumerator();

        return true;
      }

      if (nodeTraversalStrategies.HasNodeTraversalStrategies(NodeTraversalStrategies.SkipNode))
      {
        _Queue.RemoveLast();

        if (EnumeratorMoveNext())
          return AddScheduledNode();

        if (_Queue.Count > 0)
          return VisitLeadNode();

        return false;
      }

      return VisitLeadNode();
    }

    private bool EnumeratorMoveNext()
    {
      if (_Enumerator?.MoveNext() == true)
        return true;

      DisposeEnumerator();

      return false;
    }

    private bool VisitLeadNode()
    {
      if (_Queue.Count == 0)
        return false;

      ref var nodeAndDepth = ref _Queue.GetFirst();

      nodeAndDepth.VisitCount++;

      Node = nodeAndDepth.Node;
      VisitCount = nodeAndDepth.VisitCount;
      Position = new NodePosition(0, nodeAndDepth.Depth);
      Mode = TreenumeratorMode.VisitingNode;

      return true;
    }

    private bool AddScheduledNode()
    {
      Node = _Enumerator.Current;
      VisitCount = 0;
      Position = new NodePosition(0, Position.Depth + 1);
      Mode = TreenumeratorMode.SchedulingNode;

      _Queue.AddLast(new NodeAndDepth(Node, 0, Position.Depth));

      return true;
    }

    private bool FinishEnumeration()
    {
      _EnumerationFinished = true;
      return false;
    }

    public void Dispose()
    {
      DisposeEnumerator();
    }

    private void DisposeEnumerator()
    {
      _Enumerator?.Dispose();
      _Enumerator = null;
    }

    private struct NodeAndDepth
    {
      public NodeAndDepth(
        TNode node,
        int visitCount,
        int depth)
      {
        Node = node;
        VisitCount = visitCount;
        Depth = depth;
      }

      public TNode Node { get; }
      public int VisitCount { get; set; }
      public int Depth { get; }
    }
  }
}
