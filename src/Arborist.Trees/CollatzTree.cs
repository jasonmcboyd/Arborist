using Arborist.Common;
using Arborist.Treenumerables;

namespace Arborist.Trees
{
  public class CollatzTree : Treenumerable<ulong, CollatzTreeNode, CollatzTreeNodeChildEnumerator>
  {
    public CollatzTree()
      : base(
          node => node.GetChildEnumerator(),
          ChildEnumeratorMoveNextDelegate,
          DisposeChildEnumeratorDelegate,
          node => node.Value,
          _Roots)
    {
    }

    private static bool ChildEnumeratorMoveNextDelegate(
      ref CollatzTreeNodeChildEnumerator childEnumerator,
      out NodeAndSiblingIndex<CollatzTreeNode> childNodeAndSiblingIndex)
      => childEnumerator.TryMoveNext(out childNodeAndSiblingIndex);

    private static void DisposeChildEnumeratorDelegate(
      ref CollatzTreeNodeChildEnumerator childEnumerator)
    {
    }

    private static IEnumerable<CollatzTreeNode> _Roots =
      new CollatzTreeNode[] { new CollatzTreeNode(2) };
  }
}
