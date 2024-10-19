namespace Arborist.Benchmarks
{
  public static class EnumerableExtensions
  {
    internal static void Consume<TNode>(this IEnumerable<TNode> source)
    {
      using (var enumerator = source.GetEnumerator())
        while (enumerator.MoveNext());
    }
  }
}
