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
    public void TriangleTree()
      => new TriangleTree()
      .GetBreadthFirstTraversal(nc =>
        nc.Position.Depth == 2896
        ? NodeTraversalStrategies.SkipDescendants
        : NodeTraversalStrategies.TraverseAll)
      .Consume();

    [Benchmark]
    public void CompleteBinaryTree()
      => new CompleteBinaryTree()
      .GetDepthFirstTraversal(nc =>
        nc.Position.Depth == 21
        ? NodeTraversalStrategies.SkipDescendants
        : NodeTraversalStrategies.TraverseAll)
      .Consume();

    [Benchmark]
    public void TrivialForest()
      => Enumerable
      .Range(0, 1 << 22)
      .ToTrivialForest()
      .Consume(TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void DegenerateTree()
      => Enumerable
      .Range(0, 1 << 22)
      .ToDegenerateTree()
      .Consume(TreeTraversalStrategy.BreadthFirst);
  }
}
