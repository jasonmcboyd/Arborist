using System;
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

    private int _RootsEnumerationIndex = -1;

    private readonly Stack<IndexableTreenumeratorNodeVisit<TNode, TValue>> _Stack = new Stack<IndexableTreenumeratorNodeVisit<TNode, TValue>>();

    protected override bool OnMoveNext(ChildStrategy childStrategy)
    {
      if (_Stack.Count == 0)
        return OnEmptyStack();

      var scheduleForTraversal =
        _Stack.Peek().VisitCount != 1
        || childStrategy == ChildStrategy.ScheduleForTraversal;

      if (scheduleForTraversal)
        return OnScheduleForTraversal();

      if (childStrategy == ChildStrategy.SkipSubtree)
        return OnSkipSubtree();

      return OnSkipNode();
    }

    private bool OnEmptyStack()
    {
      if (!_RootsEnumerator.MoveNext())
        return false;

      _RootsEnumerationIndex++;

      var nextVisit =
        DepthFirstNodeVisit
        .Create<TNode, TValue>(
          _RootsEnumerator.Current,
          1,
          _RootsEnumerationIndex,
          0,
          0,
          false);

      _Stack.Push(nextVisit);

      Current = nextVisit.ToNodeVisit();

      return true;
    }

    private bool IncrementTopOfStackVisitCount()
    {
      var nextVisit = _Stack.Pop().IncrementVisitCount();

      _Stack.Push(nextVisit);

      Current = nextVisit.ToNodeVisit();

      return true;
    }

    private bool OnHasNextChild()
    {
      var previousVisit = _Stack.Pop();

      var nextVisit = previousVisit.GetNextChildVisit();

      previousVisit = previousVisit.IncrementVisitedChildrenCount();

      _Stack.Push(previousVisit);
      _Stack.Push(nextVisit);

      Current = nextVisit.ToNodeVisit();

      return true;
    }

    private bool OnScheduleForTraversal()
    {
      if (_Stack.Count == 0)
        return false;

      var previousVisit = _Stack.Peek();

      if (previousVisit.VisitCount == 1
        || (!previousVisit.HasNextChild() && previousVisit.VisitCount == 2))
        return IncrementTopOfStackVisitCount();

      if (previousVisit.HasNextChild())
        return OnHasNextChild();

      _Stack.Pop();

      if (_Stack.Count == 0)
        return OnEmptyStack();

      previousVisit = _Stack.Peek();

      if (previousVisit.Skipped)
        return OnSkipNode();

      IndexableTreenumeratorNodeVisit<TNode, TValue> nextVisit;

      nextVisit = _Stack.Pop().IncrementVisitCount();

      _Stack.Push(nextVisit);

      Current = nextVisit.ToNodeVisit();

      return true;
    }

    private bool OnSkipSubtree()
    {
      _Stack.Pop();

      if (_Stack.Count == 0)
        return OnEmptyStack();

      var previousNode = _Stack.Pop().IncrementVisitCount();

      _Stack.Push(previousNode);

      Current = previousNode.ToNodeVisit();

      return true;
    }

    private bool OnSkipNode()
    {
      var previousVisit = _Stack.Pop().Skip();
      _Stack.Push(previousVisit);

      if (previousVisit.HasNextChild())
        return OnHasNextChild();

      _Stack.Pop();

      if (_Stack.Count == 0)
        return OnEmptyStack();

      previousVisit = _Stack.Peek();

      if (previousVisit.Skipped)
        return OnSkipNode();

      IndexableTreenumeratorNodeVisit<TNode, TValue> nextVisit;

      nextVisit = _Stack.Pop().IncrementVisitCount();

      _Stack.Push(nextVisit);

      Current = nextVisit.ToNodeVisit();

      return true;
    }

    public override void Dispose()
    {
      _RootsEnumerator?.Dispose();
    }
  }
}
