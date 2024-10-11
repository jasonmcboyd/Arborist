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

    private static TryMoveNextChildResult<CollatzTreeNode> ChildEnumeratorMoveNextDelegate(
      ref CollatzTreeNodeChildEnumerator childEnumerator)
      => childEnumerator.TryMoveNext();

    private static void DisposeChildEnumeratorDelegate(
      ref CollatzTreeNodeChildEnumerator childEnumerator)
    {
    }

    private static IEnumerable<CollatzTreeNode> _Roots =
      new CollatzTreeNode[] { new CollatzTreeNode(2) };
  }
}
