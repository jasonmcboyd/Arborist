using System.Collections.Generic;

namespace Copse.Linq.TreeEnumerable.DepthFirstTree
{
  public interface IDepthFirstTreeEnumerable<TNode> : IEnumerable<DepthFirstTreeEnumerableToken<TNode>>
  {
  }
}
