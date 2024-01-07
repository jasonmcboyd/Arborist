using System.Collections.Generic;
using System.Linq;

namespace Arborist.Treenumerables.Treenumerators
{
  internal sealed class IndexableDepthFirstTreenumerator<TNode, TValue>
    : StackTreenumeratorBase<TValue>
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

    protected override void OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (_ShadowStack.Count == 0)
      {
        OnEmptyStack();
        return;
      }

      if (State == TreenumeratorState.SchedulingNode)
      {
        OnSchedulingNode(schedulingStrategy);
        return;
      }

      OnVisitingNode();
    }

    private void OnEmptyStack()
    {
      if (!_RootsEnumerator.MoveNext())
        return;

      _RootsEnumeratorIndex++;

      if (State != TreenumeratorState.SchedulingNode)
        _RootsSiblingIndex++;

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
      Stack.Add(nextVisit.ToNodeVisit());
    }

    private void OnSchedulingNode(SchedulingStrategy schedulingStrategy)
    {
      if (schedulingStrategy == SchedulingStrategy.ScheduleForTraversal)
      {
        OnVisitingNode();
        return;
      }

      if (schedulingStrategy == SchedulingStrategy.SkipSubtree)
      {
        OnSkipSubtree();
        return;
      }

      OnSkipNode();
    }

    private void OnVisitingNode()
    {
      if (_ShadowStack.Count == 0)
        return;

      var previousVisit = _ShadowStack.Peek();

      if (previousVisit.VisitCount == 0
        || (!previousVisit.HasNextChild() && previousVisit.VisitCount == 1))
      {
        IncrementTopOfStackVisitCount();
        return;
      }

      if (previousVisit.HasNextChild())
      {
        OnHasNextChild();
        return;
      }

      _ShadowStack.Pop();
      Stack.RemoveAt(Stack.Count - 1);

      if (_ShadowStack.Count == 0)
      {
        OnEmptyStack();
        return;
      }

      previousVisit = _ShadowStack.Peek();

      if (previousVisit.Skipped)
      {
        OnSkipNode();
        return;
      }

      var nextShadowVisit = _ShadowStack.Pop().IncrementVisitCount();
      _ShadowStack.Push(nextShadowVisit);

      var nextVisit = Stack[Stack.Count - 1].IncrementVisitCount();
      Stack[Stack.Count - 1] = nextVisit;
    }

    private void OnSkipSubtree()
    {
      _ShadowStack.Pop();
      Stack.RemoveAt(Stack.Count - 1);

      if (_ShadowStack.Count == 0)
      {
        OnEmptyStack();
        return;
      }

      OnVisitingNode();
    }

    private void OnSkipNode()
    {
      var previousVisit = _ShadowStack.Pop().Skip();
      _ShadowStack.Push(previousVisit);

      //if (!previousVisit.HasNextChild()
      //  && )
      //{
      //  OnSkipSubtree();
      //  return;
      //}

      if (State == TreenumeratorState.SchedulingNode)
        Stack.RemoveAt(Stack.Count - 1);

      if (previousVisit.HasNextChild())
      {
        OnHasNextChild();
        return;
      }

      _ShadowStack.Pop();
      if (Stack.Count > 0)
        Stack.RemoveAt(Stack.Count - 1);

      if (_ShadowStack.Count == 0)
      {
        OnEmptyStack();
        return;
      }

      previousVisit = _ShadowStack.Peek();

      if (previousVisit.Skipped)
      {
        OnSkipNode();
        return;
      }

      var nextVisit = _ShadowStack.Pop().IncrementVisitCount();

      _ShadowStack.Push(nextVisit);
      Stack.Add(nextVisit.ToNodeVisit());

      State = TreenumeratorState.VisitingNode;

    }

    private void OnHasNextChild()
    {
      var previousVisit = _ShadowStack.Pop();

      var nextVisit = previousVisit.GetNextChildVisit();

      previousVisit = previousVisit.IncrementVisitedChildrenCount();

      _ShadowStack.Push(previousVisit);
      _ShadowStack.Push(nextVisit);

      Stack.Add(nextVisit.ToNodeVisit());

      State = TreenumeratorState.SchedulingNode;
    }

    private void IncrementTopOfStackVisitCount()
    {
      var nextShadowVisit = _ShadowStack.Pop().IncrementVisitCount();
      var nextVisit = Stack.Last().IncrementVisitCount();

      if (nextVisit.Depth == 0 && nextVisit.VisitCount == 1)
        nextVisit = nextVisit.WithSiblingIndex(_RootsSiblingIndex);
      else if (Stack.Count > 1)
        nextVisit = nextVisit.WithSiblingIndex(Stack[Stack.Count - 2].VisitCount - 1);

      if (nextVisit.Depth != Stack.Count - 1)
        nextVisit = nextVisit.WithDepth(Stack.Count - 1);

      _ShadowStack.Push(nextShadowVisit);
      Stack[Stack.Count - 1] = nextVisit;

      State = TreenumeratorState.VisitingNode;
    }

    public override void Dispose()
    {
      _RootsEnumerator?.Dispose();
    }
  }
}
