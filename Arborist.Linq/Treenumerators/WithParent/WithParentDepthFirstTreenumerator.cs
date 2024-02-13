﻿using System.Linq;

namespace Arborist.Linq.Treenumerators
{

  internal class WithParentDepthFirstTreenumerator<TNode>
    : StackTreenumeratorWrapper<TNode, WithParentNode<TNode>, WithParentNode<TNode>>
  {
    public WithParentDepthFirstTreenumerator(ITreenumerator<TNode> innerTreenumerator)
      : base(innerTreenumerator, node => node)
    {
    }

    protected override void OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (!InnerTreenumerator.MoveNext(schedulingStrategy))
      {
        Stack.RemoveAt(Stack.Count - 1);
        return;
      }

      if (Stack.Count == 0)
      {
        var node = new WithParentNode<TNode>(InnerTreenumerator.Current.Node, default);

        var visit =
          NodeVisit
          .Create(
            node,
            InnerTreenumerator.Current.VisitCount,
            InnerTreenumerator.Current.OriginalPosition,
            InnerTreenumerator.Current.Position,
            InnerTreenumerator.Current.SchedulingStrategy);

        Stack.Add(visit);

        return;
      }

      var depthComparison = InnerTreenumerator.Current.OriginalPosition.Depth.CompareTo(Stack.Last().OriginalPosition.Depth);

      if (depthComparison < 0)
        Stack.RemoveAt(Stack.Count - 1);
      else if (depthComparison == 0)
        return;
      else
      {
        var parentNode = Stack.Last().Node.Node;

        var node = new WithParentNode<TNode>(InnerTreenumerator.Current.Node, parentNode);

        var step =
          NodeVisit
          .Create(
            node,
            InnerTreenumerator.Current.VisitCount,
            InnerTreenumerator.Current.OriginalPosition,
            InnerTreenumerator.Current.Position,
            InnerTreenumerator.Current.SchedulingStrategy);

        Stack.Add(step);
      }
    }
  }
}
