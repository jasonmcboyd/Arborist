using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{

  internal class WithPeekNextTreenumerator<TNode> : TreenumeratorWrapper<TNode, WithPeekNextVisit<TNode>>
  {
    public WithPeekNextTreenumerator(ITreenumerator<TNode> innerTreenumerator) : base(innerTreenumerator)
    {
    }

    private readonly Queue<NodeVisit<TNode>> _Queue = new Queue<NodeVisit<TNode>>();

    protected override bool OnMoveNext(bool skipChildren)
    {
      while (_Queue.Count < 2 && InnerTreenumerator.MoveNext(skipChildren))
        _Queue.Enqueue(InnerTreenumerator.Current);

      if (_Queue.Count == 2)
      {
        var next = _Queue.Dequeue();
       
        Current =
          new NodeVisit<WithPeekNextVisit<TNode>>(
            new WithPeekNextVisit<TNode>(next.Node, _Queue.Peek()),
            next.VisitCount,
            next.SiblingIndex,
            next.Depth);

        return true;
      }

      if (_Queue.Count == 1)
      {
        var next = _Queue.Dequeue();

        Current =
          new NodeVisit<WithPeekNextVisit<TNode>>(
            new WithPeekNextVisit<TNode>(next.Node),
            next.VisitCount,
            next.SiblingIndex,
            next.Depth);

        return true;
      }

      return false;
    }
  }
}
