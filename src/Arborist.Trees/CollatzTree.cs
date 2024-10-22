using Arborist.Treenumerables;

namespace Arborist.Trees
{
  public class CollatzTree : Treenumerable<ulong, ulong, CollatzTreeNodeChildEnumerator>
  {
    public CollatzTree()
      : base(
          nodeContext => new CollatzTreeNodeChildEnumerator(nodeContext.Node),
          ChildEnumeratorMoveNextDelegate,
          DisposeChildEnumeratorDelegate,
          node => node,
          _Roots)
    {
    }

    private static bool ChildEnumeratorMoveNextDelegate(
      ref CollatzTreeNodeChildEnumerator childEnumerator,
      out NodeAndSiblingIndex<ulong> childNodeAndSiblingIndex)
      => childEnumerator.TryMoveNext(out childNodeAndSiblingIndex);

    private static void DisposeChildEnumeratorDelegate(
      ref CollatzTreeNodeChildEnumerator childEnumerator)
    {
    }

    private static IEnumerable<ulong> _Roots =
      new ulong[] { 2 };
  }
}
