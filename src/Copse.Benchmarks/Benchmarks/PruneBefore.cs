using Copse.Core;
using Copse.Linq;
using BenchmarkDotNet.Attributes;
using System.Linq;

namespace Copse.Benchmarks
{
  [MemoryDiagnoser]
  [BenchmarkCategory("LINQ", "Pruning")]
  public class PruneBefore
  {
    [Benchmark]
    public void Bft_TrivialForest_1M() =>
      Enumerable
      .Range(0, 1_000_000)
      .ToTrivialForest()
      .PruneBefore(_ => true)
      .Consume(TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void Dft_TrivialForest_1M() =>
      Enumerable
      .Range(0, 1_000_000)
      .ToTrivialForest()
      .PruneBefore(_ => true)
      .Consume(TreeTraversalStrategy.DepthFirst);
  }
}
