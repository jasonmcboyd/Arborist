using Copse.Core;
using Copse.Linq.TreeEnumerable.BreadthFirstTree;

namespace Copse.Linq
{
  public static partial class Treenumerable
  {
    public static IBreadthFirstTreeEnumerable<TNode> ToBreadthFirstTreeEnumerable<TNode>(
      this ITreenumerable<TNode> source)
    {
      return new BreadthFirstTreeEnumerable<TNode>(source);
    }
  }
}
