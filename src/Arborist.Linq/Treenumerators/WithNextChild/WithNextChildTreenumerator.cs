using Arborist.Core;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{

  internal class WithNextChildTreenumerator<TNode>
    : TreenumeratorWrapper<TNode, WithPeekNextVisit<TNode>>
  {
    public WithNextChildTreenumerator(ITreenumerator<TNode> innerTreenumerator) : base(innerTreenumerator)
    {
    }

    private readonly Queue<NodeVisit<TNode>> _Queue = new Queue<NodeVisit<TNode>>();

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      throw new NotImplementedException();
    }
  }
}
