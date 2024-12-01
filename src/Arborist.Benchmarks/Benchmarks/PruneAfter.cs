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
    public void Bft_TrivialForest_1M() =>
      Enumerable
      .Range(0, 1_000_000)
      .ToTrivialForest()
      .PruneAfter(_ => true)
      .Consume(TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void Dft_TrivialForest_1M() =>
      Enumerable
      .Range(0, 1_000_000)
      .ToTrivialForest()
      .PruneAfter(_ => true)
      .Consume(TreeTraversalStrategy.DepthFirst);
  }
}
