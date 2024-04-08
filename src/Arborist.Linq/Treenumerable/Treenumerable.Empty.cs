using Arborist.Core;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TNode> Empty<TNode>()
      => EmptyTreenumerable<TNode>.Instance;
  }
}
