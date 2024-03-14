using Arborist.Core;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators
{
  internal class SelectManyBreadthFirstTreenumerator<TSource, TResult>
    : TreenumeratorWrapper<TSource, TResult>
  {
    public SelectManyBreadthFirstTreenumerator(
      ITreenumerator<TSource> source,
      Func<TSource, ITreenumerable<TResult>> selector)
      : base(source)
    {
      _Selector = selector;

      _Stack.Push(new NodeVisit<TResult>(TreenumeratorState.VisitingNode, default, 1, (0, -1), (0, -1), SchedulingStrategy.TraverseSubtree));
    }

    public readonly Func<TSource, ITreenumerable<TResult>> _Selector;

    private Stack<NodeVisit<TResult>> _Stack = new Stack<NodeVisit<TResult>>();

    private ITreenumerator<TResult> _NodeTreenumerator;

    private bool _HasCachedVisit = false;

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (!InnerTreenumerator.MoveNext(schedulingStrategy))
        return false;

      throw new NotImplementedException();
    }

    private void UpdateStateFromNodeVisit(NodeVisit<TResult> visit)
    {
      State = visit.TreenumeratorState;
      Node = visit.Node;
      VisitCount = visit.VisitCount;
      OriginalPosition = visit.OriginalPosition;
      Position = visit.Position;
      SchedulingStrategy = visit.SchedulingStrategy;
    }
  }
}
