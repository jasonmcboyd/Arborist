using System.Collections.Generic;
using System.Linq;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<T[]> GetBranches<T>(this ITreenumerable<T> source)
    {
      var branch = new List<NodeVisit<T>>();

      foreach (var step in source.GetDepthFirstTraversal())
      {
        if (branch.Count == 0)
        {
          branch.Add(step);
          continue;
        }

        var depthComparison = step.Depth.CompareTo(branch.Last().Depth);

        if (depthComparison < 0)
          branch.RemoveAt(branch.Count - 1);
        else if (depthComparison == 0)
          yield return branch.Select(branchStep => branchStep.Node).ToArray();
        else
          branch.Add(step);
      }
    }
  }
}
