using System;
using System.Collections.Generic;

namespace Arborist.Linq.Extensions
{
  internal static class ListExtensions
  {
    public static void RemoveLast<T>(this IList<T> list) => list.RemoveAt(list.Count - 1);

    public static void ReplaceLast<T>(this IList<T> list, T item) => list[list.Count - 1] = item;

    public static T PopLast<T>(this IList<T> list)
    {
      if (list.Count == 0)
        throw new InvalidOperationException("This list is empty.");

      var last = list[list.Count - 1];

      list.RemoveAt(list.Count - 1);

      return last;
    }
  }
}
