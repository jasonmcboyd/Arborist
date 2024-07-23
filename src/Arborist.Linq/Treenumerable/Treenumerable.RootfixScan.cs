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
        source,
        breadthFirstTreenumerator => new RootfixScanBreadthFirstTreenumerator<TNode, TAccumulate>(breadthFirstTreenumerator, accumulator, seed),
        depthFirstTreenumerator => new RootfixScanDepthFirstTreenumerator<TNode, TAccumulate>(depthFirstTreenumerator, accumulator, seed));
  }
}
