﻿using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<T> GetLeaves<T>(this ITreenumerable<T> source)
    {
      using (var enumerator = source.GetDepthFirstTreenumerator())
      {
        if (!enumerator.MoveNext(ChildStrategy.ScheduleForTraversal))
          yield break;

        var previousStep = enumerator.Current;

        while (enumerator.MoveNext(ChildStrategy.ScheduleForTraversal))
        {
          var step = enumerator.Current;

          if (step.Depth != previousStep.Depth)
          {
            previousStep = step;
            continue;
          }

          if (step.SiblingIndex != previousStep.SiblingIndex)
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
