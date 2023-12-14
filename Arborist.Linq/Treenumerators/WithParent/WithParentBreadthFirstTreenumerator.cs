using System;

namespace Arborist.Linq.Treenumerators
{

  internal class WithParentBreadthFirstTreenumerator<TNode> : TreenumeratorWrapper<TNode, WithPeekNextVisit<TNode>>
  {
    public WithParentBreadthFirstTreenumerator(ITreenumerator<TNode> innerTreenumerator) : base(innerTreenumerator)
    {
    }

    protected override bool OnMoveNext(bool skipChildren)
    {
      throw new NotImplementedException();
    }
  }
}
