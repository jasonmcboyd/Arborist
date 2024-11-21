using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [ShortRunJob]
  public class PostOrderTraversal
  {
    [Benchmark]
    public void PostOrderTraversal_DeepTree() =>
      Treenumerables
      .GetDeepTree(20)
      .PostOrderTraversal()
      .Consume();

    [Benchmark]
    public void PostOrderTraversal_TriangleTree() =>
      new TriangleTree()
      .PruneAfter(nodeContext => nodeContext.Position.Depth == 1447)
      .PostOrderTraversal()
      .Consume();

    [Benchmark]
    public void PostOrderTraversal_WideTree_PruneBeforeDepth_20() =>
      new CompleteBinaryTree()
      .PruneBefore(nodeContext => nodeContext.Position.Depth == 20)
      .PostOrderTraversal()
      .Consume();

    [Benchmark]
    public void PostOrderTraversal_WideTree_PruneAfterDepth_19() =>
      new CompleteBinaryTree()
      .PruneAfter(nodeContext => nodeContext.Position.Depth == 19)
      .PostOrderTraversal()
      .Consume();
  }
}
