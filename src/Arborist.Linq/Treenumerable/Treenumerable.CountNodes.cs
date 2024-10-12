using Arborist.Common;
using Arborist.Core;
using System;
using System.Linq;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static int CountNodes<TNode>(this ITreenumerable<TNode> source)
      => source.CountNodes(_ => true);

    public static int CountNodes<TNode>(
      this ITreenumerable<TNode> source,
      Func<NodeContext<TNode>, bool> predicate)
    {
      if (source == null)
        return 0;

      return source.Materialize().PreOrderTraversal().Count(predicate);
    }
  }
}
