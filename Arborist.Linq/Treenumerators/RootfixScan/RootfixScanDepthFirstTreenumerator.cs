using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class RootfixScanDepthFirstTreenumerator<TInner, TNode> : TreenumeratorWrapper<TInner, TNode>
  {
    public RootfixScanDepthFirstTreenumerator(
      ITreenumerator<TInner> innerTreenumerator,
      Func<NodeVisit<TNode>, NodeVisit<TInner>, TNode> accumulator)
      : this(innerTreenumerator, accumulator, default)
    {
      _Accumulator = accumulator;
    }

    public RootfixScanDepthFirstTreenumerator(
      ITreenumerator<TInner> InnerTreenumerator,
      Func<NodeVisit<TNode>, NodeVisit<TInner>, TNode> accumulator,
      TNode seed) : base(InnerTreenumerator)
    {
      _Accumulator = accumulator;
      _SeedVisit = new NodeVisit<TNode>(seed, 1, 0, -1);
    }

    private readonly Func<NodeVisit<TNode>, NodeVisit<TInner>, TNode> _Accumulator;

    private readonly NodeVisit<TNode> _SeedVisit;

    private readonly Stack<NodeVisit<TNode>> CurrentBranch = new Stack<NodeVisit<TNode>>();

    protected override bool OnMoveNext(ChildStrategy childStrategy)
    {
      if (!InnerTreenumerator.MoveNext(childStrategy))
        return false;

      var innerVisit = InnerTreenumerator.Current;

      if (innerVisit.Depth == 0)
      {
        var currentNode = _Accumulator(_SeedVisit, innerVisit);
        Current = NodeVisit.Create(currentNode, innerVisit.VisitCount, innerVisit.SiblingIndex, innerVisit.Depth);
        CurrentBranch.Push(Current);
      }
      else if (innerVisit.Depth == Current.Depth)
      {
        Current = NodeVisit.Create(Current.Node, innerVisit.VisitCount, Current.SiblingIndex, Current.Depth);
        CurrentBranch.Pop();
        CurrentBranch.Push(Current);
      }
      else if (innerVisit.Depth > Current.Depth)
      {
        var currentNode = _Accumulator(Current, innerVisit);
        Current = NodeVisit.Create(currentNode, innerVisit.VisitCount, innerVisit.SiblingIndex, innerVisit.Depth);
        CurrentBranch.Push(Current);
      }
      else
      {
        CurrentBranch.Pop();
        Current = CurrentBranch.Peek();
      }

      return true;
    }
  }
}
