using Arborist.Core;
using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TNode> Where<TNode>(
      this ITreenumerable<TNode> source,
      Func<NodeContext<TNode>, bool> predicate)
      => TreenumerableFactory.Create(
        source,
        breadthFirstTreenumerator => new WhereBreadthFirstTreenumerator<TNode>(breadthFirstTreenumerator, nodeContext => !predicate(nodeContext), NodeTraversalStrategy.SkipNode),
        depthFirstTreenumerator => new WhereDepthFirstTreenumerator<TNode>(depthFirstTreenumerator, predicate));
  }
}
