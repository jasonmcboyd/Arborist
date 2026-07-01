using Copse.Linq;
using Copse.Trees;
using BenchmarkDotNet.Attributes;

namespace Copse.Benchmarks
{
  [MemoryDiagnoser]
  [BenchmarkCategory("LINQ", "Query")]
  public class GetLeaves
  {
    [Benchmark]
    public void DeepTree() =>
      Treenumerables
      .GetDeepTree(19)
      .GetLeaves()
      .Consume();

    [Benchmark]
    public void CompleteBinaryTree_PruneBefore_20() =>
      new CompleteBinaryTree()
      .PruneBefore(nodeContext => nodeContext.Position.Depth == 20)
      .GetLeaves()
      .Consume();
  }
}
