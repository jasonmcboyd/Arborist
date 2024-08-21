using Arborist.Core;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TLeft> Subtract<TLeft, TRight>(
      this ITreenumerable<TLeft> leftTreenumerable,
      ITreenumerable<TRight> rightTreenumerable)
    {
      return
        leftTreenumerable
        .Union(rightTreenumerable)
        .Where(mergeNodeContext => !mergeNodeContext.Node.HasRight)
        .Select(mergeNodeContext => mergeNodeContext.Node.Left);
    }
  }
}
