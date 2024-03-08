using Arborist.Core;
using Arborist.Linq.Extensions;
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

        treenumerator.MoveNext(SchedulingStrategy.TraverseSubtree);

        NodeVisit<T> previousVisit = default;
        NodeVisit<T> currentVisit = treenumerator.ToNodeVisit();

        while (treenumerator.MoveNext(SchedulingStrategy.TraverseSubtree))
        {
          if (treenumerator.VisitCount == 0)
            continue;

          previousVisit = currentVisit;
          currentVisit = treenumerator.ToNodeVisit();

          if (previousVisit.VisitCount == 1)
          {
            if (previousVisit.OriginalPosition.Depth > currentVisit.OriginalPosition.Depth
              || previousVisit.OriginalPosition.Depth == currentVisit.OriginalPosition.Depth)
              yield return previousVisit.Node;
          }
        }

        if (currentVisit.VisitCount == 1)
          yield return currentVisit.Node;
      }
    }
  }
}
