using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<T> GetRoots<T>(this ITreenumerable<T> source)
    {
      using (var treenumerator = source.GetBreadthFirstTreenumerator())
      {
        while(treenumerator.MoveNext(SchedulingStrategy.SkipDescendantSubtrees))
          if (treenumerator.VisitCount == 1)
            yield return treenumerator.Node;
      }
    }
  }
}
