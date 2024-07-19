using Arborist.Core;
using Arborist.Linq.Extensions;
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
      Func<NodeAndPosition<TNode>, bool> predicate)
    {
      if (source == null)
        return 0;

      return source.Select(visit => visit.ToNodeAndPosition()).Where(nodeVisit => predicate(nodeVisit.Node)).PreOrderTraversal().Count();
    }
  }
}
