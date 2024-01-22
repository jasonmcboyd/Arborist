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
        if (visit.VisitCount == 1)
          continue;

        if (branch.Count == 0)
        {
          branch.Add(visit);
          continue;
        }

        var depthComparison = visit.Depth.CompareTo(branch.Last().Depth);

        if (depthComparison < 0)
          branch.RemoveAt(branch.Count - 1);
        else if (depthComparison == 0)
          yield return branch.Select(branchStep => branchStep.Node).ToArray();
        else
          branch.Add(visit);
      }
    }
  }
}
