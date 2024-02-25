using Arborist.Core;
using System;
using System.Linq;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<T> TakeLastTrees<T>(
      this ITreenumerable<T> source,
      int count)
    {
      var treeCount = source.GetRoots().Count();

      var skipCount = Math.Max(treeCount - count, 0);

      return source.SkipTrees(skipCount);
    }
  }
}
