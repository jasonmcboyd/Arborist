using Arborist.Common;
using Arborist.Core;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<TNode[]> GetLevels<TNode>(this ITreenumerable<TNode> source)
    {
      var depth = 0;
      var deque = new RefSemiDeque<TNode>();

      using (var treenumerator = source.GetBreadthFirstTreenumerator())
      {
        while (treenumerator.MoveNext(NodeTraversalStrategy.TraverseSubtree))
        {
          if (treenumerator.Mode != TreenumeratorMode.SchedulingNode)
            continue;

          if (treenumerator.Position.Depth != depth)
          {
            depth++;
            yield return CopyDequeToArray(deque);
          }

          deque.AddLast(treenumerator.Node);
        }

        if (deque.Count > 0)
          yield return CopyDequeToArray(deque);
      }

      TNode[] CopyDequeToArray(RefSemiDeque<TNode> localDeque)
      {
        var result = new TNode[deque.Count];

        for (int i = deque.Count - 1; i >= 0; i--)
          result[i] = deque.RemoveLast();

        return result;
      }
    }
  }
}
