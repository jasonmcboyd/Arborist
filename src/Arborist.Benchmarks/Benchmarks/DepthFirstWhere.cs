using Arborist.Benchmarks;
using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;
using System.Linq;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [BenchmarkCategory("LINQ", "Filter", "DepthFirst")]
  public class DepthFirstWhere
  {
    [Benchmark]
    public void TriangleTree_PruneAfter_1448()
      => new TriangleTree()
      .PruneAfter(nodeContext => nodeContext.Position.Depth == 1448)
      .Where(nodeContext => (nodeContext.Position.Depth + nodeContext.Position.SiblingIndex) % 2 == 0)
      .Consume(TreeTraversalStrategy.DepthFirst);

    [Benchmark]
    public void WhereAll_TrivialForest_1M()
      => Enumerable
      .Range(0, 1_000_000)
      .ToTrivialForest()
      .Where(_ => true)
      .Consume(TreeTraversalStrategy.DepthFirst);

    [Benchmark]
    public void WhereNone_TrivialForest_1M()
      => Enumerable
      .Range(0, 1_000_000)
      .ToTrivialForest()
      .Where(_ => false)
      .Consume(TreeTraversalStrategy.DepthFirst);

    [Benchmark]
    public void WhereAll_DegenerateTree_1M()
      => Enumerable
      .Range(0, 1_000_000)
      .ToDegenerateTree()
      .Where(_ => true)
      .Consume(TreeTraversalStrategy.DepthFirst);

    [Benchmark]
    public void WhereNone_DegenerateTree_1M()
      => Enumerable
      .Range(0, 1_000_000)
      .ToDegenerateTree()
      .Where(_ => false)
      .Consume(TreeTraversalStrategy.DepthFirst);
  }
}
