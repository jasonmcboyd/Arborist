using System.Collections.Generic;
using System.Linq;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<T> GetRoots<T>(this ITreenumerable<T> source)
    {
      return
        source
        .GetBreadthFirstTraversal()
        .TakeWhile(step => step.Depth == 0)
        .Select(step => step.Node);
    }
  }
}
