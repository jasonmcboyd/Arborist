using System;

namespace Arborist.Core
{
  public readonly struct NodeVisit<TNode>
  {
    public NodeVisit(
      TreenumeratorMode mode,
      TNode node,
      int visitCount,
      NodePosition originalPosition,
      NodePosition position,
      SchedulingStrategy schedulingStrategy)
    {
      Mode = mode;
      Node = node;
      VisitCount = visitCount;
      OriginalPosition = originalPosition;
      Position = position;
      SchedulingStrategy = schedulingStrategy;
    }

    public TreenumeratorMode Mode { get; }
    public TNode Node { get; }
    public int VisitCount { get; }
    public NodePosition OriginalPosition { get; }
    public NodePosition Position { get; }
    public SchedulingStrategy SchedulingStrategy { get; }

    public override string ToString()
    {
      return $"{ModeToChar()}  {SchedulingStrategyToChar()}  {OriginalPosition}  {Position}  {VisitCount}  {Node}";
    }

    private char ModeToChar()
    {
      switch (Mode)
      {
        case TreenumeratorMode.EnumerationFinished:
          return 'F';
        case TreenumeratorMode.EnumerationNotStarted:
          return 'N';
        case TreenumeratorMode.SchedulingNode:
          return 'S';
        case TreenumeratorMode.VisitingNode:
          return 'V';
        default:
          throw new NotImplementedException();
      }
    }

    private char SchedulingStrategyToChar()
    {
      if (Mode != TreenumeratorMode.SchedulingNode)
        return '_';

      switch (SchedulingStrategy)
      {
        case SchedulingStrategy.SkipDescendants:
          return 'D';
        case SchedulingStrategy.SkipNode:
          return 'N';
        case SchedulingStrategy.SkipSubtree:
          return 'S';
        case SchedulingStrategy.TraverseSubtree:
          return 'T';
        default:
          throw new NotImplementedException();
      }
    }
  }
}
