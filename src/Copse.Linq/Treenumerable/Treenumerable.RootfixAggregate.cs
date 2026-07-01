using Copse.Core;
using System;
using System.Collections.Generic;

namespace Copse.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<TAccumulate> RootfixAggregate<TNode, TAccumulate>(
      this ITreenumerable<TNode> source,
      Func<NodeContext<TAccumulate>, NodeContext<TNode>, TAccumulate> accumulator,
      TAccumulate seed)
    {
      return
        source
        .RootfixScan(accumulator, seed)
        .GetLeaves();
    }
  }
}
