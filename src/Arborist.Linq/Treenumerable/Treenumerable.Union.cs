using Arborist.Core;
using Arborist.Linq.Treenumerators;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<MergeNode<TLeft, TRight>> Union<TLeft, TRight>(
      this ITreenumerable<TLeft> leftTreenumerable,
      ITreenumerable<TRight> rightTreenumerable)
      => TreenumerableFactory.Create(
        leftTreenumerable,
        breadthFirstTreenumerator => new StructuralMergeBreadthFirstTreenumerator<TLeft, TRight>(breadthFirstTreenumerator, rightTreenumerable.GetBreadthFirstTreenumerator()),
        depthFirstTreenumerator => new StructuralMergeDepthFirstTreenumerator<TLeft, TRight>(depthFirstTreenumerator, rightTreenumerable.GetDepthFirstTreenumerator()));
  }
}
