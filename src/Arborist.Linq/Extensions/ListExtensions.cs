using System;
using System.Collections.Generic;

namespace Arborist.Linq.Extensions
{
  internal static class ListExtensions
  {
    public static void RemoveLast<T>(this IList<T> list) => list.RemoveAt(list.Count - 1);

    public static T GetAtOrAdd<T>(this IList<T> list, int index) where T : new() =>
      list.GetAtOrAdd(index, () => new T());

    public static T GetAtOrAdd<T>(this IList<T> list, int index, Func<T> factory)
    {
      while (list.Count <= index)
        list.Add(factory());

      return list[index];
    }
  }
}
