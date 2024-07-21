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
      Func<NodeAndPosition<TAccumulate>, NodeAndPosition<TSource>, TAccumulate> seedAccumulator,
      Func<NodeAndPosition<TAccumulate>, NodeAndPosition<TSource>, TAccumulate> initialAccumulator,
      Func<NodeAndPosition<TAccumulate>, NodeAndPosition<TAccumulate>, TAccumulate> accumulator,
      Func<NodeAndPosition<TSource>, TAccumulate> seedGenerator)
    {
      var rootNodes =
        source
        .Select(nodeVisit => nodeVisit.ToNodeAndPosition())
        .ToPreorderTreeEnumerable()
        .ToLeaffixScanTreeRoots(
          seedAccumulator,
          initialAccumulator,
          accumulator,
          seedGenerator);

      return new IndexableTreenumerable<TAccumulate>(rootNodes);
    }

    public static ITreenumerable<TAccumulate> LeaffixScan<TSource, TAccumulate>(
      this ITreenumerable<TSource> source,
      Func<NodeAndPosition<TAccumulate>, NodeAndPosition<TSource>, TAccumulate> initialAccumulator,
      Func<NodeAndPosition<TAccumulate>, NodeAndPosition<TAccumulate>, TAccumulate> accumulator,
      Func<NodeAndPosition<TSource>, TAccumulate> seedGenerator)
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
      Func<NodeAndPosition<TSource>, NodeAndPosition<TSource>, TSource> accumulator)
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
