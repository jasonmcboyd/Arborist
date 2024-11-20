using Arborist.Treenumerables;
using Arborist.Trees;

namespace Arborist.Trees
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
          node => node,
          new[] { 0 })
    {
    }
  }
}
