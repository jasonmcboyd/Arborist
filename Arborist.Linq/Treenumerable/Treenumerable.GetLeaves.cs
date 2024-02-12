using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<T> GetLeaves<T>(this ITreenumerable<T> source)
    {
      using (var enumerator = source.GetDepthFirstTreenumerator())
      {
        if (!enumerator.MoveNext(SchedulingStrategy.ScheduleForTraversal))
          yield break;

        var previousStep = enumerator.Current;

        while (enumerator.MoveNext(SchedulingStrategy.ScheduleForTraversal))
        {
          var step = enumerator.Current;

          if (step.OriginalPosition.Depth != previousStep.OriginalPosition.Depth)
          {
            previousStep = step;
            continue;
          }

          if (step.VisitCount != 2)
          {
            previousStep = step;
            continue;
          }

          yield return step.Node;
        }
      }
    }
  }
}
