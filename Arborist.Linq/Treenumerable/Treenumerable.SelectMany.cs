using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TResult> SelectMany<TSource, TResult>(
      this ITreenumerable<ITreenumerable<TSource>> source,
      Func<NodeVisit<TSource>, TResult> selector)
      => TreenumerableFactory.Create(
        source,
        breadthFirstTreenumerator => throw new NotSupportedException(),
        depthFirstTreenumerator => new SelectManyDepthFirstTreenumerator<TSource, TResult>(depthFirstTreenumerator, selector));

    public static ITreenumerable<TNode> SelectMany<TNode>(
      this ITreenumerable<ITreenumerable<TNode>> source)
      => source.SelectMany(visit => visit.Node);
  }
}
