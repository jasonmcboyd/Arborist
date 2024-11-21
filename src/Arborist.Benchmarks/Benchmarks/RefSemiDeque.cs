using BenchmarkDotNet.Attributes;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [ShortRunJob]
  public class RefSemiDeque
  {
    [Benchmark]
    public void RefSemiDeque_Add()
    {
      var deque = new RefSemiDeque<int>();

      for (int i = 0; i < (1 << 23); i++)
        deque.AddLast(i);
    }

    [Benchmark]
    public void RefSemiDeque_RemoveFirst()
    {
      var deque = new RefSemiDeque<int>();

      for (int i = 0; i < (1 << 23); i++)
        deque.AddLast(i);

      while (deque.Count > 0)
        deque.RemoveFirst();
    }

    [Benchmark]
    public void RefSemiDeque_RemoveLast()
    {
      var deque = new RefSemiDeque<int>();

      for (int i = 0; i < (1 << 23); i++)
        deque.AddLast(i);

      while (deque.Count > 0)
        deque.RemoveLast();
    }
  }
}
