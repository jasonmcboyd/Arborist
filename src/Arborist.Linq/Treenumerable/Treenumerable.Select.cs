using Arborist.Core;
using Arborist.Linq.Treenumerables;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TResult> Select<TSource, TResult>(
      this ITreenumerable<TSource> source,
      Func<NodeContext<TSource>, TResult> selector)
    {
      if (source is ISelectTreenumerable<TSource> innerSelectTreenumerable)
        return innerSelectTreenumerable.Compose(selector);
      else
        return new SelectTreenumerable<TSource, TResult>(source, selector);
    }
  }
}
