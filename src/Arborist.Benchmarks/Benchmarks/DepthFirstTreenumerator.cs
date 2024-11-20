using Arborist.Benchmarks.Trees;
using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;
using System.Linq;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  //[ShortRunJob]
  public class DepthFirstTreenumerator
  {
    [Benchmark]
    public void TriangleTree()
      => new TriangleTree()
      .GetDepthFirstTraversal(nc =>
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

  }
}
