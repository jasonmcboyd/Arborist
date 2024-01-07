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
        while (treenumerator.MoveNext(ChildStrategy.ScheduleForTraversal))
          if (treenumerator.Current.VisitCount == 1)
            yield return treenumerator.Current.Node;
    }
  }
}
