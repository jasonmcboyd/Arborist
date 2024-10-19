using Arborist.Treenumerables;

namespace Arborist.Trees
{
  public class CompleteBinaryTree : Treenumerable<int, int, CompleteBinaryTreeNodeChildEnumerator>
  {
    public CompleteBinaryTree()
      : base(
          node => new CompleteBinaryTreeNodeChildEnumerator(node),
          MoveNextChild,
          DisposeChildEnumeratorDelegate,
          node => node,
          _Roots)
    {
    }

    private static bool MoveNextChild(
      ref CompleteBinaryTreeNodeChildEnumerator childEnumerator,
      out NodeAndSiblingIndex<int> childNodeAndSiblingIndex)
      => childEnumerator.TryMoveNext(out childNodeAndSiblingIndex);

    private static void DisposeChildEnumeratorDelegate(
      ref CompleteBinaryTreeNodeChildEnumerator childEnumerator)
    {
    }

    private static IEnumerable<int> _Roots =
      new int[] { 0 };
  }
}
