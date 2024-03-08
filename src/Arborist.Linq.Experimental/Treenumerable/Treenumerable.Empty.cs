using Arborist.Core;
using Arborist.Linq.Treenumerables;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TNode> Empty<TNode>()
      => EmptyTreenumerable<TNode>.Instance;
  }
}
