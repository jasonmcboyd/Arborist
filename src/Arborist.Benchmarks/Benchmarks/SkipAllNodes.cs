using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [ShortRunJob]
  public class SkipAllNodes
  {
    [Benchmark]
    public void BreadthFirstTreenumerator_SkipNodes()
    {
      var treenumerable =
        new TriangleTree()
        .PruneAfter(nodeContext => nodeContext.Position.Depth == (1 << 12));

      using (var treenumerator = treenumerable.GetBreadthFirstTreenumerator())
      {
        while (treenumerator.MoveNext(NodeTraversalStrategies.SkipNode)) ;
      }
    }

    [Benchmark]
    public void DepthFirstTreenumerator_SkipNodes()
    {
      var treenumerable =
        new TriangleTree()
        .PruneAfter(nodeContext => nodeContext.Position.Depth == (1 << 12));

      using (var treenumerator = treenumerable.GetDepthFirstTreenumerator())
      {
        while (treenumerator.MoveNext(NodeTraversalStrategies.SkipNode)) ;
      }
    }
  }
}
