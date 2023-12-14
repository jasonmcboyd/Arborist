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

    private readonly Stack<NodeVisit<TNode>> _CurrentBranch = new Stack<NodeVisit<TNode>>();

    protected override bool OnMoveNext(bool skipChildren)
    {
      if (!InnerTreenumerator.MoveNext(skipChildren))
        return false;
      
      throw new NotImplementedException();
    }
  }
}
