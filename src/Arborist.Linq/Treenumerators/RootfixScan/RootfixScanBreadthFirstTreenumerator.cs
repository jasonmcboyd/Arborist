using Arborist.Core;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class RootfixScanBreadthFirstTreenumerator<TNode, TAccumulate>
    : TreenumeratorWrapper<TNode, TAccumulate>
  {
    public RootfixScanBreadthFirstTreenumerator(
      ITreenumerator<TNode> innerTreenumerator,
      Func<NodeVisit<TAccumulate>, NodeVisit<TNode>, TAccumulate> accumulator,
      TAccumulate seed) : base(innerTreenumerator)
    {
      _Accumulator = accumulator;

      var seedVisit =
        new NodeVisit<TAccumulate>(
          TreenumeratorState.VisitingNode,
          seed,
          1,
          (0, -1),
          (0, -1),
          SchedulingStrategy.TraverseSubtree);

      _CurrentLevel.Enqueue(seedVisit);
    }

    private readonly Func<NodeVisit<TAccumulate>, NodeVisit<TNode>, TAccumulate> _Accumulator;

    private Queue<NodeVisit<TAccumulate>> _CurrentLevel = new Queue<NodeVisit<TAccumulate>>();
    private Queue<NodeVisit<TAccumulate>> _NextLevel = new Queue<NodeVisit<TAccumulate>>();

    private readonly Stack<NodeVisit<TAccumulate>> _CurrentBranch = new Stack<NodeVisit<TAccumulate>>();

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      throw new NotImplementedException();
    }
  }
}
