using Arborist.Common;
using Arborist.Core;
using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TAccumulate> RootfixScan<TNode, TAccumulate>(
      this ITreenumerable<TNode> source,
      Func<NodeContext<TAccumulate>, NodeContext<TNode>, TAccumulate> accumulator,
      TAccumulate seed)
      => TreenumerableFactory.Create(
        () => new RootfixScanBreadthFirstTreenumerator<TNode, TAccumulate>(
          source.GetBreadthFirstTreenumerator,
          accumulator,
          seed),
        () => new RootfixScanDepthFirstTreenumerator<TNode, TAccumulate>(
          source.GetDepthFirstTreenumerator,
          accumulator,
          seed));
  }
}
