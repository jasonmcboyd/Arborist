using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [BenchmarkCategory("LINQ", "Skip")]
  public class SkipAllNodes
  {
    [Benchmark]
    public void Bft_TriangleTree_1448()
    {
      var treenumerable =
        new TriangleTree()
        .PruneAfter(nodeContext => nodeContext.Position.Depth == 1448);

      using (var treenumerator = treenumerable.GetBreadthFirstTreenumerator())
        while (treenumerator.MoveNext(NodeTraversalStrategies.SkipNode)) ;
    }

    [Benchmark]
    public void Dft_TriangleTree_1448()
    {
      var treenumerable =
        new TriangleTree()
        .PruneAfter(nodeContext => nodeContext.Position.Depth == 1448);

      using (var treenumerator = treenumerable.GetDepthFirstTreenumerator())
        while (treenumerator.MoveNext(NodeTraversalStrategies.SkipNode)) ;
    }
  }
}
