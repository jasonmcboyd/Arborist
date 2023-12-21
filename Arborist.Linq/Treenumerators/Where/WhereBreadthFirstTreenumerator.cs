using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class WhereBreadthFirstTreenumerator<TNode> : TreenumeratorWrapper<TNode>
  {
    public WhereBreadthFirstTreenumerator(
      ITreenumerator<TNode> innerTreenumerator,
      Func<NodeVisit<TNode>, bool> predicate)
      : base(innerTreenumerator)
    {
      _Predicate = predicate;
    }

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;

    private readonly Queue<NodeVisit<TNode>> _Queue = new Queue<NodeVisit<TNode>>();

    protected override bool OnMoveNext(ChildStrategy childStrategy)
    {
      if (!InnerTreenumerator.MoveNext(childStrategy))
        return false;

      while (!_Predicate(InnerTreenumerator.Current))
        if (!InnerTreenumerator.MoveNext(false))
          return false;

      Current = InnerTreenumerator.Current;

      return true;
    }
  }
}
