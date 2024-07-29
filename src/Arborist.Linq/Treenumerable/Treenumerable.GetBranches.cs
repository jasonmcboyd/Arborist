using Arborist.Core;
using Arborist.Linq.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<T[]> GetBranches<T>(this ITreenumerable<T> source)
    {
      var branch = new List<NodeContext<T>>();

      using (var treenumerator = source.GetDepthFirstTreenumerator())
      {
        if (!treenumerator.MoveNext(NodeTraversalStrategy.TraverseSubtree))
          yield break;

        branch.Add(treenumerator.ToNodeContext());

        while (treenumerator.MoveNext(NodeTraversalStrategy.TraverseSubtree))
        {
          if (treenumerator.Mode != TreenumeratorMode.SchedulingNode)
            continue;

          var depth = treenumerator.Position.Depth;

          if (depth > branch.Count - 1)
          {
            branch.Add(treenumerator.ToNodeContext());
          }
          else
          {
            yield return branch.Select(nodeContext => nodeContext.Node).ToArray();

            branch.RemoveRange(depth, branch.Count - depth);
            branch.Add(treenumerator.ToNodeContext());
          }
        }

        if (branch.Count > 0)
          yield return branch.Select(nodeContext => nodeContext.Node).ToArray();
      }
    }
  }
}
