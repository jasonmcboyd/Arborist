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
        () => new StructuralMergeBreadthFirstTreenumerator<TLeft, TRight>(
          leftTreenumerable.GetBreadthFirstTreenumerator,
          rightTreenumerable.GetBreadthFirstTreenumerator),
        () => new StructuralMergeDepthFirstTreenumerator<TLeft, TRight>(
          leftTreenumerable.GetDepthFirstTreenumerator,
          rightTreenumerable.GetDepthFirstTreenumerator));
  }
}
