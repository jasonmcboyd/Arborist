namespace Arborist
{
  public static partial class NodeVisit
  {
    public static NodeVisit<TNode> Create<TNode>(
      TNode node,
      int visitCount,
      NodePosition originalPosition,
      NodePosition position,
      SchedulingStrategy schedulingStrategy)
      => new NodeVisit<TNode>(node, visitCount, originalPosition, position, schedulingStrategy);
  }
}
