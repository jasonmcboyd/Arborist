using Arborist.Common;
using Arborist.Treenumerables;

namespace Arborist.Trees
{
  public class CompleteBinaryTree : Treenumerable<ulong, ulong, CompleteBinaryTreeNodeChildEnumerator>
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
      out NodeAndSiblingIndex<ulong> childNodeAndSiblingIndex)
      => childEnumerator.TryMoveNext(out childNodeAndSiblingIndex);

    private static void DisposeChildEnumeratorDelegate(
      ref CompleteBinaryTreeNodeChildEnumerator childEnumerator)
    {
    }

    private static IEnumerable<ulong> _Roots =
      new ulong[] { 0 };
  }
}
