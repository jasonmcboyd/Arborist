using Arborist.Benchmarks.Trees;
using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;

namespace Arborist.Benchmarks
{
  internal static class Treenumerables
  {
    public static ITreenumerable<int> GetDeepTree(int width) => new DeepTree(width);

    public static ITreenumerable<int> GetWideTree(int depth) =>
      new CompleteBinaryTree()
      .PruneBefore(nodeContext => nodeContext.Position.Depth == depth);

    public static ITreenumerable<int> GetTree(int cutoff, TreeShape treeShape)
    {
      return
        treeShape == TreeShape.Deep
        ? GetDeepTree(cutoff)
        : GetWideTree(cutoff);
    }
  }
}
