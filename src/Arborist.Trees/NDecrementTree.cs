using Arborist.Treenumerables;
using Arborist.Trees;

namespace Arborist.Benchmarks.Trees
{
  /// <summary>
  /// Interesting fact about this tree is that the width of the levels follows
  /// Pascal's triangle.
  /// </summary>
  public class NDecrementTree : Treenumerable<int, int, NDecrementTreeNodeChildEnumerator>
  {
    public NDecrementTree(int depth)
      : base(
          nodeContext => new NDecrementTreeNodeChildEnumerator(nodeContext.Node),
          MoveNextChild,
          DisposeChildEnumeratorDelegate,
          node => node,
          new[] { depth - 1 })
    {
    }

    private static bool MoveNextChild(
      ref NDecrementTreeNodeChildEnumerator childEnumerator,
      out NodeAndSiblingIndex<int> childNodeAndSiblingIndex)
      => childEnumerator.TryMoveNext(out childNodeAndSiblingIndex);

    private static void DisposeChildEnumeratorDelegate(
      ref NDecrementTreeNodeChildEnumerator childEnumerator)
    {
    }
  }
}
