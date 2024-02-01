using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class RootfixScanBreadthFirstTreenumerator<TInner, TNode> : TreenumeratorWrapper<TInner, TNode>
  {
    public RootfixScanBreadthFirstTreenumerator(
      ITreenumerator<TInner> innerTreenumerator,
      Func<NodeVisit<TNode>, NodeVisit<TInner>, TNode> accumulator)
      : base(innerTreenumerator)
    {
      _Accumulator = accumulator;
      _SeedStep = default;
      _HasSeed = false;
    }

    public RootfixScanBreadthFirstTreenumerator(
      ITreenumerator<TInner> innerTreenumerator,
      Func<NodeVisit<TNode>, NodeVisit<TInner>, TNode> accumulator,
      TNode seed) : base(innerTreenumerator)
    {
      _Accumulator = accumulator;
      _SeedStep = NodeVisit.Create(seed, 1, 0, -1, false);
      _HasSeed = true;
    }

    private readonly Func<NodeVisit<TNode>, NodeVisit<TInner>, TNode> _Accumulator;

    private readonly NodeVisit<TNode> _SeedStep;

    private readonly bool _HasSeed;

    private readonly Stack<NodeVisit<TNode>> _CurrentBranch = new Stack<NodeVisit<TNode>>();

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      // TODO:
      throw new NotImplementedException();
      //if (!InnerTreenumerator.MoveNext(schedulingStrategy))
      //  return false;
      
      //var innerStep = InnerTreenumerator.Current;

      //if (innerStep.Depth == 0)
      //{
      //  var currentNode = _Accumulator(_SeedStep, innerStep);
      //  Current = NodeVisit.Create(currentNode, 1, innerStep.SiblingIndex, innerStep.Depth);
      //  _CurrentBranch.Push(Current);
      //}
      //else if (innerStep.Depth == Current.Depth)
      //{
      //  Current = NodeVisit.Create(Current.Node, 1, Current.SiblingIndex, Current.Depth);
      //  _CurrentBranch.Pop();
      //  _CurrentBranch.Push(Current);
      //}
      //else if (innerStep.Depth > Current.Depth)
      //{
      //  var currentNode = _Accumulator(Current, innerStep);
      //  Current = NodeVisit.Create(currentNode, 1, innerStep.SiblingIndex, innerStep.Depth);
      //  _CurrentBranch.Push(Current);
      //}
      //else
      //{
      //  _CurrentBranch.Pop();
      //  Current = _CurrentBranch.Peek();
      //}

      //return true;
    }
  }
}
