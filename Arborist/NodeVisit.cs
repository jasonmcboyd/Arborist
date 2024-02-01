namespace Arborist
{
  public readonly struct NodeVisit<TNode>
  {
    public NodeVisit(
      TNode node,
      int visitCount,
      int siblingIndex,
      int depth,
      bool skipped)
    {
      Node = node;
      VisitCount = visitCount;
      SiblingIndex = siblingIndex;
      Depth = depth;
      Skipped = skipped;
    }

    public TNode Node { get; }
    public int VisitCount { get; }
    public int SiblingIndex { get; }
    public int Depth { get; }
    public bool Skipped { get; }
  }
}
