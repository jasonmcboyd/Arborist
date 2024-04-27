using System;

namespace Arborist.Core
{
  public readonly struct TreenumeratorState<TNode>
  {
    public TreenumeratorState(
      TreenumeratorMode mode,
      TNode node,
      int visitCount,
      NodePosition position)
    {
      Mode = mode;
      Node = node;
      VisitCount = visitCount;
      Position = position;
    }

    public TreenumeratorMode Mode { get; }
    public TNode Node { get; }
    public int VisitCount { get; }
    public NodePosition Position { get; }

    public override string ToString()
    {
      return $"{Position}  {ModeToChar()}  {VisitCount}  {Node}";
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
