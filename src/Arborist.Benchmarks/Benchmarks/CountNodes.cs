using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [ShortRunJob]
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
