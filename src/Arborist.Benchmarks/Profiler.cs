using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;

namespace Arborist.Benchmarks
{
  public class Profiler
  {
    private readonly ITreenumerable<int> _Tree = new CompleteBinaryTree();
    private ITreenumerable<int> GetTreeWithDepth(int depth) =>
      _Tree
      .PruneAfter(nodeContext => nodeContext.Position.Depth == depth);

    public int DepthFirstTraversalBig() =>
      GetTreeWithDepth(19)
      .PreOrderTraversal()
      .Count();

    //public int BreadthFirstTraversalBig() => GetTreeWithDepth(21).GetBreadthFirstTraversal().Count();
  }
}
