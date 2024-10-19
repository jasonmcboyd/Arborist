using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;

namespace Arborist.Benchmarks
{
  internal static class Trees
  {
    public static ITreenumerable<int> GetWideTree(int depth) =>
      new CompleteBinaryTree()
      .PruneAfter(nodeContext => nodeContext.Position.Depth == depth);
  }
}
