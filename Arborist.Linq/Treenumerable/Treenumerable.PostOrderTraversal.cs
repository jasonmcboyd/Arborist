using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<T> PostOrderTraversal<T>(this ITreenumerable<T> source)
    {
      if (source == null)
        yield break;

      NodeVisit<T>? previousStep = null;

      foreach (var step in source.GetDepthFirstTraversal())
      {
        var canYield =
          previousStep != null
          && (step.Depth < previousStep.Value.Depth
            || (previousStep.Value.Depth == 0
              && step.Depth == 0));

        if (canYield)
          yield return previousStep.Value.Node;

        previousStep = step;
      }

      if (previousStep != null)
        yield return previousStep.Value.Node;
    }
  }
}
