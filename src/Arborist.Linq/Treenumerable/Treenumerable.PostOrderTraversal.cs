using Arborist.Core;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<T> PostOrderTraversal<T>(this ITreenumerable<T> source)
    {
      if (source == null)
        yield break;

      NodeVisit<T>? previousVisit = null;

      foreach (var visit in source.GetDepthFirstTraversal())
      {
        if (visit.VisitCount == 1)
          continue;

        var canYield =
          previousVisit != null
          && (visit.OriginalPosition.Depth < previousVisit.Value.OriginalPosition.Depth
            || (previousVisit.Value.OriginalPosition.Depth == 0
              && visit.OriginalPosition.Depth == 0));

        if (canYield)
          yield return previousVisit.Value.Node;

        previousVisit = visit;
      }

      if (previousVisit != null)
        yield return previousVisit.Value.Node;
    }
  }
}
