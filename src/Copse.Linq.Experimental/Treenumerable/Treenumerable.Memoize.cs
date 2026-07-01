using Copse.Core;
using Copse.Linq.Experimental.Treenumerables;

namespace Copse.Linq.Experimental
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<T> Memoize<T>(
      this ITreenumerable<T> source)
      => new MemoizeTreenumerable<T>(source);
  }
}
