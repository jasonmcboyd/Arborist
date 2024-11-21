using Arborist.Linq;
using BenchmarkDotNet.Attributes;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [ShortRunJob]
  public class GetLeaves
  {
    [Benchmark]
    public void GetLeaves_DeepTree() =>
      Treenumerables
      .GetDeepTree(19)
      .GetLeaves()
      .Consume();

    [Benchmark]
    public void GetLeaves_WideTree() =>
      Treenumerables
      .GetWideTree(19)
      .GetLeaves()
      .Consume();
  }
}
