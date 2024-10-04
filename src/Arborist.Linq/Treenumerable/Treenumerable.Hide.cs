using Arborist.Core;

namespace Arborist.Linq
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
