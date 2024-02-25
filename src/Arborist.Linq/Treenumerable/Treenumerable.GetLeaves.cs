using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<T> GetLeaves<T>(this ITreenumerable<T> source)
    {
      using (var treenumerator = source.GetDepthFirstTreenumerator())
      {
        if (!treenumerator.MoveNext(SchedulingStrategy.TraverseSubtree))
          yield break;

        var previousVisit = treenumerator.ToNodeVisit();

        while (treenumerator.MoveNext(SchedulingStrategy.TraverseSubtree))
        {
          var currentVisit = treenumerator.ToNodeVisit();

          if (currentVisit.OriginalPosition.Depth != previousVisit.OriginalPosition.Depth)
          {
            previousVisit = currentVisit;
            continue;
          }

          if (currentVisit.VisitCount != 2)
          {
            previousVisit = currentVisit;
            continue;
          }

          yield return currentVisit.Node;
        }
      }
    }
  }
}
