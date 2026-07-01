using Copse.Core;

namespace Copse.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TNode> Empty<TNode>()
      => EmptyTreenumerable<TNode>.Instance;
  }
}
