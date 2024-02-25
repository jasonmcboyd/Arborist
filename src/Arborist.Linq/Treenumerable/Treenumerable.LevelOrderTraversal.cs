using Arborist.Core;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<T> LevelOrderTraversal<T>(this ITreenumerable<T> source)
    {
      if (source == null)
        return Enumerable.Empty<T>();

      return
        source
        .GetBreadthFirstTraversal()
        .Where(visit => visit.VisitCount == 1)
        .Select(visit => visit.Node);
    }
  }
}
