namespace Arborist.Linq.Extensions
{
  public static class NodeVisitExtensions
  {
    public static NodeVisit<TNode> IncrementVisitCount<TNode>(this NodeVisit<TNode> visit)
    {
      return
        new NodeVisit<TNode>(
          visit.TreenumeratorState,
          visit.Node,
          visit.VisitCount + 1,
          visit.OriginalPosition,
          visit.Position,
          visit.SchedulingStrategy);
    }
  }
}
