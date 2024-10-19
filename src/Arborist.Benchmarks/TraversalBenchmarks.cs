using Arborist.Core;
using Arborist.Linq;
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

    [Benchmark]
    public int DepthFirstTraversalDepth18() => GetTreeWithDepth(18).PreOrderTraversal().Count();
    [Benchmark]
    public int DepthFirstTraversalDepth19() => GetTreeWithDepth(19).PreOrderTraversal().Count();

    [Benchmark]
    public int BreadthFirstTraversalDepth18() => GetTreeWithDepth(18).LevelOrderTraversal().Count();
    [Benchmark]
    public int BreadthFirstTraversalDepth19() => GetTreeWithDepth(19).LevelOrderTraversal().Count();

    [Benchmark]
    public int DepthFirstWhereDepth18() => GetTreeWithDepthWhere(18).PreOrderTraversal().Count();
    [Benchmark]
    public int DepthFirstWhereDepth19() => GetTreeWithDepthWhere(19).PreOrderTraversal().Count();

    [Benchmark]
    public int BreadthFirstWhereDepth18() => GetTreeWithDepthWhere(18).LevelOrderTraversal().Count();
    [Benchmark]
    public int BreadthFirstWhereDepth19() => GetTreeWithDepthWhere(19).LevelOrderTraversal().Count();

    [Benchmark]
    public int CountLeaves() => GetTreeWithDepthWhere(19).GetLeaves().Count();


    [Benchmark]
    public int PruneBeforeBFT() =>
      Enumerable
      .Range(0, 1_000_000)
      .ToForest()
      .PruneBefore(_ => true)
      .LevelOrderTraversal()
      .Count();

    [Benchmark]
    public int PruneBeforeDFT() =>
      Enumerable
      .Range(0, 1_000_000)
      .ToForest()
      .PruneBefore(_ => true)
      .PreOrderTraversal()
      .Count();

    [Benchmark]
    public int PruneAfterDFT() =>
      Enumerable
      .Range(0, 2_000_000)
      .ToForest()
      .PruneAfter(_ => true)
      .PreOrderTraversal()
      .Count();

    [Benchmark]
    public int SelectComposition() =>
      Enumerable
      .Range(0, 1_000_000)
      .ToForest()
      .Select(x => x.Node * 2)
      .Select(x => x.Node + 'a')
      .Select(x => x.Node + 1)
      .Select(x => (char)x.Node)
      .PreOrderTraversal()
      .Count();
  }
}
