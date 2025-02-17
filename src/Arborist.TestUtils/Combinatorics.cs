using System;
using System.Collections.Generic;

namespace Arborist.TestUtils
{
  public static class Combinatorics
  {
    public static List<List<T>> GetCombinationsUpToCount<T>(this ReadOnlySpan<T> span, int count)
    {
      var result = new List<List<T>>();

      count = Math.Min(count, span.Length);

      for (int i = 0; i <= count; i++)
        result.AddRange(GetCombinationsOfCount(span, i));

      return result;
    }

    public static List<List<T>> GetCombinationsOfCount<T>(this ReadOnlySpan<T> span, int count)
    {
      var result = GetReversedCombinations(span, count);

      foreach (var list in result)
        list.Reverse();

      return result;
    }

    private static List<List<T>> GetReversedCombinations<T>(ReadOnlySpan<T> span, int count)
    {
      count = Math.Min(count, span.Length);

      var result = new List<List<T>>();

      if (count < 1)
      {
        result.Add(new List<T>());
        return result;
      }

      if (count == 1)
      {
        for (int i = 0; i < span.Length; i++)
          result.Add(new List<T> { span[i] });

        return result;
      }

      var innerCount = count - 1;

      for (int i = 0; i < span.Length - innerCount; i++)
      {
        foreach (var list in GetReversedCombinations(span.Slice(i + 1), innerCount))
        {
          list.Add(span[i]);
          result.Add(list);
        }
      }

      return result;
    }
  }
}
