using Arborist.Treenumerables;
using Arborist.Trees;

namespace Arborist.Benchmarks.Trees
{
  /// <summary>
  /// Interesting fact about this tree is that the width of the levels follows
  /// Pascal's triangle.
  /// </summary>
  public class TriangleTree : Treenumerable<int, int, TriangleTreeNodeChildEnumerator>
  {
    public TriangleTree()
      : base(
          nodeContext => new TriangleTreeNodeChildEnumerator(nodeContext.Node == 0 ? nodeContext.Position.Depth + 2 : 0),
          MoveNextChild,
          DisposeChildEnumeratorDelegate,
          node => node,
          new[] { 0 })
    {
    }

    private static bool MoveNextChild(
      ref TriangleTreeNodeChildEnumerator childEnumerator,
      out NodeAndSiblingIndex<int> childNodeAndSiblingIndex)
      => childEnumerator.TryMoveNext(out childNodeAndSiblingIndex);

    private static void DisposeChildEnumeratorDelegate(
      ref TriangleTreeNodeChildEnumerator childEnumerator)
    {
    }
  }
}
