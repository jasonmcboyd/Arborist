using System;
using System.Linq;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<T> SkipLastTrees<T>(
      this ITreenumerable<T> source,
      int count)
    {
      var treeCount = source.GetRoots().Count();

      var takeCount = Math.Max(treeCount - count, 0);

      return source.TakeTrees(takeCount);
    }
  }
}
