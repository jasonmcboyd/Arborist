using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  public class TreenumerableBenchmarks
  {
    private readonly ITreenumerable<ulong> _Tree = new CompleteBinaryTree();
    
    private ITreenumerable<ulong> GetTreeWithDepth(int depth) => _Tree.PruneAfter(nodeContext => nodeContext.Position.Depth == depth);

    [Benchmark]
    public int DepthFirstTraversalDepth18() => GetTreeWithDepth(18).GetDepthFirstTraversal().Count();
    [Benchmark]
    public int DepthFirstTraversalDepth19() => GetTreeWithDepth(19).GetDepthFirstTraversal().Count();
    [Benchmark]
    public int DepthFirstTraversalDepth20() => GetTreeWithDepth(20).GetDepthFirstTraversal().Count();

    [Benchmark]
    public int BreadthFirstTraversalDepth18() => GetTreeWithDepth(18).GetBreadthFirstTraversal().Count();
    [Benchmark]
    public int BreadthFirstTraversalDepth19() => GetTreeWithDepth(19).GetBreadthFirstTraversal().Count();
    [Benchmark]
    public int BreadthFirstTraversalDepth20() => GetTreeWithDepth(20).GetBreadthFirstTraversal().Count();
  }
}
