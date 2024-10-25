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
    {
      if (predicate == null)
        return source;

      return
        TreenumerableFactory.Create(
          () => new WhereBreadthFirstTreenumerator<TNode>(
            source.GetBreadthFirstTreenumerator,
            nodeContext => !predicate(nodeContext),
            NodeTraversalStrategies.SkipNode),
          () => new WhereDepthFirstTreenumerator<TNode>(
            source.GetDepthFirstTreenumerator,
            predicate));
    }
  }
}
