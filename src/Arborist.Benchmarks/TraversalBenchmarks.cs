using Arborist.Benchmarks.Trees;
using Arborist.Core;
using Arborist.Linq;
using BenchmarkDotNet.Attributes;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [ShortRunJob]
  public partial class TraversalBenchmarks
  {
    #region LevelOrderTraversal

    [Benchmark]
    public void LevelOrderTraversal_DeepTree() =>
      Treenumerables
      .GetDeepTree(20)
      .LevelOrderTraversal()
      .Consume();

    [Benchmark]
    public void LevelOrderTraversal_TriangleTree() =>
      new TriangleTree()
      .PruneAfter(nodeContext => nodeContext.Position.Depth == 1447)
      .LevelOrderTraversal()
      .Consume();

    [Benchmark]
    public void LevelOrderTraversal_WideTree() =>
      Treenumerables
      .GetWideTree(20)
      .LevelOrderTraversal()
      .Consume();

    #endregion LevelOrderTraversal

    #region PreOrderTraversal

    [Benchmark]
    public void PreOrderTraversal_DeepTree() =>
      Treenumerables
      .GetDeepTree(20)
      .PreOrderTraversal()
      .Consume();

    [Benchmark]
    public void PreOrderTraversal_TriangleTree() =>
      new TriangleTree()
      .PruneAfter(nodeContext => nodeContext.Position.Depth == 1447)
      .PreOrderTraversal()
      .Consume();
    [Benchmark]

    public void PreOrderTraversal_WideTree() =>
      Treenumerables
      .GetWideTree(20)
      .PreOrderTraversal()
      .Consume();

    #endregion PreOrderTraversal

    //#region Where

    //private ITreenumerable<int> GetTreeWithDepthWhere(int cutoff, TreeShape treeShape)
    //{
    //  return
    //    Trees
    //    .GetTree(cutoff, treeShape)
    //    .Where(nodeContext => nodeContext.Position.Depth % 2 == 1);
    //}

    //private void WhereTest(
    //  TreeTraversalStrategy treeTraversalStrategy,
    //  TreeShape treeShape,
    //  int depth)
    //  => GetTreeWithDepthWhere(depth, treeShape).Consume(treeTraversalStrategy);

    //[Benchmark]
    //public void Where_Bft_DeepTree()
    //  => WhereTest(TreeTraversalStrategy.BreadthFirst, TreeShape.Deep, 19);

    //[Benchmark]
    //public void Where_Bft_WideTree()
    //  => WhereTest(TreeTraversalStrategy.BreadthFirst, TreeShape.Wide, 19);

    //[Benchmark]
    //public void Where_Dft_DeepTree()
    //  => WhereTest(TreeTraversalStrategy.DepthFirst, TreeShape.Deep, 19);

    //[Benchmark]
    //public void Where_Dft_WideTree()
    //  => WhereTest(TreeTraversalStrategy.DepthFirst, TreeShape.Wide, 19);

    //#endregion Where

    //#region GetLeaves

    //[Benchmark]
    //public void GetLeaves_DeepTree() =>
    //  Trees
    //  .GetDeepTree(19)
    //  .GetLeaves()
    //  .Consume();

    //[Benchmark]
    //public void GetLeaves_WideTree() =>
    //  Trees
    //  .GetWideTree(19)
    //  .GetLeaves()
    //  .Consume();

    //#endregion GetLeaves

    //#region AnyNodes

    //private void AnyNodesTest(
    //  TreeTraversalStrategy treeTraversalStrategy,
    //  TreeShape treeShape,
    //  int cutoff)
    //{
    //  Trees
    //  .GetTree(cutoff, treeShape)
    //  .AnyNodes(nodeContext => nodeContext.Node == -1, treeTraversalStrategy);
    //}

    //[Benchmark]
    //public void AnyNodes_Bft_DeepTree()
    //  => AnyNodesTest(TreeTraversalStrategy.BreadthFirst, TreeShape.Deep, 19);

    //[Benchmark]
    //public void AnyNodes_Bft_WideTree()
    //  => AnyNodesTest(TreeTraversalStrategy.BreadthFirst, TreeShape.Wide, 19);

    //[Benchmark]
    //public void AnyNodes_Dft_DeepTree()
    //  => AnyNodesTest(TreeTraversalStrategy.DepthFirst, TreeShape.Deep, 19);

    //[Benchmark]
    //public void AnyNodes_Dft_WideTree()
    //  => AnyNodesTest(TreeTraversalStrategy.DepthFirst, TreeShape.Wide, 19);

    //#endregion AnyNodes

    //#region AllNodes

    //private void AllNodesTest(
    //  TreeTraversalStrategy treeTraversalStrategy,
    //  TreeShape treeShape,
    //  int cutoff)
    //{
    //  Trees
    //  .GetTree(cutoff, treeShape)
    //  .AnyNodes(nodeContext => nodeContext.Node == -1, treeTraversalStrategy);
    //}

    //[Benchmark]
    //public void AllNodes_Bft_DeepTree()
    //  => AllNodesTest(TreeTraversalStrategy.BreadthFirst, TreeShape.Deep, 19);

    //[Benchmark]
    //public void AllNodes_Bft_WideTree()
    //  => AllNodesTest(TreeTraversalStrategy.BreadthFirst, TreeShape.Wide, 19);

    //[Benchmark]
    //public void AllNodes_Dft_DeepTree()
    //  => AllNodesTest(TreeTraversalStrategy.DepthFirst, TreeShape.Deep, 19);

    //[Benchmark]
    //public void AllNodes_Dft_WideTree()
    //  => AllNodesTest(TreeTraversalStrategy.DepthFirst, TreeShape.Wide, 19);

    //#endregion AllNodes

    //#region PruneBefore

    //[Benchmark]
    //public void PruneBeforeBft() =>
    //  Enumerable
    //  .Range(0, 1_000_000)
    //  .ToForest()
    //  .PruneBefore(_ => true)
    //  .Consume(TreeTraversalStrategy.BreadthFirst);

    //[Benchmark]
    //public void PruneBeforeDft() =>
    //  Enumerable
    //  .Range(0, 1_000_000)
    //  .ToForest()
    //  .PruneBefore(_ => true)
    //  .Consume(TreeTraversalStrategy.DepthFirst);

    //#endregion PruneBefore

    //#region PruneAfter

    //[Benchmark]
    //public void PruneAfterBft() =>
    //  Enumerable
    //  .Range(0, 1_000_000)
    //  .ToForest()
    //  .PruneAfter(_ => true)
    //  .Consume(TreeTraversalStrategy.BreadthFirst);

    //[Benchmark]
    //public void PruneAfterDft() =>
    //  Enumerable
    //  .Range(0, 1_000_000)
    //  .ToForest()
    //  .PruneAfter(_ => true)
    //  .Consume(TreeTraversalStrategy.DepthFirst);

    //#endregion PruneAfter

    //#region Select

    //[Benchmark]
    //public int SelectComposition() =>
    //  Enumerable
    //  .Range(0, 1_000_000)
    //  .ToForest()
    //  .Select(x => x.Node * 2)
    //  .Select(x => x.Node + 'a')
    //  .Select(x => x.Node + 1)
    //  .Select(x => (char)x.Node)
    //  .PreOrderTraversal()
    //  .Count();

    //#endregion Select
  }
}
