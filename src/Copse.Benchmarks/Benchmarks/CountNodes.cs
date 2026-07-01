using Copse.Linq;
using Copse.Trees;
using BenchmarkDotNet.Attributes;

namespace Copse.Benchmarks
{
  [MemoryDiagnoser]
  [BenchmarkCategory("LINQ", "Query")]
  public class CountNodes
  {
    [Benchmark]
    public int DeepTree() =>
      Treenumerables
      .GetDeepTree(20)
      .CountNodes();

    [Benchmark]
    public int TriangleTree_PruneAfter_2048() =>
      new TriangleTree()
      .PruneAfter(nodeContext => nodeContext.Position.Depth == 2048)
      .CountNodes();

    [Benchmark]
    public int CompleteBinaryTree_PruneAfter_20() =>
      new CompleteBinaryTree()
      .PruneAfter(nodeContext => nodeContext.Position.Depth == 20)
      .CountNodes();
  }
}
