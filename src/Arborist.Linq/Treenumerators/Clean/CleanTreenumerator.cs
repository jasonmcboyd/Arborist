using Arborist.Linq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Linq.Treenumerators
{
  internal class CleanTreenumerator<TNode> : TreenumeratorWrapper<TNode>
  {
    public CleanTreenumerator(ITreenumerator<TNode> innerTreenumerator)
      : base(innerTreenumerator)
    {
    }

    private readonly Stack<NodeVisit<TNode>> _Stack = new Stack<NodeVisit<TNode>>();

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      while (true)
      {
        if (InnerTreenumerator.MoveNext(SchedulingStrategy.TraverseSubtree))
          return false;


      }
    }
  }
}
