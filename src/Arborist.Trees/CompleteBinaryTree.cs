using Arborist.Treenumerables;

namespace Arborist.Trees
{
  public class CompleteBinaryTree : Treenumerable<ulong, CompleteBinaryTreeNode, CompleteBinaryTreeNodeChildEnumerator>
  {
    public CompleteBinaryTree()
      : base(
          node => node.GetChildEnumerator(),
          ChildEnumeratorMoveNextDelegate,
          DisposeChildEnumeratorDelegate,
          node => node.Value,
          _Roots)
    {
    }

    private static TryMoveNextChildResult<CompleteBinaryTreeNode> ChildEnumeratorMoveNextDelegate(
      ref CompleteBinaryTreeNodeChildEnumerator childEnumerator)
      => childEnumerator.TryMoveNext();

    private static void DisposeChildEnumeratorDelegate(
      ref CompleteBinaryTreeNodeChildEnumerator childEnumerator)
    {
    }

    private static IEnumerable<CompleteBinaryTreeNode> _Roots =
      new CompleteBinaryTreeNode[] { new CompleteBinaryTreeNode() };
  }
}
