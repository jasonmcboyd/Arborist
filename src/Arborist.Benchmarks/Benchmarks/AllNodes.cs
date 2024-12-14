using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [ShortRunJob]
  public class AllNodes
  {
    [Benchmark]
    public void Bft_CompleteBinaryTree_PruneBefore_19() =>
      new CompleteBinaryTree()
      .PruneBefore(nodeContext => nodeContext.Position.Depth == 19)
      .AllNodes(_ => true, TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void Dft_CompleteBinaryTree_PruneBefore_19() =>
      new CompleteBinaryTree()
      .PruneBefore(nodeContext => nodeContext.Position.Depth == 19)
      .AllNodes(_ => true, TreeTraversalStrategy.DepthFirst);

    [Benchmark]
    public void Bft_TriangleTree_PruneBefore_19() =>
      new TriangleTree()
      .PruneBefore(nodeContext => nodeContext.Position.Depth == 1448)
      .AllNodes(_ => true, TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void Dft_TriangleTree_PruneBefore_19() =>
      new TriangleTree()
      .PruneBefore(nodeContext => nodeContext.Position.Depth == 1448)
      .AllNodes(_ => true, TreeTraversalStrategy.DepthFirst);
  }
}
