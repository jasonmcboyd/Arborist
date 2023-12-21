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
        if (!_StartedEnumeration)
          throw new InvalidOperationException("Enumeration has not begun.");

        if (_CompletedEnumeration)
          throw new InvalidOperationException("Enumeration has completed.");

        _Current = Stack.Last().WithNode(_Selector(Stack.Last().Node));

        return _Current;
      }
    }

    private bool _StartedEnumeration = false;
    private bool _CompletedEnumeration = false;

    protected List<NodeVisit<TStack>> Stack { get; } = new List<NodeVisit<TStack>>();

    public bool MoveNext(ChildStrategy childStrategy)
    {
      if (_CompletedEnumeration)
        return false;
      
      _StartedEnumeration = true;

      OnMoveNext(childStrategy);

      if (Stack.Count == 0)
        _CompletedEnumeration = true;

      return Stack.Count > 0;
    }

    protected abstract void OnMoveNext(ChildStrategy childStrategy);

    public abstract void Dispose();
  }
}
