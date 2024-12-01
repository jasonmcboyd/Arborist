using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [ShortRunJob]
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
