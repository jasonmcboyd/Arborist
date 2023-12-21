using System;

namespace Arborist
{
  public abstract class TreenumeratorBase<TNode> : ITreenumerator<TNode>
  {
    private NodeVisit<TNode> _Current;
    public NodeVisit<TNode> Current
    {
      get
      {
        if (!_StartedEnumeration)
          throw new InvalidOperationException("Enumeration has not begun.");

        if (_CompletedEnumeration)
          throw new InvalidOperationException("Enumeration has completed.");

        return _Current;
      }
      protected set => _Current = value;
    }

    private bool _StartedEnumeration;
    private bool _CompletedEnumeration;

    public abstract void Dispose();

    public bool MoveNext(ChildStrategy childStrategy)
    {
      if (_CompletedEnumeration)
        return false;

      _StartedEnumeration = true;

      if (OnMoveNext(childStrategy))
        return true;

      _CompletedEnumeration = true;
      return false;
    }

    protected abstract bool OnMoveNext(ChildStrategy childStrategy);
  }
}
