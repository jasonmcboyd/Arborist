using Copse.Treenumerables;
using System.Collections.Generic;

namespace Copse.Trees
{
  public class CompleteBinaryTree : Treenumerable<int, CompleteBinaryTreeNodeChildEnumerator>
  {
    public CompleteBinaryTree()
      : base(
          nodeContext => new CompleteBinaryTreeNodeChildEnumerator(nodeContext.Node),
          new int[] { 0 })
    {
    }
  }
}
