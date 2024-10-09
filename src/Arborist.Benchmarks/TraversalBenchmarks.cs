using Arborist.Core;
using Arborist.Linq;
using Arborist.Linq.Extensions;
using Arborist.Nodes;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  public class TraversalBenchmarks
  {
    private readonly ITreenumerable<ulong> _Tree = new CompleteBinaryTree();

    private ITreenumerable<ulong> GetTreeWithDepth(int depth) =>
      _Tree
      .PruneAfter(nodeContext => nodeContext.Position.Depth == depth);

    private ITreenumerable<ulong> GetTreeWithDepthWhere(int depth) =>
      _Tree
      .PruneAfter(nodeContext => nodeContext.Position.Depth == depth)
      .Where(nodeContext => nodeContext.Position.Depth % 2 == 1);

    //[Benchmark]
    //public int DepthFirstTraversalDepth17() => GetTreeWithDepth(17).GetDepthFirstTraversal().Count();
    //[Benchmark]
    //public int DepthFirstTraversalDepth18() => GetTreeWithDepth(18).GetDepthFirstTraversal().Count();
    //[Benchmark]
    //public int DepthFirstTraversalDepth19() => GetTreeWithDepth(19).GetDepthFirstTraversal().Count();

    //[Benchmark]
    //public int BreadthFirstTraversalDepth17() => GetTreeWithDepth(17).GetBreadthFirstTraversal().Count();
    //[Benchmark]
    //public int BreadthFirstTraversalDepth18() => GetTreeWithDepth(18).GetBreadthFirstTraversal().Count();
    //[Benchmark]
    //public int BreadthFirstTraversalDepth19() => GetTreeWithDepth(19).GetBreadthFirstTraversal().Count();

    //[Benchmark]
    //public int DepthFirstWhereDepth17() => GetTreeWithDepthWhere(17).GetDepthFirstTraversal().Count();
    //[Benchmark]
    //public int DepthFirstWhereDepth18() => GetTreeWithDepthWhere(18).GetDepthFirstTraversal().Count();
    //[Benchmark]
    //public int DepthFirstWhereDepth19() => GetTreeWithDepthWhere(19).GetDepthFirstTraversal().Count();

    //[Benchmark]
    //public int BreadthFirstWhereDepth17() => GetTreeWithDepthWhere(17).GetBreadthFirstTraversal().Count();
    //[Benchmark]
    //public int BreadthFirstWhereDepth18() => GetTreeWithDepthWhere(18).GetBreadthFirstTraversal().Count();
    //[Benchmark]
    //public int BreadthFirstWhereDepth19() => GetTreeWithDepthWhere(19).GetBreadthFirstTraversal().Count();


    [Benchmark]
    public int PruneBeforeBFT() =>
      Enumerable
      .Range(0, 1_000_000)
      .ToForest()
      .PruneBefore(_ => true)
      .GetBreadthFirstTraversal()
      .Count();

    [Benchmark]
    public int PruneBeforeDFT() =>
      Enumerable
      .Range(0, 1_000_000)
      .ToForest()
      .PruneBefore(_ => true)
      .GetDepthFirstTraversal()
      .Count();
  }
}
