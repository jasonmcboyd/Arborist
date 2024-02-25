using Arborist.Core;
using System;
using System.Linq;

namespace Arborist.Linq.Treenumerators
{

  internal class WithParentDepthFirstTreenumerator<TNode>
    : TreenumeratorWrapper<TNode, WithParentNode<TNode>>
  {
    public WithParentDepthFirstTreenumerator(ITreenumerator<TNode> innerTreenumerator)
      : base(innerTreenumerator)
    {
    }

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      throw new NotImplementedException();
      //if (!InnerTreenumerator.MoveNext(schedulingStrategy))
      //{
      //  Stack.RemoveAt(Stack.Count - 1);
      //  return;
      //}

      //if (Stack.Count == 0)
      //{
      //  var node = new WithParentNode<TNode>(InnerTreenumerator.Node.Node, default);

      //  var visit =
      //    NodeVisit
      //    .Create(
      //      node,
      //      InnerTreenumerator.Node.VisitCount,
      //      InnerTreenumerator.Node.OriginalPosition,
      //      InnerTreenumerator.Node.Position,
      //      InnerTreenumerator.Node.SchedulingStrategy);

      //  Stack.Add(visit);

      //  return;
      //}

      //var depthComparison = InnerTreenumerator.Node.OriginalPosition.Depth.CompareTo(Stack.Last().OriginalPosition.Depth);

      //if (depthComparison < 0)
      //  Stack.RemoveAt(Stack.Count - 1);
      //else if (depthComparison == 0)
      //  return;
      //else
      //{
      //  var parentNode = Stack.Last().Node.Node;

      //  var node = new WithParentNode<TNode>(InnerTreenumerator.Node.Node, parentNode);

      //  var step =
      //    NodeVisit
      //    .Create(
      //      node,
      //      InnerTreenumerator.Node.VisitCount,
      //      InnerTreenumerator.Node.OriginalPosition,
      //      InnerTreenumerator.Node.Position,
      //      InnerTreenumerator.Node.SchedulingStrategy);

      //  Stack.Add(step);
      //}
    }
  }
}
