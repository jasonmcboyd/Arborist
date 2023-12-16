using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist
{
  public abstract class StackTreenumeratorBase<TBranch, TNode> : ITreenumerator<TNode>
  {
    public StackTreenumeratorBase(Func<TBranch, TNode> selector)
    {
      _Selector = selector;
    }

    private readonly Func<TBranch, TNode> _Selector;

    private NodeVisit<TNode> _Current;
    public NodeVisit<TNode> Current
    {
      get
      {
        if (!_StartedEnumeration)
          throw new InvalidOperationException("Enumeration has not begun.");

        if (_CompletedEnumeration)
          throw new InvalidOperationException("Enumeration has completed.");

        _Current = Branch.Last().WithNode(_Selector(Branch.Last().Node));

        return _Current;
      }
    }

    private bool _StartedEnumeration = false;
    private bool _CompletedEnumeration = false;

    protected List<NodeVisit<TBranch>> Branch { get; } = new List<NodeVisit<TBranch>>();

    public bool MoveNext(bool skipChildren)
    {
      if (_CompletedEnumeration)
        return false;
      
      _StartedEnumeration = true;

      OnMoveNext(skipChildren);

      if (Branch.Count == 0)
        _CompletedEnumeration = true;

      return Branch.Count > 0;
    }

    protected abstract void OnMoveNext(bool skipChildren);

    public abstract void Dispose();
  }
}
