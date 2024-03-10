using Arborist.Core;
using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TNode> RootfixScan<TInner, TNode>(
      this ITreenumerable<TInner> source,
      Func<NodeVisit<TNode>, NodeVisit<TInner>, TNode> accumulator,
      TNode seed)
      => TreenumerableFactory.Create(
        source,
        breadthFirstTreenumerator => new RootfixScanDepthFirstTreenumerator<TInner, TNode>(breadthFirstTreenumerator, accumulator, seed),
        depthFirstTreenumerator => new RootfixScanDepthFirstTreenumerator<TInner, TNode>(depthFirstTreenumerator, accumulator, seed));
  }
}
