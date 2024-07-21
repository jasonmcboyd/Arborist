using Arborist.Core;
using Arborist.Linq.Extensions;
using Arborist.Nodes;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TNode> Reverse<TNode>(
      this ITreenumerable<TNode> source)
      => source.ToPreorderTreeEnumerable().ToReverseTreeRoots().ToTreenumerable();
  }
}
