namespace Arborist
{
  public readonly struct NodeVisit<TNode>
  {
    public NodeVisit(
      TNode node,
      int visitCount,
      NodePosition originalPosition,
      NodePosition position,
      SchedulingStrategy schedulingStrategy)
    {
      Node = node;
      VisitCount = visitCount;
      OriginalPosition = originalPosition;
      Position = position;
      SchedulingStrategy = schedulingStrategy;
    }

    public TNode Node { get; }
    public int VisitCount { get; }
    public NodePosition OriginalPosition { get; }
    public NodePosition Position { get; }
    public SchedulingStrategy SchedulingStrategy { get; }
    public bool Skipped => SchedulingStrategy == SchedulingStrategy.SkipNode;
    public bool SkippedDescendants => SchedulingStrategy == SchedulingStrategy.SkipDescendantSubtrees;
  }
}
