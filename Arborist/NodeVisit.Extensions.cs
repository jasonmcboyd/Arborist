namespace Arborist
{
  public static partial class NodeVisit
  {
    public static NodeVisit<TNode> WithDepth<TNode>(
      this NodeVisit<TNode> source,
      int depth)
      => NodeVisit.Create(
        source.Node,
        source.VisitCount,
        source.SiblingIndex,
        depth,
        source.Skipped);

    public static NodeVisit<TResult> WithNode<TSource, TResult>(
      this NodeVisit<TSource> source,
      TResult node)
      => NodeVisit.Create(
        node,
        source.VisitCount,
        source.SiblingIndex,
        source.Depth,
        source.Skipped);

    public static NodeVisit<TNode> WithVisitCount<TNode>(
      this NodeVisit<TNode> source,
      int visitCount)
      => NodeVisit.Create(
        source.Node,
        visitCount,
        source.SiblingIndex,
        source.Depth,
        source.Skipped);

    public static NodeVisit<TNode> IncrementVisitCount<TNode>(
      this NodeVisit<TNode> source)
      => NodeVisit.Create(
        source.Node,
        source.VisitCount + 1,
        source.SiblingIndex,
        source.Depth,
        source.Skipped);

    public static NodeVisit<TNode> WithSiblingIndex<TNode>(
      this NodeVisit<TNode> source,
      int siblingIndex)
      => NodeVisit.Create(
        source.Node,
        source.VisitCount,
        siblingIndex,
        source.Depth,
        source.Skipped);

    public static NodeVisit<TNode> Skip<TNode>(
      this NodeVisit<TNode> source)
      => NodeVisit.Create(
        source.Node,
        source.VisitCount,
        source.SiblingIndex,
        source.Depth,
        true);
  }
}
