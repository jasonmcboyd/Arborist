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
      var branch = new List<NodeVisit<T>>();

      using (var treenumerator = source.GetDepthFirstTreenumerator())
      {
        while (treenumerator.MoveNext(TraversalStrategy.TraverseSubtree))
        {
          if (treenumerator.VisitCount == 0)
            continue;

          if (branch.Count == 0)
          {
            branch.Add(treenumerator.ToNodeVisit());
            continue;
          }

          var depth = treenumerator.OriginalPosition.Depth;

          if (depth > branch.Count - 1)
          {
            branch.Add(treenumerator.ToNodeVisit());
          }
          else if (depth < branch.Count - 1)
          {
            if (branch.Last().VisitCount == 1)
              yield return branch.Select(visit => visit.Node).ToArray();

            branch.RemoveLast();
            branch.RemoveLast();
            branch.Add(treenumerator.ToNodeVisit());
          }
          else
          {
            if (branch.Last().VisitCount == 1)
              yield return branch.Select(visit => visit.Node).ToArray();

            branch.Clear();
            branch.Add(treenumerator.ToNodeVisit());
          }
        }

        if (branch.Last().VisitCount == 1)
          yield return branch.Select(visit => visit.Node).ToArray();
      }
    }
  }
}
