namespace Arborist.Linq
{
  internal static class TreenumeratorExtensions
  {
    public static NodeVisit<TNode> ToNodeVisit<TNode>(this ITreenumerator<TNode> treenumerator)
    {
      return
        new NodeVisit<TNode>(
          treenumerator.State,
          treenumerator.Node,
          treenumerator.VisitCount,
          treenumerator.OriginalPosition,
          treenumerator.Position,
          treenumerator.SchedulingStrategy);
    }
  }
}
