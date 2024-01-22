using Arborist.Linq.Treenumerators;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<WithParentNode<TNode>> WithParent<TNode>(this ITreenumerable<TNode> source)
      => TreenumerableFactory.Create(
          source,
          breadthFirstEnumerator => new WithParentBreadthFirstTreenumerator<TNode>(breadthFirstEnumerator),
          depthFirstEnumerator => new WithParentDepthFirstTreenumerator<TNode>(depthFirstEnumerator));
  }
}
