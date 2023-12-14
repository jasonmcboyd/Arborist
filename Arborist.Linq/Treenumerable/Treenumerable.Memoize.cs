using Arborist.Linq.Treenumerables;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<T> Memoize<T>(
      this ITreenumerable<T> source)
      => new MemoizeTreenumerable<T>(source);
  }
}
