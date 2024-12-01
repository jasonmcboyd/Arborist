using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;
using System.Linq;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [ShortRunJob]
  public class BreadthFirstTreenumerator
  {
    [Benchmark]
    public void TriangleTree_2896()
      => new TriangleTree()
      .GetBreadthFirstTraversal(nc =>
        nc.Position.Depth == 2896
        ? NodeTraversalStrategies.SkipDescendants
        : NodeTraversalStrategies.TraverseAll)
      .Consume();

    [Benchmark]
    public void CompleteBinaryTree_21()
      => new CompleteBinaryTree()
      .GetDepthFirstTraversal(nc =>
        nc.Position.Depth == 21
        ? NodeTraversalStrategies.SkipDescendants
        : NodeTraversalStrategies.TraverseAll)
      .Consume();

    [Benchmark]
    public void TrivialForest_4M()
      => Enumerable
      .Range(0, 4_000_000)
      .ToTrivialForest()
      .Consume(TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void DegenerateTree_4M()
      => Enumerable
      .Range(0, 4_000_000)
      .ToDegenerateTree()
      .Consume(TreeTraversalStrategy.BreadthFirst);
  }
}
