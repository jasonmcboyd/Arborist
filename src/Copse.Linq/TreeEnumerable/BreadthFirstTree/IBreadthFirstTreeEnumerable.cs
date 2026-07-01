using System.Collections.Generic;

namespace Copse.Linq.TreeEnumerable.BreadthFirstTree
{
  public interface IBreadthFirstTreeEnumerable<TNode> : IEnumerable<BreadthFirstTreeEnumerableToken<TNode>>
  {
  }
}
