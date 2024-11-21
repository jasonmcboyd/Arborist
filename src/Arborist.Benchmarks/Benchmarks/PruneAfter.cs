using Arborist.Core;
using Arborist.Linq;
using BenchmarkDotNet.Attributes;
using System.Linq;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [ShortRunJob]
  public class PruneAfter
  {
    [Benchmark]
    public void PruneAfterBft() =>
      Enumerable
      .Range(0, 1_000_000)
      .ToTrivialForest()
      .PruneAfter(_ => true)
      .Consume(TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void PruneAfterDft() =>
      Enumerable
      .Range(0, 1_000_000)
      .ToTrivialForest()
      .PruneAfter(_ => true)
      .Consume(TreeTraversalStrategy.DepthFirst);
  }
}
