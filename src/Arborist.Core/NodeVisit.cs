using System;

namespace Arborist.Core
{
  public readonly struct NodeVisit<TNode>
  {
    public NodeVisit(
      TreenumeratorMode mode,
      TNode node,
      int visitCount,
      NodePosition originalPosition)
    {
      Mode = mode;
      Node = node;
      VisitCount = visitCount;
      OriginalPosition = originalPosition;
    }

    public TreenumeratorMode Mode { get; }
    public TNode Node { get; }
    public int VisitCount { get; }
    public NodePosition OriginalPosition { get; }

    public override string ToString()
    {
      return $"{OriginalPosition}  {ModeToChar()}  {VisitCount}  {Node}";
    }

    private char ModeToChar()
    {
      switch (Mode)
      {
        case TreenumeratorMode.SchedulingNode:
          return 'S';
        case TreenumeratorMode.VisitingNode:
          return 'V';
        default:
          throw new NotImplementedException();
      }
    }
  }
}
