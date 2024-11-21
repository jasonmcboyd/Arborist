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
    public int CountNodes_DeepTree() =>
      Treenumerables
      .GetDeepTree(20)
      .CountNodes();

    [Benchmark]
    public int CountNodes_TriangleTree() =>
      new TriangleTree()
      .PruneAfter(nodeContext => nodeContext.Position.Depth == 2048)
      .CountNodes();

    [Benchmark]
    public int CountNodes_WideTree() =>
      new CompleteBinaryTree()
      .PruneAfter(nodeContext => nodeContext.Position.Depth == 20)
      .CountNodes();
  }
}
