using Arborist.Core;
using System;

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

    public override string ToString()
    {
      return $"{StateToChar()}  {SchedulingStrategyToChar()}  {Node}  {VisitCount}  {OriginalPosition}  {Position}";
    }

    private char StateToChar()
    {
      switch (TreenumeratorState)
      {
        case TreenumeratorState.EnumerationFinished:
          return 'F';
        case TreenumeratorState.EnumerationNotStarted:
          return 'N';
        case TreenumeratorState.SchedulingNode:
          return 'S';
        case TreenumeratorState.VisitingNode:
          return 'V';
        default:
          throw new NotImplementedException();
      }
    }

    private char SchedulingStrategyToChar()
    {
      if (TreenumeratorState != TreenumeratorState.SchedulingNode)
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
