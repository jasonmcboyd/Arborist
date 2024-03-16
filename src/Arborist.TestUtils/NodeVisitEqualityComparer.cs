using Arborist.Core;
using System.Collections.Generic;

namespace Arborist.TestUtils
{
  public class NodeVisitEqualityComparer<T> : IEqualityComparer<NodeVisit<T>>
  {
    public bool Equals(NodeVisit<T> left, NodeVisit<T> right)
    {
      return
        left.Mode == right.Mode
        && left.SchedulingStrategy == right.SchedulingStrategy
        && left.OriginalPosition == right.OriginalPosition
        && left.Position == right.Position
        && left.VisitCount == right.VisitCount
        && left.Node.Equals(right.Node);
    }

    public int GetHashCode(NodeVisit<T> obj)
      => (obj.Mode, obj.SchedulingStrategy, obj.OriginalPosition, obj.Position, obj.VisitCount, obj.Node).GetHashCode();
  }
}
