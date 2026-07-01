using Copse.Core;

namespace Copse.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TNode> Hide<TNode>(
      this ITreenumerable<TNode> source)
    {
      return new HideTreenumerable<TNode>(source);
    }
  }
}
