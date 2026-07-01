using Copse.Core;
using Copse.Linq;
using Copse.Trees;
using BenchmarkDotNet.Attributes;

namespace Copse.Benchmarks
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
