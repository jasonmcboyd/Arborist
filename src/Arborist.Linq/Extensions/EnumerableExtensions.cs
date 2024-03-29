﻿using System;
using System.Collections.Generic;

namespace Arborist.Linq.Extensions
{
  internal static class EnumerableExtensions
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
