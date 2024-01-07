using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist
{
  public abstract class StackTreenumeratorBase<TStack, TNode> : ITreenumerator<TNode>
  {
    public StackTreenumeratorBase(Func<TStack, TNode> selector)
    {
      _Selector = selector;
    }

    private readonly Func<TStack, TNode> _Selector;

    private NodeVisit<TNode> _Current;
    public NodeVisit<TNode> Current
    {
      get
      {
        if (State == TreenumeratorState.EnumerationNotStarted)
          throw new InvalidOperationException("Enumeration has not started.");

        if (State == TreenumeratorState.EnumerationFinished)
          throw new InvalidOperationException("Enumeration has finished.");

        _Current = Stack.Last().WithNode(_Selector(Stack.Last().Node));

        return _Current;
      }
    }

    public TreenumeratorState State { get; protected set; } = TreenumeratorState.EnumerationNotStarted;

    protected List<NodeVisit<TStack>> Stack { get; } = new List<NodeVisit<TStack>>();

    public bool MoveNext(ChildStrategy childStrategy)
    {
      if (State == TreenumeratorState.EnumerationFinished)
        return false;

      OnMoveNext(childStrategy);

      return Stack.Count > 0;
    }

    protected abstract void OnMoveNext(ChildStrategy childStrategy);

    public abstract void Dispose();
  }

  public abstract class StackTreenumeratorBase<TNode> : StackTreenumeratorBase<TNode, TNode>
  {
    public StackTreenumeratorBase() : base(x => x)
    {
    }
  }
}
