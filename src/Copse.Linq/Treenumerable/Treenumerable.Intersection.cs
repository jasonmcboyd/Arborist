using Copse.Core;
using Copse.Linq.Treenumerators;

namespace Copse.Linq
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
