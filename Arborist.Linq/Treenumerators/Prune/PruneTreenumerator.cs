using System;

namespace Arborist.Linq.Treenumerators
{
  internal class PruneTreenumerator<TNode> : TreenumeratorWrapper<TNode>
  {
    public PruneTreenumerator(
      ITreenumerator<TNode> innerTreenumerator,
      Func<NodeVisit<TNode>, bool> predicate)
      : base(innerTreenumerator)
    {
      _Predicate = predicate;
    }

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;
    private bool _StartedEnumeration = false;

    protected override bool OnMoveNext(bool skipChildren)
    {
      if (!_StartedEnumeration)
      {
        _StartedEnumeration = true;

        if (!InnerTreenumerator.MoveNext(skipChildren))
          return false;

        Current = InnerTreenumerator.Current;

        return true;
      }

      skipChildren = skipChildren || _Predicate(InnerTreenumerator.Current);

      if (InnerTreenumerator.MoveNext(skipChildren))
      {
        Current = InnerTreenumerator.Current;
        return true;
      }

      return false;
    }
  }
}
