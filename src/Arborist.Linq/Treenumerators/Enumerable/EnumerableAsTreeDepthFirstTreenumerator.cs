using Arborist.Common;
using Arborist.Core;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators.Enumerator
{
  internal class EnumerableAsTreeDepthFirstTreenumerator<TNode> : ITreenumerator<TNode>
  {
    public EnumerableAsTreeDepthFirstTreenumerator(
      IEnumerable<TNode> enumerable)
    {
      _Enumerator = enumerable.GetEnumerator();
    }

    private IEnumerator<TNode> _Enumerator;
    private readonly RefSemiDeque<(TNode, int)> _Stack = new RefSemiDeque<(TNode, int)>();
    private bool _EnumerationFinished = false;

    public TNode Node { get; private set; } = default;
    public int VisitCount { get; private set; } = 0;
    public NodePosition Position { get; private set; } = new NodePosition(0, -1);
    public TreenumeratorMode Mode { get; private set; } = default;

    public bool MoveNext(NodeTraversalStrategy nodeTraversalStrategy)
    {
      if (_EnumerationFinished)
        return false;

      if (_Enumerator == null)
        return MoveUpStack();

      if (Mode == TreenumeratorMode.VisitingNode || _Stack.Count == 0)
        return OnVisiting();

      return OnScheduling(nodeTraversalStrategy);
    }

    private bool OnVisiting()
    {
      if (!EnumeratorMoveNext())
      {
        // This only happens for empty treenumerables
        if (_Stack.Count == 0)
        {
          _EnumerationFinished = true;
          return false;
        }

        _Stack.RemoveLast();

        return MoveUpStack();
      }

      AddScheduledNode();

      return true;
    }

    private bool OnScheduling(NodeTraversalStrategy nodeTraversalStrategy)
    {
      if (nodeTraversalStrategy == NodeTraversalStrategy.TraverseSubtree)
      {
        VisitCount = 1;
        Mode = TreenumeratorMode.VisitingNode;
        return true;
      }
      else if (nodeTraversalStrategy == NodeTraversalStrategy.SkipNode)
      {
        _Stack.RemoveLast();

        if (!EnumeratorMoveNext())
          return MoveUpStack();

        AddScheduledNode();

        return true;
      }
      else
      {
        DisposeEnumerator();

        if (nodeTraversalStrategy == NodeTraversalStrategy.SkipDescendants)
        {
          VisitCount = 1;
          Mode = TreenumeratorMode.VisitingNode;
          _Stack.RemoveLast();
          return true;
        }
        else
        {
          _Stack.RemoveLast();
          return MoveUpStack();
        }
      }
    }

    private bool EnumeratorMoveNext()
    {
      if (_Enumerator?.MoveNext() == true)
        return true;

      DisposeEnumerator();

      return false;
    }

    private void AddScheduledNode()
    {
      Node = _Enumerator.Current;
      VisitCount = 0;
      Position = new NodePosition(0, Position.Depth + 1);
      Mode = TreenumeratorMode.SchedulingNode;

      _Stack.AddLast((Node, Position.Depth));
    }

    private bool MoveUpStack()
    {
      if (_Stack.Count == 0)
      {
        _EnumerationFinished = true;
        return false;
      }

      (var node, var depth) = _Stack.RemoveLast();

      Node = node;
      VisitCount = 2;
      Position = new NodePosition(0, depth);
      Mode = TreenumeratorMode.VisitingNode;

      return true;
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
  }
}
