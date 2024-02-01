namespace Arborist
{
  public static partial class NodeVisit
  {
    public static NodeVisit<TNode> Create<TNode>(
      TNode node,
      int visitCount,
      int siblingIndex,
      int depth,
      bool skipped)
      => new NodeVisit<TNode>(node, visitCount, siblingIndex, depth, skipped);
  }
}
