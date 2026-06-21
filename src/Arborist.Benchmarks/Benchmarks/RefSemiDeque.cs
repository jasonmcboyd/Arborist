using BenchmarkDotNet.Attributes;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [BenchmarkCategory("DataStructures")]
  public class RefSemiDeque
  {
    [Benchmark]
    public void Add_8M()
    {
      var deque = new RefSemiDeque<int>();

      for (int i = 0; i < 8_000_000; i++)
        deque.AddLast(i);
    }

    [Benchmark]
    public void RemoveFirst_8M()
    {
      var deque = new RefSemiDeque<int>();

      for (int i = 0; i < 8_000_000; i++)
        deque.AddLast(i);

      while (deque.Count > 0)
        deque.RemoveFirst();
    }

    [Benchmark]
    public void RemoveLast_8M()
    {
      var deque = new RefSemiDeque<int>();

      for (int i = 0; i < 8_000_000; i++)
        deque.AddLast(i);

      while (deque.Count > 0)
        deque.RemoveLast();
    }
  }
}
