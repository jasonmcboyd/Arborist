using Arborist.Core;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Linq
{
  internal static class TreeInverter
  {
    public static IEnumerable<SimpleNode<TNode>> Invert<TNode>(ITreenumerable<TNode> source)
    {
      return
        LeaffixAggregator
        .Aggregate(
          source,
          nodeContext => new SimpleNode<TNode>(nodeContext.Node),
          (nodeContext, children) => new SimpleNode<TNode>(nodeContext.Node, children.Reverse()))
        .Reverse();
    }
  }
}
