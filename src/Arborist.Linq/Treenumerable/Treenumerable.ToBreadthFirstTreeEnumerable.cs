using Arborist.Core;
using Arborist.Linq.TreeEnumerable.BreadthFirstTree;

namespace Arborist.Linq
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
