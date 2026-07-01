using Copse;
using Copse.Linq;
using Copse.Trees;
using BenchmarkDotNet.Attributes;
using System.Linq;

namespace Copse.Benchmarks
{
  [MemoryDiagnoser]
  [BenchmarkCategory("LINQ", "Leaffix")]
  public class LeaffixAggregate
  {
    private static int SubtreeNodeCount(NodeContext<int> nodeContext, ChildAccumulations<int> children)
    {
      var count = 1;
      foreach (var child in children)
        count += child;
      return count;
    }

    [Benchmark]
    public int TriangleTree_1448()
      => new TriangleTree()
        .PruneAfter(nodeContext => nodeContext.Position.Depth == 1448)
        .LeaffixAggregate(_ => 1, SubtreeNodeCount)
        .Sum();

    [Benchmark]
    public int DegenerateTree_1M()
      => Enumerable.Range(0, 1_000_000).ToDegenerateTree()
        .LeaffixAggregate(_ => 1, SubtreeNodeCount)
        .Sum();

    [Benchmark]
    public int TrivialForest_1M()
      => Enumerable.Range(0, 1_000_000).ToTrivialForest()
        .LeaffixAggregate(_ => 1, SubtreeNodeCount)
        .Sum();
  }
}
