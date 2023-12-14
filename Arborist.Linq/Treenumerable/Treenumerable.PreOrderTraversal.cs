using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<T> PreOrderTraversal<T>(this ITreenumerable<T> source)
    {
      if (source == null)
        yield break;

      NodeVisit<T>? previousStep = null;

      foreach (var step in source.GetDepthFirstTraversal())
      {
        var canYield =
          previousStep == null
          || previousStep.Value.Depth < step.Depth
          || (step.Depth == 0 && previousStep.Value.Depth == 0);

        if (canYield)
          yield return step.Node;

        previousStep = step;
      }
    }
  }
}
