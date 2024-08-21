using Arborist.Core;
using Arborist.Linq.Treenumerators;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<MergeNode<TLeft, TRight>> Intersection<TLeft, TRight>(
      this ITreenumerable<TLeft> leftTreenumerable,
      ITreenumerable<TRight> rightTreenumerable)
    {
      return
        leftTreenumerable
        .Union(rightTreenumerable)
        .PruneBefore(mergeNodeContext => !mergeNodeContext.Node.HasLeftAndRight);
    }
  }
}
