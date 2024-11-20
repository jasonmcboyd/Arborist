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
  public class DepthFirstWhere
  {
    [Benchmark]
    public void TriangleTree()
      => new TriangleTree()
      .PruneAfter(nodeContext => nodeContext.Position.Depth == 1448)
      .Where(nodeContext => (nodeContext.Position.Depth + nodeContext.Position.SiblingIndex) % 2 == 0)
      .Consume(TreeTraversalStrategy.DepthFirst);

    [Benchmark]
    public void TrivialForest_WhereAll()
      => Enumerable
      .Range(0, 1 << 20)
      .ToTrivialForest()
      .Where(_ => true)
      .Consume(TreeTraversalStrategy.DepthFirst);

    [Benchmark]
    public void TrivialForest_WhereNone()
      => Enumerable
      .Range(0, 1 << 20)
      .ToTrivialForest()
      .Where(_ => false)
      .Consume(TreeTraversalStrategy.DepthFirst);

    [Benchmark]
    public void ToDegenerateTree_WhereAll()
      => Enumerable
      .Range(0, 1 << 20)
      .ToDegenerateTree()
      .Where(_ => true)
      .Consume(TreeTraversalStrategy.DepthFirst);

    [Benchmark]
    public void ToDegenerateTree_WhereNone()
      => Enumerable
      .Range(0, 1 << 20)
      .ToDegenerateTree()
      .Where(_ => false)
      .Consume(TreeTraversalStrategy.DepthFirst);
  }
}
