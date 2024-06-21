using Arborist.Core;
using System;
using System.Collections;

namespace Arborist.Virtualization
{
  // TODO: I am not sure how much value this provides.
  internal class VirtualNodeVisit<TNode>
  {
    public TreenumeratorMode Mode { get; set; }
    public TNode Node { get; set; }
    public int VisitCount { get; set; }
    public NodePosition Position { get; set; }
    public TraversalStrategy TraversalStrategy { get; set; }

    public override string ToString()
    {
      var node =
        (Node is IEnumerator enumerator)
        ? enumerator.Current.ToString()
        : Node.ToString();

      return $"{Position}  {ModeToChar()}  {VisitCount}  {node}";
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
