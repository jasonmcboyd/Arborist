using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [ShortRunJob]
  public class PreorderTraversal
  {
    [Benchmark]
    public void DeepTree() =>
      Treenumerables
      .GetDeepTree(20)
      .PreOrderTraversal()
      .Consume();

    [Benchmark]
    public void TriangleTree_PruneAfter_1447() =>
      new TriangleTree()
      .PruneAfter(nodeContext => nodeContext.Position.Depth == 1447)
      .PreOrderTraversal()
      .Consume();

    [Benchmark]
    public void CompleteBinaryTree_PruneBefore_20() =>
      new CompleteBinaryTree()
      .PruneBefore(nodeContext => nodeContext.Position.Depth == 20)
      .PreOrderTraversal()
      .Consume();

    [Benchmark]
    public void CompleteBinaryTree_PruneAfterDepth_19() =>
      new CompleteBinaryTree()
      .PruneAfter(nodeContext => nodeContext.Position.Depth == 19)
      .PreOrderTraversal()
      .Consume();
  }
}
