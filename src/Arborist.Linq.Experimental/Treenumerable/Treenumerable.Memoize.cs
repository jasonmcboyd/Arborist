using Arborist.Core;
using Arborist.Linq.Experimental.Treenumerables;

namespace Arborist.Linq.Experimental
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<T> Memoize<T>(
      this ITreenumerable<T> source)
      => new MemoizeTreenumerable<T>(source);
  }
}
