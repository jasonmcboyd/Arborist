using Arborist.Benchmarks;
using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;
using System.Linq;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [ShortRunJob]
  public class BreadthFirstWhere
  {
    [Benchmark]
    public void TriangleTree_1448()
      => new TriangleTree()
      .PruneAfter(nodeContext => nodeContext.Position.Depth == 1448)
      .Where(nodeContext => (nodeContext.Position.Depth + nodeContext.Position.SiblingIndex) % 2 == 0)
      .Consume(TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void TrivialForest_WhereAll_1M()
      => Enumerable
      .Range(0, 1_000_000)
      .ToTrivialForest()
      .Where(_ => true)
      .Consume(TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void TrivialForest_WhereNone_1M()
      => Enumerable
      .Range(0, 1_000_000)
      .ToTrivialForest()
      .Where(_ => false)
      .Consume(TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void DegenerateTree_WhereAll_1M()
      => Enumerable
      .Range(0, 1_000_000)
      .ToDegenerateTree()
      .Where(_ => true)
      .Consume(TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void DegenerateTree_WhereNone_1M()
      => Enumerable
      .Range(0, 1_000_000)
      .ToDegenerateTree()
      .Where(_ => false)
      .Consume(TreeTraversalStrategy.BreadthFirst);
  }
}
