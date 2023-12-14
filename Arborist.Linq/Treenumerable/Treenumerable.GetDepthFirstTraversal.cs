using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<NodeVisit<T>> GetDepthFirstTraversal<T>(this ITreenumerable<T> source)
    {
      using (var enumerator = source.GetDepthFirstTreenumerator())
        while (enumerator.MoveNext(false))
          yield return enumerator.Current;
    }
  }
}
