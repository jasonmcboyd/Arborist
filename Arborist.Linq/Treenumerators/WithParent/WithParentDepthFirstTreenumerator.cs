using System.Linq;

namespace Arborist.Linq.Treenumerators
{

  internal class WithParentDepthFirstTreenumerator<TNode>
    : StackTreenumeratorWrapper<TNode, WithParentNode<TNode>, WithParentNode<TNode>>
  {
    public WithParentDepthFirstTreenumerator(ITreenumerator<TNode> innerTreenumerator)
      : base(innerTreenumerator, node => node)
    {
    }

    protected override void OnMoveNext(bool skipChildren)
    {
      if (!InnerTreenumerator.MoveNext(skipChildren))
      {
        Branch.RemoveAt(Branch.Count - 1);
        return;
      }

      if (Branch.Count == 0)
      {
        var node = new WithParentNode<TNode>(InnerTreenumerator.Current.Node, default);

        var visit =
          NodeVisit
          .Create(
            node,
            InnerTreenumerator.Current.VisitCount,
            InnerTreenumerator.Current.SiblingIndex,
            InnerTreenumerator.Current.Depth);

        Branch.Add(visit);

        return;
      }

      var depthComparison = InnerTreenumerator.Current.Depth.CompareTo(Branch.Last().Depth);

      if (depthComparison < 0)
        Branch.RemoveAt(Branch.Count - 1);
      else if (depthComparison == 0)
        return;
      else
      {
        var parentNode = Branch.Last().Node.Node;

        var node = new WithParentNode<TNode>(InnerTreenumerator.Current.Node, parentNode);
        
        var step =
          NodeVisit
          .Create(
            node,
            InnerTreenumerator.Current.VisitCount,
            InnerTreenumerator.Current.SiblingIndex,
            InnerTreenumerator.Current.Depth);
        
        Branch.Add(step);
      }
    }
  }
}
