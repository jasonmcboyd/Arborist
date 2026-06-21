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

      // TODO: The predicates are inverted. One or the other should be changed for consistency.
      return
        TreenumerableFactory.Create(
          () => new WhereBreadthFirstTreenumerator<TNode>(
            source.GetBreadthFirstTreenumerator,
            nodeContext => !predicate(nodeContext),
            NodeTraversalStrategies.SkipNode),
          // SPIKE: DFT path swapped to the clean-room extraction treenumerator to validate it
          // against the full existing DFT test suite (consumer strategies included).
          () => new WhereDepthFirstTreenumerator2<TNode>(
            source.GetDepthFirstTreenumerator,
            predicate));
    }
  }
}
