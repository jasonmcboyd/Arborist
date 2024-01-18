using System.Collections.Generic;

namespace Arborist.Treenumerables.Treenumerators
{
  internal sealed class IndexableDepthFirstTreenumerator<TNode, TValue>
    : TreenumeratorBase<TValue>
    where TNode : INodeWithIndexableChildren<TNode, TValue>
  {
    public IndexableDepthFirstTreenumerator(IEnumerable<TNode> roots)
    {
      _RootsEnumerator = roots.GetEnumerator();
    }

    private readonly IEnumerator<TNode> _RootsEnumerator;

    private int _RootsEnumeratorIndex = -1;
    private int _RootsSiblingIndex = -1;

    private readonly Stack<IndexableTreenumeratorNodeVisit<TNode, TValue>> _ShadowStack = new Stack<IndexableTreenumeratorNodeVisit<TNode, TValue>>();
    private readonly Stack<NodeVisit<TValue>> _Stack = new Stack<NodeVisit<TValue>>();

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      while (true)
      {
        if (_ShadowStack.Count == 0)
          return OnEmptyStack();

        if (State == TreenumeratorState.SchedulingNode)
        {
          var onSchedulingNode = OnSchedulingNode(schedulingStrategy);

          if (onSchedulingNode.HasValue)
            return onSchedulingNode.Value;
        }

        var onTraversingNode = OnVisitingNode();

        if (onTraversingNode.HasValue)
          return onTraversingNode.Value;

        var onAscendingTree = OnAscendingTree();

        if (onAscendingTree.HasValue)
          return onAscendingTree.Value;
      }
    }

    private bool OnEmptyStack()
    {
      if (!_RootsEnumerator.MoveNext())
        return false;

      _RootsEnumeratorIndex++;

      var nextVisit =
        IndexableTreenumeratorNodeVisit
        .Create<TNode, TValue>(
          _RootsEnumerator.Current,
          0,
          _RootsEnumeratorIndex,
          0,
          0,
          false);

      State = TreenumeratorState.SchedulingNode;

      _ShadowStack.Push(nextVisit);

      Current = nextVisit.ToNodeVisit();

      return true;
    }

    private bool? OnSchedulingNode(SchedulingStrategy schedulingStrategy)
    {
      IndexableTreenumeratorNodeVisit<TNode, TValue> previousShadowVisit;
      NodeVisit<TValue> previousVisit;
      NodeVisit<TValue> nextVisit;

      if (schedulingStrategy == SchedulingStrategy.ScheduleForTraversal)
      {
        previousShadowVisit = _ShadowStack.Pop();
        previousShadowVisit.IncrementVisitCount();
        _ShadowStack.Push(previousShadowVisit);

        if (_Stack.Count > 0)
        {
          previousVisit = _Stack.Peek();

          nextVisit =
            NodeVisit
            .Create(
              previousShadowVisit.Node.Value,
              1,
              previousVisit.VisitCount - 1,
              previousVisit.Depth + 1);
        }
        else
        {
          _RootsSiblingIndex++;

          nextVisit =
            NodeVisit
            .Create(
              previousShadowVisit.Node.Value,
              1,
              _RootsSiblingIndex,
              0);
        }

        _Stack.Push(nextVisit);

        Current = nextVisit;

        State = TreenumeratorState.VisitingNode;

        return true;
      }

      if (schedulingStrategy == SchedulingStrategy.SkipSubtree)
      {
        _ShadowStack.Pop();

        if (_ShadowStack.Count == 0)
          return null;

        previousVisit = _Stack.Pop().IncrementVisitCount();

        _Stack.Push(previousVisit);

        Current = previousVisit;

        State = TreenumeratorState.VisitingNode;

        return true;
      }

      previousShadowVisit = _ShadowStack.Pop().Skip();

      _ShadowStack.Push(previousShadowVisit);

      return null;
    }

    private bool? OnVisitingNode()
    {
      if (_ShadowStack.Count == 0)
        return null;

      IndexableTreenumeratorNodeVisit<TNode, TValue> previousShadowVisit = _ShadowStack.Peek();
      IndexableTreenumeratorNodeVisit<TNode, TValue> nextShadowVisit;
      NodeVisit<TValue> previousVisit;
      NodeVisit<TValue> nextVisit;

      if (!previousShadowVisit.Skipped
        && _Stack.Count > 0
        && Current.VisitCount == 0)
      {
        nextVisit = _Stack.Pop().IncrementVisitCount();
        _Stack.Push(nextVisit);
        Current = nextVisit;
        return true;
      }

      if (previousShadowVisit.HasNextChild())
      {
        nextShadowVisit = previousShadowVisit.GetNextChildVisit();

        if (_Stack.Count > 0)
        {
          previousVisit = _Stack.Peek();

          nextVisit =
            NodeVisit
            .Create(
              nextShadowVisit.Node.Value,
              0,
              previousVisit.VisitCount - 1,
              previousVisit.Depth + 1);
        }
        else
        {
          _RootsSiblingIndex++;

          nextVisit =
            NodeVisit
            .Create(
              nextShadowVisit.Node.Value,
              0,
              _RootsSiblingIndex,
              0);
        }

        _ShadowStack.Push(_ShadowStack.Pop().IncrementVisitedChildrenCount());
        _ShadowStack.Push(nextShadowVisit);

        State = TreenumeratorState.SchedulingNode;

        Current = nextVisit;

        return true;
      }

      if (_Stack.Count == 0)
        return null;

      if (Current.VisitCount == 1)
      {
        nextVisit = _Stack.Pop().IncrementVisitCount();
        _Stack.Push(nextVisit);
        Current = nextVisit;
        return true;
      }

      if (!_ShadowStack.Peek().Skipped)
        _Stack.Pop();

      _ShadowStack.Pop();

      return null;
    }

    private bool? OnAscendingTree()
    {
      if (_ShadowStack.Count == 0)
        return null;

      IndexableTreenumeratorNodeVisit<TNode, TValue> previousShadowVisit = _ShadowStack.Pop();
      IndexableTreenumeratorNodeVisit<TNode, TValue> nextShadowVisit;
      NodeVisit<TValue> previousVisit;
      NodeVisit<TValue> nextVisit;

      while (previousShadowVisit.Skipped
        && !previousShadowVisit.HasNextChild())
      {
        if (_ShadowStack.Count == 0)
          return null;

        previousShadowVisit = _ShadowStack.Pop();
      }

      if (!previousShadowVisit.Skipped)
      {
        // TODO: Do I need this check?
        if (_Stack.Count == 0)
          return null;

        previousVisit = _Stack.Pop().IncrementVisitCount();

        _Stack.Push(previousVisit);
        _ShadowStack.Push(previousShadowVisit);

        Current = previousVisit;

        State = TreenumeratorState.VisitingNode;

        return true;
      }

      nextShadowVisit = previousShadowVisit.GetNextChildVisit();

      nextVisit =
        NodeVisit
        .Create(
          nextShadowVisit.Node.Value,
          0,
          Current.VisitCount - 1,
          Current.Depth + 1);

      _ShadowStack.Push(previousShadowVisit.IncrementVisitedChildrenCount());
      _ShadowStack.Push(nextShadowVisit);

      State = TreenumeratorState.VisitingNode;

      _Stack.Push(nextVisit);

      Current = nextVisit;

      return true;
    }

    public override void Dispose()
    {
      _RootsEnumerator?.Dispose();
    }

    private enum TraversalDirection
    {
      Downward,
      Updward,
    }
  }
}
