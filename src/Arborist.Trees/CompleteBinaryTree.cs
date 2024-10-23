using Arborist.Treenumerables;
using System.Collections.Generic;

namespace Arborist.Trees
{
  public class CompleteBinaryTree : Treenumerable<int, int, CompleteBinaryTreeNodeChildEnumerator>
  {
    public CompleteBinaryTree()
      : base(
          nodeContext => new CompleteBinaryTreeNodeChildEnumerator(nodeContext.Node),
          node => node,
          new int[] { 0 })
    {
    }
  }
}
