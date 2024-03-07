using Arborist.Core;
using Arborist.Linq.Extensions;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<NodeVisit<TNode>> GetDepthFirstTraversal<TNode>(
      this ITreenumerable<TNode> source)
    {
      using (var treenumerator = source.GetDepthFirstTreenumerator())
        while (treenumerator.MoveNext(SchedulingStrategy.TraverseSubtree))
          yield return treenumerator.ToNodeVisit();
    }
  }
}
