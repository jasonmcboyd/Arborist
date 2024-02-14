using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class WithParentBreadthFirstTreenumerator<TNode>
    : TreenumeratorWrapper<TNode, WithParentNode<TNode>>
  {
    public WithParentBreadthFirstTreenumerator(ITreenumerator<TNode> innerTreenumerator)
      : base(innerTreenumerator)
    {
      _Queue.Enqueue(default);
      _Queue.Enqueue(default);
    }

    private readonly Queue<TNode> _Queue = new Queue<TNode>();

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      throw new NotImplementedException();
      //if (!InnerTreenumerator.MoveNext(schedulingStrategy))
      //  return false;

      //if (InnerTreenumerator.Node.VisitCount == 1)
      //{
      //  _Queue.Enqueue(InnerTreenumerator.Node.Node);

      //  if (InnerTreenumerator.Node.OriginalPosition.SiblingIndex == 0)
      //    _Queue.Dequeue();
      //}

      //var node =
      //  InnerTreenumerator.Node.OriginalPosition.Depth == 0
      //  ? new WithParentNode<TNode>(InnerTreenumerator.Node.Node)
      //  : new WithParentNode<TNode>(InnerTreenumerator.Node.Node, _Queue.Peek());

      //var visit =
      //  NodeVisit
      //  .Create(
      //    node,
      //    InnerTreenumerator.Node.VisitCount,
      //    InnerTreenumerator.Node.OriginalPosition,
      //    InnerTreenumerator.Node.Position,
      //    InnerTreenumerator.Node.SchedulingStrategy);

      //Current = visit;

      //return true;
    }
  }
}
