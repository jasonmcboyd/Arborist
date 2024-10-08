using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;

namespace Arborist.Benchmarks
{
  public class Profiler
  {
    private readonly ITreenumerable<ulong> _Tree = new CompleteBinaryTree();
    private ITreenumerable<ulong> GetTreeWithDepth(int depth) => _Tree.PruneAfter(nodeContext => nodeContext.Position.Depth == depth);

    public int DepthFirstTraversalDepthBig() => GetTreeWithDepth(21).GetDepthFirstTraversal().Count();
    public int BreadthFirstTraversalDepthBig() => GetTreeWithDepth(21).GetBreadthFirstTraversal().Count();
  }
}
