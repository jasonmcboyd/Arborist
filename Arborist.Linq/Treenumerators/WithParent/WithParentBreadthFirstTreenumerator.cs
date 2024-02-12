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
      if (!InnerTreenumerator.MoveNext(schedulingStrategy))
        return false;

      if (InnerTreenumerator.Current.VisitCount == 1)
      {
        _Queue.Enqueue(InnerTreenumerator.Current.Node);

        if (InnerTreenumerator.Current.OriginalPosition.SiblingIndex == 0)
          _Queue.Dequeue();
      }

      var node =
        InnerTreenumerator.Current.OriginalPosition.Depth == 0
        ? new WithParentNode<TNode>(InnerTreenumerator.Current.Node)
        : new WithParentNode<TNode>(InnerTreenumerator.Current.Node, _Queue.Peek());

      var visit =
        NodeVisit
        .Create(
          node,
          InnerTreenumerator.Current.VisitCount,
          InnerTreenumerator.Current.OriginalPosition,
          InnerTreenumerator.Current.Position,
          InnerTreenumerator.Current.Skipped);

      Current = visit;

      return true;
    }
  }
}
