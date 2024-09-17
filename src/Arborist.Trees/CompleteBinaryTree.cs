using Arborist.Treenumerables;

namespace Arborist.Trees
{
  public class CompleteBinaryTree : IndexableTreenumerable<ulong, CompleteBinaryTreeNode>
  {
    public CompleteBinaryTree() : base(_Roots)
    {
    }

    private static IEnumerable<CompleteBinaryTreeNode> _Roots =
      new CompleteBinaryTreeNode[] { new CompleteBinaryTreeNode() };
  }
}
