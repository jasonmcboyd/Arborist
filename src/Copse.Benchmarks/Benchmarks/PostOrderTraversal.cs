using Copse.Linq;
using Copse.Trees;
using BenchmarkDotNet.Attributes;

namespace Copse.Benchmarks
{
  [MemoryDiagnoser]
  [BenchmarkCategory("Traversal", "PostOrder")]
  public class PostOrderTraversal
  {
    [Benchmark]
    public void DeepTree() =>
      Treenumerables
      .GetDeepTree(20)
      .PostOrderTraversal()
      .Consume();

    [Benchmark]
    public void TriangleTree_PruneAfter_1447() =>
      new TriangleTree()
      .PruneAfter(nodeContext => nodeContext.Position.Depth == 1447)
      .PostOrderTraversal()
      .Consume();

    [Benchmark]
    public void CompleteBinaryTree_PruneBefore_20() =>
      new CompleteBinaryTree()
      .PruneBefore(nodeContext => nodeContext.Position.Depth == 20)
      .PostOrderTraversal()
      .Consume();

    [Benchmark]
    public void CompleteBinaryTree_PruneAfter_19() =>
      new CompleteBinaryTree()
      .PruneAfter(nodeContext => nodeContext.Position.Depth == 19)
      .PostOrderTraversal()
      .Consume();
  }
}
