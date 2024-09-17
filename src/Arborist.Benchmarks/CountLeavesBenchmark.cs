using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;

namespace Arborist.Benchmarks
{
  //[MemoryDiagnoser, EtwProfiler]
  [MemoryDiagnoser]
  public class CountLeavesBenchmark
  {
    private readonly ITreenumerable<ulong> _Tree = new CompleteBinaryTree();

    private int CountLeaves(int level) => _Tree.PruneAfter(x => x.Position.Depth == level).GetLeaves().Count();

    [Benchmark]
    public int CountLeavesLevel20() => CountLeaves(20);
  }
}
