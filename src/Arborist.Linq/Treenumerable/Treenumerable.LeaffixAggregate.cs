using Arborist.Core;
using System;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<TAccumulate> LeaffixAggregate<TNode, TAccumulate>(
      this ITreenumerable<TNode> source,
      Func<NodeContext<TNode>, TAccumulate> leafSelector,
      Func<NodeContext<TNode>, TAccumulate[], TAccumulate> accumulator)
    {
      return
        LeaffixAggregator
        .Aggregate(
          source,
          leafSelector,
          accumulator);
    }
  }
}
