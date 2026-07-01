using Copse.Core;
using Copse.Treenumerables;
using System.Collections.Generic;

namespace Copse.Linq
{
  public static partial class Treenumerable
  {
    // Snapshots the source into a concrete, immutable PreorderTree (flat pre-order arrays).
    // Re-enumerating the result re-runs the engine over the in-memory structure -- cheap, and it
    // still honors dynamic NodeTraversalStrategies. Eager: the full source is consumed up front
    // (with TraverseAll) so the snapshot is complete regardless of how it is later traversed.
    public static ITreenumerable<TValue> Materialize<TValue>(this ITreenumerable<TValue> source)
    {
      var values = new List<TValue>();
      var subtreeSizes = new List<int>();
      var open = new Stack<int>(); // indices of ancestors whose subtree is still being read

      using (var treenumerator = source.GetDepthFirstTreenumerator())
      {
        while (treenumerator.MoveNext(NodeTraversalStrategies.TraverseAll))
        {
          // First visit of each node, in pre-order (the same node selector TreeSerializer uses).
          if (treenumerator.VisitCount != 1)
            continue;

          var depth = treenumerator.Position.Depth;

          // Any still-open nodes at or below this depth are finished subtrees -- close them out.
          while (open.Count > depth)
          {
            var closedIndex = open.Pop();
            subtreeSizes[closedIndex] = values.Count - closedIndex;
          }

          open.Push(values.Count);
          values.Add(treenumerator.Node);
          subtreeSizes.Add(0); // backfilled when this node's subtree closes
        }
      }

      while (open.Count > 0)
      {
        var closedIndex = open.Pop();
        subtreeSizes[closedIndex] = values.Count - closedIndex;
      }

      return new PreorderTree<TValue>(values.ToArray(), subtreeSizes.ToArray());
    }
  }
}
