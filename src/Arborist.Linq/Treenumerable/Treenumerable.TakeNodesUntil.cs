using Arborist.Core;
using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TNode> TakeNodesUntil<TNode>(
      this ITreenumerable<TNode> source,
      Func<NodeContext<TNode>, bool> predicate,
      bool keepFinalNode)
      => TreenumerableFactory.Create(
        () => new TakeNodesUntilTreenumerator<TNode>(
          source.GetBreadthFirstTreenumerator,
          predicate,
          keepFinalNode),
        () => new TakeNodesUntilTreenumerator<TNode>(
          source.GetDepthFirstTreenumerator,
          predicate,
          keepFinalNode));
  }
}
