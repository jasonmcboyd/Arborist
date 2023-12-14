namespace Arborist
{
  public readonly struct NodeVisit<TNode>
  {
    public NodeVisit(
      TNode node,
      int visitCount,
      int siblingIndex,
      int depth)
    {
      Node = node;
      VisitCount = visitCount;
      SiblingIndex = siblingIndex;
      Depth = depth;
    }

    public TNode Node { get; }
    public int VisitCount { get; }
    public int SiblingIndex { get; }
    public int Depth { get; }
  }
}
