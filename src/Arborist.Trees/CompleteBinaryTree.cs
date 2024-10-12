using Arborist.Common;
using Arborist.Treenumerables;

namespace Arborist.Trees
{
  public class CompleteBinaryTree : Treenumerable<ulong, CompleteBinaryTreeNode, CompleteBinaryTreeNodeChildEnumerator>
  {
    public CompleteBinaryTree()
      : base(
          node => node.GetChildEnumerator(),
          MoveNextChild,
          DisposeChildEnumeratorDelegate,
          node => node.Value,
          _Roots)
    {
    }

    private static bool MoveNextChild(
      ref CompleteBinaryTreeNodeChildEnumerator childEnumerator,
      out NodeAndSiblingIndex<CompleteBinaryTreeNode> childNodeAndSiblingIndex)
      => childEnumerator.TryMoveNext(out childNodeAndSiblingIndex);

    private static void DisposeChildEnumeratorDelegate(
      ref CompleteBinaryTreeNodeChildEnumerator childEnumerator)
    {
    }

    private static IEnumerable<CompleteBinaryTreeNode> _Roots =
      new CompleteBinaryTreeNode[] { new CompleteBinaryTreeNode() };
  }
}
