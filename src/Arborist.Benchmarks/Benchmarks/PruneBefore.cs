using Arborist.Core;
using Arborist.Linq;
using BenchmarkDotNet.Attributes;
using System.Linq;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [ShortRunJob]
  public class PruneBefore
  {
    [Benchmark]
    public void PruneBeforeBft() =>
      Enumerable
      .Range(0, 1_000_000)
      .ToTrivialForest()
      .PruneBefore(_ => true)
      .Consume(TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void PruneBeforeDft() =>
      Enumerable
      .Range(0, 1_000_000)
      .ToTrivialForest()
      .PruneBefore(_ => true)
      .Consume(TreeTraversalStrategy.DepthFirst);
  }
}
