using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [ShortRunJob]
  public class LevelOrderTraversal
  {
    [Benchmark]
    public void LevelOrderTraversal_DeepTree() =>
      Treenumerables
      .GetDeepTree(20)
      .LevelOrderTraversal()
      .Consume();

    [Benchmark]
    public void LevelOrderTraversal_TriangleTree() =>
      new TriangleTree()
      .PruneAfter(nodeContext => nodeContext.Position.Depth == 1447)
      .LevelOrderTraversal()
      .Consume();

    [Benchmark]
    public void LevelOrderTraversal_WideTree_PruneBeforeDepth_20() =>
      new CompleteBinaryTree()
      .PruneBefore(nodeContext => nodeContext.Position.Depth == 20)
      .LevelOrderTraversal()
      .Consume();

    [Benchmark]
    public void LevelOrderTraversal_WideTree_PruneAfterDepth_19() =>
      new CompleteBinaryTree()
      .PruneAfter(nodeContext => nodeContext.Position.Depth == 19)
      .LevelOrderTraversal()
      .Consume();
  }
}
