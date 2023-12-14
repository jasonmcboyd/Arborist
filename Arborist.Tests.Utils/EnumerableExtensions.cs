using System;
using System.Collections.Generic;

namespace Arborist.Tests.Utils
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
  }
}
