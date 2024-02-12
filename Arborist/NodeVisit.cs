namespace Arborist
{
  public readonly struct NodeVisit<TNode>
  {
    public NodeVisit(
      TNode node,
      int visitCount,
      NodePosition originalPosition,
      NodePosition position,
      bool skipped)
    {
      Node = node;
      VisitCount = visitCount;
      OriginalPosition = originalPosition;
      Position = position;
      Skipped = skipped;
    }

    public TNode Node { get; }
    public int VisitCount { get; }
    public NodePosition OriginalPosition { get; }
    public NodePosition Position { get; }
    public bool Skipped { get; }
  }
}
