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

      foreach (var visit in source.GetDepthFirstTraversal())
      {
        if (visit.VisitCount == 0)
          continue;

        if (visit.Depth == 0 && visit.VisitCount == 1)
          branch.Clear();

        if (branch.Count == 0)
        {
          branch.Add(visit);
          continue;
        }

        var depthComparison = visit.Depth.CompareTo(branch.Last().Depth);

        if (depthComparison < 0)
          branch.RemoveLast();
        else if (depthComparison == 0)
        {
          var result = branch.Select(branchStep => branchStep.Node).ToArray();

          if (branch.Count == 1)
            branch.RemoveLast();

          yield return result;
        }
        else
          branch.Add(visit);
      }
    }
  }
}
