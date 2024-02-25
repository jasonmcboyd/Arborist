using Arborist.Core;

namespace Arborist.Linq
{
  public readonly struct NodeVisit<TNode>
  {
    public NodeVisit(
      TreenumeratorState treenumeratorState,
      TNode node,
      int visitCount,
      NodePosition originalPosition,
      NodePosition position,
      SchedulingStrategy schedulingStrategy)
    {
      TreenumeratorState = treenumeratorState;
      Node = node;
      VisitCount = visitCount;
      OriginalPosition = originalPosition;
      Position = position;
      SchedulingStrategy = schedulingStrategy;
    }

    public TreenumeratorState TreenumeratorState { get; }
    public TNode Node { get; }
    public int VisitCount { get; }
    public NodePosition OriginalPosition { get; }
    public NodePosition Position { get; }
    public SchedulingStrategy SchedulingStrategy { get; }
  }
}
