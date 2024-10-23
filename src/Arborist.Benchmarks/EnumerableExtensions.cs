using System.Collections.Generic;

namespace Arborist.Benchmarks
{
  internal static class EnumerableExtensions
  {
    public static IEnumerable<int> Geometric(int initialValue = 1, int ratio = 2)
    {
      yield return initialValue;

      while (true)
      {
        try
        {
          initialValue = checked(initialValue * ratio);
        }
        catch
        {
          yield break;
        }

        yield return initialValue;
      }
    }

    public static void Consume<TNode>(this IEnumerable<TNode> source)
    {
      using (var enumerator = source.GetEnumerator())
        while (enumerator.MoveNext());
    }
  }
}
