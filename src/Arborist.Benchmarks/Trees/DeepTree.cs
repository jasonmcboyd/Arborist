using Arborist.Treenumerables;
using System.Linq;

namespace Arborist.Benchmarks.Trees
{
  public class DeepTree : Treenumerable<int, int, DeepTreeNodeChildEnumerator>
  {
    public DeepTree(int width)
      : base(
          nodeContext => new DeepTreeNodeChildEnumerator(nodeContext.Node - 1),
          MoveNextChild,
          DisposeChildEnumeratorDelegate,
          node => node,
          EnumerableExtensions.Geometric(1, 2).Take(width))
    {
    }

    private readonly int _Width;

    private static bool MoveNextChild(
      ref DeepTreeNodeChildEnumerator childEnumerator,
      out NodeAndSiblingIndex<int> childNodeAndSiblingIndex)
      => childEnumerator.TryMoveNext(out childNodeAndSiblingIndex);

    private static void DisposeChildEnumeratorDelegate(
      ref DeepTreeNodeChildEnumerator childEnumerator)
    {
    }
  }
}
