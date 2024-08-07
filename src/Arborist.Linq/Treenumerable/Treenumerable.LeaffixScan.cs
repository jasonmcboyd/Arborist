using Arborist.Core;
using Arborist.Linq.Extensions;
using Arborist.Treenumerables;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TAccumulate> LeaffixScan<TSource, TAccumulate>(
      this ITreenumerable<TSource> source,
      Func<NodeContext<TAccumulate>, NodeContext<TSource>, TAccumulate> seedAccumulator,
      Func<NodeContext<TAccumulate>, NodeContext<TSource>, TAccumulate> initialAccumulator,
      Func<NodeContext<TAccumulate>, NodeContext<TAccumulate>, TAccumulate> accumulator,
      Func<NodeContext<TSource>, TAccumulate> seedGenerator)
    {
      //var rootNodes =
      //  source
      //  .Materialize()
      //  .ToPreorderTreeEnumerable()
      //  .ToLeaffixScanTreeRoots(
      //    seedAccumulator,
      //    initialAccumulator,
      //    accumulator,
      //    seedGenerator);

      //return new IndexableTreenumerable<TAccumulate>(rootNodes);
      throw new NotImplementedException();
    }

    public static ITreenumerable<TAccumulate> LeaffixScan<TSource, TAccumulate>(
      this ITreenumerable<TSource> source,
      Func<NodeContext<TAccumulate>, NodeContext<TSource>, TAccumulate> initialAccumulator,
      Func<NodeContext<TAccumulate>, NodeContext<TAccumulate>, TAccumulate> accumulator,
      Func<NodeContext<TSource>, TAccumulate> seedGenerator)
    {
      return
        source
        .LeaffixScan(
          initialAccumulator,
          initialAccumulator,
          accumulator,
          seedGenerator);
    }

    public static ITreenumerable<TSource> LeaffixScan<TSource>(
      this ITreenumerable<TSource> source,
      Func<NodeContext<TSource>, NodeContext<TSource>, TSource> accumulator)
    {
      return
        source
        .LeaffixScan(
          (_, leaf) => leaf.Node,
          accumulator,
          accumulator,
          _ => default);
    }
  }
}
