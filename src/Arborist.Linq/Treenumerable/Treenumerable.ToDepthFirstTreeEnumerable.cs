using Arborist.Core;
using Arborist.Linq.TreeEnumerable.DepthFirstTree;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IDepthFirstTreeEnumerable<TNode> ToDepthFirstTreeEnumerable<TNode>(
      this ITreenumerable<TNode> source)
    {
      return new DepthFirstTreeEnumerable<TNode>(source);
    }
  }
}
