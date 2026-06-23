using Arborist.Core;
using Arborist.Treenumerables;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    // Mirror: reverse the order of every node's children (and the roots). Materializes the source
    // into flat pre-order arrays, then emits the mirror with a stack -- pushing roots/children in
    // forward order so they pop in reverse. Subtree sizes are invariant under mirroring. O(N);
    // zero per-node allocation (two result arrays + transient capture buffers + an index stack).
    public static ITreenumerable<TNode> Invert<TNode>(this ITreenumerable<TNode> source)
    {
      // 1. Capture the source as flat pre-order arrays (value + subtree size per node).
      var values = new List<TNode>();
      var subtreeSizes = new List<int>();
      var open = new Stack<int>();

      using (var treenumerator = source.GetDepthFirstTreenumerator())
      {
        while (treenumerator.MoveNext(NodeTraversalStrategies.TraverseAll))
        {
          if (treenumerator.Mode != TreenumeratorMode.SchedulingNode)
            continue;

          while (open.Count > treenumerator.Position.Depth)
          {
            var closed = open.Pop();
            subtreeSizes[closed] = values.Count - closed;
          }

          open.Push(values.Count);
          values.Add(treenumerator.Node);
          subtreeSizes.Add(0);
        }
      }

      while (open.Count > 0)
      {
        var closed = open.Pop();
        subtreeSizes[closed] = values.Count - closed;
      }

      // 2. Emit the mirror. Pushing roots/children in forward order makes them pop in reverse, which
      //    is exactly the mirror's pre-order. Each subtree keeps its size; only ordering changes.
      var count = values.Count;
      var mirroredValues = new TNode[count];
      var mirroredSubtreeSizes = new int[count];
      var stack = new Stack<int>();

      for (var root = 0; root < count; root += subtreeSizes[root])
        stack.Push(root);

      var output = 0;
      while (stack.Count > 0)
      {
        var index = stack.Pop();
        mirroredValues[output] = values[index];
        mirroredSubtreeSizes[output] = subtreeSizes[index];
        output++;

        var end = index + subtreeSizes[index];
        for (var child = index + 1; child < end; child += subtreeSizes[child])
          stack.Push(child);
      }

      return new PreorderTree<TNode>(mirroredValues, mirroredSubtreeSizes);
    }
  }
}
