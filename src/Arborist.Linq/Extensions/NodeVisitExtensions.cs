using Arborist.Core;

namespace Arborist.Linq.Extensions
{
  public static class NodeVisitExtensions
  {
    public static NodeVisit<TNode> IncrementVisitCount<TNode>(this NodeVisit<TNode> visit)
    {
      return
        new NodeVisit<TNode>(
          visit.Mode,
          visit.Node,
          visit.VisitCount + 1,
          visit.Position);
    }

    public static NodeVisit<TResult> WithNode<TSource, TResult>(
      this NodeVisit<TSource> visit,
      TResult node)
    {
      return
        new NodeVisit<TResult>(
          visit.Mode,
          node,
          visit.VisitCount,
          visit.Position);
    }

    public static NodeVisit<TNode> WithSiblingIndex<TNode>(
      this NodeVisit<TNode> visit,
      int siblingIndex)
    {
      return
        new NodeVisit<TNode>(
          visit.Mode,
          visit.Node,
          visit.VisitCount,
          (siblingIndex, visit.Position.Depth));
    }

    public static NodeVisit<TNode> WithDepth<TNode>(
      this NodeVisit<TNode> visit,
      int depth)
    {
      return
        new NodeVisit<TNode>(
          visit.Mode,
          visit.Node,
          visit.VisitCount,
          (visit.Position.SiblingIndex, depth));
    }

    public static NodeVisit<TNode> WithPosition<TNode>(
      this NodeVisit<TNode> visit,
      NodePosition position)
    {
      return
        new NodeVisit<TNode>(
          visit.Mode,
          visit.Node,
          visit.VisitCount,
          position);
    }
  }
}
