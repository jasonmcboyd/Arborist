using System.Collections.Generic;

namespace Arborist.Linq.TreeEnumerable.DepthFirstTree
{
  public interface IDepthFirstTreeEnumerable<TNode> : IEnumerable<DepthFirstTreeEnumerableToken<TNode>>
  {
  }
}
