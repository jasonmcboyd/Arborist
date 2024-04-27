using Arborist.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.TestUtils
{
  public static class EnumerableExtensions
  {
    public static IEnumerable<T> Do<T>(this IEnumerable<T> source, Action<T> action)
    {
      foreach (var item in source)
      {
        action(item);
        yield return item;
      }
    }

    public static NodeVisit<T>[] ToNodeVisitArray<T>(this IEnumerable<(TreenumeratorMode, T, int, (int, int))> source)
    {
      return
        source
        .Select(nodeVisit =>
          new NodeVisit<T>(
            nodeVisit.Item1,
            nodeVisit.Item2,
            nodeVisit.Item3,
            nodeVisit.Item4))
        .ToArray();
    }
  }
}
