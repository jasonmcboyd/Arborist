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
        (depth, source.OriginalPosition.SiblingIndex),
        source.Position,
        source.SchedulingStrategy);

    public static NodeVisit<TResult> WithNode<TSource, TResult>(
      this NodeVisit<TSource> source,
      TResult node)
      => NodeVisit.Create(
        node,
        source.VisitCount,
        source.OriginalPosition,
        source.Position,
        source.SchedulingStrategy);

    public static NodeVisit<TNode> WithVisitCount<TNode>(
      this NodeVisit<TNode> source,
      int visitCount)
      => NodeVisit.Create(
        source.Node,
        visitCount,
        source.OriginalPosition,
        source.Position,
        source.SchedulingStrategy);

    public static NodeVisit<TNode> IncrementVisitCount<TNode>(
      this NodeVisit<TNode> source)
      => NodeVisit.Create(
        source.Node,
        source.VisitCount + 1,
        source.OriginalPosition,
        source.Position,
        source.SchedulingStrategy);

    public static NodeVisit<TNode> WithSiblingIndex<TNode>(
      this NodeVisit<TNode> source,
      int siblingIndex)
      => NodeVisit.Create(
        source.Node,
        source.VisitCount,
        (source.OriginalPosition.Depth, siblingIndex),
        source.Position,
        source.SchedulingStrategy);

    public static NodeVisit<TNode> WithSchedulingStrategy<TNode>(
      this NodeVisit<TNode> source,
      SchedulingStrategy schedulingStrategy)
      => NodeVisit.Create(
        source.Node,
        source.VisitCount,
        source.OriginalPosition,
        source.Position,
        schedulingStrategy);

    public static NodeVisit<TNode> SkipNode<TNode>(
      this NodeVisit<TNode> source)
      => NodeVisit.Create(
        source.Node,
        source.VisitCount,
        source.OriginalPosition,
        source.Position,
        SchedulingStrategy.SkipNode);

    public static NodeVisit<TNode> SkipDescendantSubtrees<TNode>(
      this NodeVisit<TNode> source)
      => NodeVisit.Create(
        source.Node,
        source.VisitCount,
        source.OriginalPosition,
        source.Position,
        SchedulingStrategy.SkipDescendantSubtrees);
  }
}
