using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<T> GetRoots<T>(this ITreenumerable<T> source)
    {
      using (var treenumerator = source.GetBreadthFirstTreenumerator())
      {
        while(treenumerator.MoveNext(ChildStrategy.ScheduleForTraversal))
        {
          if (treenumerator.Current.Depth > 0)
            yield break;

          if (treenumerator.Current.VisitCount == 1)
            yield return treenumerator.Current.Node;
        }
      }
    }
  }
}
