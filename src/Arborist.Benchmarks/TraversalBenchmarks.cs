using Arborist.Core;
using Arborist.Linq;
using BenchmarkDotNet.Attributes;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  public class TraversalBenchmarks
  {
    #region LevelOrderTraversal

    [Benchmark]
    public void LevelOrderTraversalDepth18() =>
      Trees
      .GetWideTree(18)
      .LevelOrderTraversal()
      .Consume();

    [Benchmark]
    public void LevelOrderTraversalDepth19() =>
      Trees
      .GetWideTree(19)
      .LevelOrderTraversal()
      .Consume();

    #endregion LevelOrderTraversal

    #region PreOrderTraversal

    [Benchmark]
    public void PreOrderTraversalDepth18() =>
      Trees
      .GetWideTree(18)
      .PreOrderTraversal()
      .Consume();

    [Benchmark]
    public void PreOrderTraversalDepth19() =>
      Trees
      .GetWideTree(19)
      .PreOrderTraversal()
      .Consume();

    #endregion PreOrderTraversal

    #region Where

    private ITreenumerable<int> GetTreeWithDepthWhere(int depth) =>
      Trees
      .GetWideTree(depth)
      .PruneAfter(nodeContext => nodeContext.Position.Depth == depth)
      .Where(nodeContext => nodeContext.Position.Depth % 2 == 1);

    [Benchmark]
    public void WhereBftDepth18() =>
      GetTreeWithDepthWhere(18)
      .Consume(TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void WhereBftDepth19() =>
      GetTreeWithDepthWhere(19)
      .Consume(TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void WhereDftDepth18() =>
      GetTreeWithDepthWhere(18)
      .Consume(TreeTraversalStrategy.DepthFirst);

    [Benchmark]
    public void WhereDftDepth19() =>
      GetTreeWithDepthWhere(19)
      .Consume(TreeTraversalStrategy.DepthFirst);

    #endregion Where

    #region GetLeaves

    [Benchmark]
    public void GetLeaves() =>
      Trees
      .GetWideTree(19)
      .GetLeaves()
      .Consume();

    #endregion GetLeaves

    #region AnyNodes

    [Benchmark]
    public bool AnyNodesBft() =>
      Trees
      .GetWideTree(19)
      .AnyNodes(
        nodeContext => nodeContext.Node == -1,
        TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public bool AnyNodesDft() =>
      Trees
      .GetWideTree(19)
      .AnyNodes(
        nodeContext => nodeContext.Node == -1,
        TreeTraversalStrategy.DepthFirst);

    #endregion AnyNodes

    #region AllNodes

    [Benchmark]
    public bool AllNodesBft() =>
      Trees
      .GetWideTree(19)
      .AllNodes(
        nodeContext => nodeContext.Node > -1,
        TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public bool AllNodesDft() =>
      Trees
      .GetWideTree(19)
      .AllNodes(
        nodeContext => nodeContext.Node > -1,
        TreeTraversalStrategy.DepthFirst);

    #endregion AllNodes

    #region PruneBefore

    [Benchmark]
    public void PruneBeforeBft() =>
      Enumerable
      .Range(0, 1_000_000)
      .ToForest()
      .PruneBefore(_ => true)
      .Consume(TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void PruneBeforeDft() =>
      Enumerable
      .Range(0, 1_000_000)
      .ToForest()
      .PruneBefore(_ => true)
      .Consume(TreeTraversalStrategy.DepthFirst);

    #endregion PruneBefore

    #region PruneAfter

    [Benchmark]
    public void PruneAfterBft() =>
      Enumerable
      .Range(0, 1_000_000)
      .ToForest()
      .PruneAfter(_ => true)
      .Consume(TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void PruneAfterDft() =>
      Enumerable
      .Range(0, 1_000_000)
      .ToForest()
      .PruneAfter(_ => true)
      .Consume(TreeTraversalStrategy.DepthFirst);

    #endregion PruneAfter

    #region Select

    [Benchmark]
    public int SelectComposition() =>
      Enumerable
      .Range(0, 1_000_000)
      .ToForest()
      .Select(x => x.Node * 2)
      .Select(x => x.Node + 'a')
      .Select(x => x.Node + 1)
      .Select(x => (char)x.Node)
      .PreOrderTraversal()
      .Count();

    #endregion Select
  }
}
