using Arborist;
using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;
using System.Linq;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [BenchmarkCategory("LINQ", "Leaffix")]
  public class LeaffixScan
  {
    // Each node accumulates its own subtree node count from its children's counts.
    private static int SubtreeNodeCount(NodeContext<int> nodeContext, ChildAccumulations<int> children)
    {
      var count = 1;
      foreach (var child in children)
        count += child;
      return count;
    }

    [Benchmark]
    public ITreenumerable<int> TriangleTree_1448()
      => new TriangleTree()
        .PruneAfter(nodeContext => nodeContext.Position.Depth == 1448)
        .LeaffixScan(_ => 1, SubtreeNodeCount);

    [Benchmark]
    public ITreenumerable<int> DegenerateTree_1M()
      => Enumerable.Range(0, 1_000_000).ToDegenerateTree()
        .LeaffixScan(_ => 1, SubtreeNodeCount);
  }
}
