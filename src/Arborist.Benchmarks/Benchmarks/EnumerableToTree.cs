using Arborist.Linq;
using BenchmarkDotNet.Attributes;
using System.Linq;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [ShortRunJob]
  public class EnumerableToTree
  {
    [Benchmark]
    public int ToDegenerateTree() =>
      Enumerable.Range(0, 1 << 20)
      .ToDegenerateTree()
      .CountNodes();

    [Benchmark]
    public int ToDegenerateTreeUsingToTree() =>
      Enumerable.Range(0, 1 << 20)
      .ToTree(_ => true)
      .CountNodes();

    [Benchmark]
    public int ToTrivialForest() =>
      Enumerable.Range(0, 1 << 20)
      .ToTrivialForest()
      .CountNodes();

    [Benchmark]
    public int ToTrivialForestUsingToTree() =>
      Enumerable.Range(0, 1 << 20)
      .ToTree(_ => false)
      .CountNodes();
  }
}
