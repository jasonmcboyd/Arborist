using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [BenchmarkCategory("LINQ", "Query")]
  public class AnyNodes
  {
    [Benchmark]
    public void Bft_CompleteBinaryTree_PruneBefore_19() =>
      new CompleteBinaryTree()
      .PruneBefore(nodeContext => nodeContext.Position.Depth == 19)
      .AnyNodes(_ => false, TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void Dft_CompleteBinaryTree_PruneBefore_19() =>
      new CompleteBinaryTree()
      .PruneBefore(nodeContext => nodeContext.Position.Depth == 19)
      .AnyNodes(_ => false, TreeTraversalStrategy.DepthFirst);

    [Benchmark]
    public void Bft_TriangleTree_PruneBefore_19() =>
      new TriangleTree()
      .PruneBefore(nodeContext => nodeContext.Position.Depth == 1448)
      .AnyNodes(_ => false, TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void Dft_TriangleTree_PruneBefore_19() =>
      new TriangleTree()
      .PruneBefore(nodeContext => nodeContext.Position.Depth == 1448)
      .AnyNodes(_ => false, TreeTraversalStrategy.DepthFirst);
  }
}
