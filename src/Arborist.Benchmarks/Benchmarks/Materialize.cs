using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;
using System.Linq;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [BenchmarkCategory("LINQ", "Materialize")]
  public class Materialize
  {
    [Benchmark]
    public ITreenumerable<int> TriangleTree_1448()
      => new TriangleTree()
        .PruneAfter(nodeContext => nodeContext.Position.Depth == 1448)
        .Materialize();

    [Benchmark]
    public ITreenumerable<int> DegenerateTree_1M()
      => Enumerable.Range(0, 1_000_000).ToDegenerateTree().Materialize();
  }
}
