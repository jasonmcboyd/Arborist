using Arborist.Core;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<NodeVisit<TNode>> GetBreadthFirstTraversal<TNode>(
      this ITreenumerable<TNode> source)
    {
      using (var treenumerator = source.GetBreadthFirstTreenumerator())
        while (treenumerator.MoveNext(SchedulingStrategy.TraverseSubtree))
          yield return treenumerator.ToNodeVisit();
    }
  }
}
