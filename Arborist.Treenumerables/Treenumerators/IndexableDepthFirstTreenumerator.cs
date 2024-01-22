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
      NodeVisit<TValue> nextVisit;

      if (schedulingStrategy == SchedulingStrategy.ScheduleForTraversal)
      {
        // TODO: I don't think tracking visit counts for the shadow stack matters.
        previousShadowVisit = _ShadowStack.Pop();
        previousShadowVisit.IncrementVisitCount();
        _ShadowStack.Push(previousShadowVisit);

        if (_Stack.Count > 0)
        {
          var parentVisit = _Stack.Peek();

          nextVisit =
            NodeVisit
            .Create(
              previousShadowVisit.Node.Value,
              1,
              parentVisit.VisitCount - 1,
              parentVisit.Depth + 1);
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

      ////if (schedulingstrategy == schedulingstrategy.skipsubtree)
      ////{
      ////  _shadowstack.pop();

      ////todo: testing this commented out.
      ////  if (_shadowstack.count == 0)
      ////    return null;

      ////  return incrementvisitcount();


      ////todo: second attempt.
      ////  return null;

      ////  return onascendingtree();
      ////}

      // Set the previously visited shadow visit to skippped.
      previousShadowVisit = _ShadowStack.Pop().Skip();

      if (schedulingStrategy == SchedulingStrategy.SkipSubtree)
        previousShadowVisit = previousShadowVisit.With(null, null, null, previousShadowVisit.Node.ChildCount, null);

      _ShadowStack.Push(previousShadowVisit);

      return null;
    }

    private bool? OnVisitingNode()
    {
      if (_ShadowStack.Count == 0)
        return null;

      IndexableTreenumeratorNodeVisit<TNode, TValue> previousShadowVisit = _ShadowStack.Peek();

      if (!previousShadowVisit.Skipped
        && _Stack.Count > 0
        && Current.VisitCount == 0)
        return IncrementVisitCountOnTopOfStack();

      if (previousShadowVisit.HasNextChild())
        return ScheduleNextShadowVisitChild();

      if (_Stack.Count == 0)
        return null;

      if (Current.VisitCount == 1)
        return IncrementVisitCountOnTopOfStack();

      return null;
    }

    private bool? OnAscendingTree()
    {
      if (_ShadowStack.Count == 0)
        return null;

      IndexableTreenumeratorNodeVisit<TNode, TValue> previousShadowVisit = _ShadowStack.Peek();
      NodeVisit<TValue> previousVisit;

      bool ascendingFromSkippedNode = false;

      while (previousShadowVisit.Skipped
        && !previousShadowVisit.HasNextChild())
      {
        ascendingFromSkippedNode = true;

        _ShadowStack.Pop();

        if (_ShadowStack.Count == 0)
          return null;

        previousShadowVisit = _ShadowStack.Peek();
      }

      if (previousShadowVisit.HasNextChild())
      {
        if (ascendingFromSkippedNode)
          return ScheduleNextShadowVisitChild();

        return IncrementVisitCountOnTopOfStack();
      }

      previousVisit = _Stack.Peek();

      if (previousVisit.VisitCount == 1)
        return IncrementVisitCountOnTopOfStack();

      _Stack.Pop();
      _ShadowStack.Pop();

      if (_Stack.Count == 0)
        return null;

      previousVisit = _Stack.Pop().IncrementVisitCount();
      previousShadowVisit = _ShadowStack.Pop().IncrementVisitCount();

      _Stack.Push(previousVisit);
      _ShadowStack.Push(previousShadowVisit);

      Current = previousVisit;

      State = TreenumeratorState.VisitingNode;

      return true;
    }

    private bool ScheduleNextShadowVisitChild()
    {
      var previousShadowVisit = _ShadowStack.Pop();

      var nextShadowVisit = previousShadowVisit.GetNextChildVisit();

      var nextVisit = nextShadowVisit.ToNodeVisit();

      _ShadowStack.Push(previousShadowVisit.IncrementVisitedChildrenCount());
      _ShadowStack.Push(nextShadowVisit);

      State = TreenumeratorState.SchedulingNode;

      Current = nextVisit;

      return true;
    }

    private bool IncrementVisitCountOnTopOfStack()
    {
      var nextVisit = _Stack.Pop().IncrementVisitCount();
      _Stack.Push(nextVisit);
      Current = nextVisit;

      State = TreenumeratorState.VisitingNode;

      return true;
    }

    public override void Dispose()
    {
      _RootsEnumerator?.Dispose();
    }
  }
}
