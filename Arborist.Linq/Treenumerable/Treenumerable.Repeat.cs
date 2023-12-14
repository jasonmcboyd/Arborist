using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TNode> Repeat<TNode>(this ITreenumerable<TNode> source)
      => TreenumerableFactory.Create(
          source,
          breadthFirstEnumerator => throw new NotImplementedException(),
          depthFirstEnumerator => new RepeatDepthFirstTreenumerator<TNode>(source));

    public static ITreenumerable<TNode> Repeat<TNode>(
      this ITreenumerable<TNode> source,
      int count)
      => TreenumerableFactory.Create(
          source,
          breadthFirstEnumerator => throw new NotImplementedException(),
          depthFirstEnumerator => new RepeatDepthFirstTreenumerator<TNode>(source, count));
  }
}
