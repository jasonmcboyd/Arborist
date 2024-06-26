using Arborist.Nodes;
using Arborist.Treenumerables;

namespace Arborist.Trees
{
  public class CompleteBinaryTree : IndexableTreenumerable<ulong>
  {
    public CompleteBinaryTree() : base(_Roots)
    {
    }

    private static IEnumerable<INodeWithIndexableChildren<ulong>> _Roots =
      new INodeWithIndexableChildren<ulong>[] { new CompleteBinaryTreeNode() };
  }
}
