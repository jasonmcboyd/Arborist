using Arborist.Core;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<T> PreOrderTraversal<T>(this ITreenumerable<T> source)
    {
      if (source == null)
        yield break;

      using (var treenumerator = source.GetDepthFirstTreenumerator())
        while (treenumerator.MoveNext(SchedulingStrategy.TraverseSubtree))
          if (treenumerator.VisitCount == 1)
            yield return treenumerator.Node;
    }
  }
}
