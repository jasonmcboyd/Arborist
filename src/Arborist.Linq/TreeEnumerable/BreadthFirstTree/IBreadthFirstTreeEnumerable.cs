using System.Collections.Generic;

namespace Arborist.Linq.TreeEnumerable.BreadthFirstTree
{
  public interface IBreadthFirstTreeEnumerable<TNode> : IEnumerable<BreadthFirstTreeEnumerableToken<TNode>>
  {
  }
}
