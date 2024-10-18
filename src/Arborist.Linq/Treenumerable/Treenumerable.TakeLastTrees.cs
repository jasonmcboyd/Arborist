using Arborist.Core;
using System;
using System.Linq;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TNode> TakeLastTrees<TNode>(
      this ITreenumerable<TNode> source,
      int count)
    {
      var treeCount = source.GetRoots().Count();

      var skipCount = Math.Max(treeCount - count, 0);

      return source.SkipTrees(skipCount);
    }
  }
}
