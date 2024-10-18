using Arborist.Core;
using Arborist.Treenumerables;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TAccumulate> LeaffixScan<TSource, TAccumulate>(
      this ITreenumerable<TSource> source,
      Func<NodeContext<TSource>, TAccumulate> leafNodeSelector,
      Func<NodeContext<TSource>, TAccumulate[], TAccumulate> accumulator)
    {
      var rootNodes =
        LeaffixAggregator
        .Aggregate(
          source,
          nodeContext => new SimpleNode<TAccumulate>(leafNodeSelector(nodeContext)),
          (nodeContext, children) =>
          {
            var temp = new TAccumulate[children.Length];

            // TODO: I don't like that I have to copy the array here. I would like to be able to pass the array directly to the accumulator.
            for (int i = 0; i < children.Length; i++)
              temp[i] = children[i].Value;

            return
              new SimpleNode<TAccumulate>(
                accumulator(nodeContext, temp),
                children);
          });

      return new SimpleNodeTreenumerable<TAccumulate>(rootNodes);
    }
  }
}
