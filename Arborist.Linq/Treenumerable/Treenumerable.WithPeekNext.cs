using Arborist.Linq.Treenumerators;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<WithPeekNextVisit<TNode>> WithPeekNext<TNode>(this ITreenumerable<TNode> source)
      => TreenumerableFactory.Create(
          source,
          breadthFirstEnumerator => new WithPeekNextTreenumerator<TNode>(breadthFirstEnumerator),
          depthFirstEnumerator => new WithPeekNextTreenumerator<TNode>(depthFirstEnumerator));
  }
}
