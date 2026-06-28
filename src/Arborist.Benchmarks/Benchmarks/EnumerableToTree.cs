using Arborist.Linq;
using BenchmarkDotNet.Attributes;
using System.Linq;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [BenchmarkCategory("Conversion")]
  public class EnumerableToTree
  {
    [Benchmark]
    public int ToDegenerateTree() =>
      Enumerable.Range(0, 1 << 20)
      .ToDegenerateTree()
      .CountNodes();

    [Benchmark]
    public int ToTrivialForest() =>
      Enumerable.Range(0, 1 << 20)
      .ToTrivialForest()
      .CountNodes();
  }
}
