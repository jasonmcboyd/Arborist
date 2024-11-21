using Arborist.Core;
using Arborist.Linq;
using BenchmarkDotNet.Attributes;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [ShortRunJob]
  public class AllNodes
  {
    #region AllNodes

    private void AllNodesTest(
      TreeTraversalStrategy treeTraversalStrategy,
      TreeShape treeShape,
      int cutoff)
    {
      Treenumerables
      .GetTree(cutoff, treeShape)
      .AnyNodes(nodeContext => nodeContext.Node == -1, treeTraversalStrategy);
    }

    [Benchmark]
    public void AllNodes_Bft_DeepTree()
      => AllNodesTest(TreeTraversalStrategy.BreadthFirst, TreeShape.Deep, 19);

    [Benchmark]
    public void AllNodes_Bft_WideTree()
      => AllNodesTest(TreeTraversalStrategy.BreadthFirst, TreeShape.Wide, 19);

    [Benchmark]
    public void AllNodes_Dft_DeepTree()
      => AllNodesTest(TreeTraversalStrategy.DepthFirst, TreeShape.Deep, 19);

    [Benchmark]
    public void AllNodes_Dft_WideTree()
      => AllNodesTest(TreeTraversalStrategy.DepthFirst, TreeShape.Wide, 19);

    #endregion AllNodes

    //#region PruneBefore

    //[Benchmark]
    //public void PruneBeforeBft() =>
    //  Enumerable
    //  .Range(0, 1_000_000)
    //  .ToTrivialForest()
    //  .PruneBefore(_ => true)
    //  .Consume(TreeTraversalStrategy.BreadthFirst);

    //[Benchmark]
    //public void PruneBeforeDft() =>
    //  Enumerable
    //  .Range(0, 1_000_000)
    //  .ToTrivialForest()
    //  .PruneBefore(_ => true)
    //  .Consume(TreeTraversalStrategy.DepthFirst);

    //#endregion PruneBefore

    //#region PruneAfter

    //[Benchmark]
    //public void PruneAfterBft() =>
    //  Enumerable
    //  .Range(0, 1_000_000)
    //  .ToTrivialForest()
    //  .PruneAfter(_ => true)
    //  .Consume(TreeTraversalStrategy.BreadthFirst);

    //[Benchmark]
    //public void PruneAfterDft() =>
    //  Enumerable
    //  .Range(0, 1_000_000)
    //  .ToTrivialForest()
    //  .PruneAfter(_ => true)
    //  .Consume(TreeTraversalStrategy.DepthFirst);

    //#endregion PruneAfter

    //#region Select

    //[Benchmark]
    //public int SelectComposition() =>
    //  Enumerable
    //  .Range(0, 1_000_000)
    //  .ToTrivialForest()
    //  .Select(x => x.Node * 2)
    //  .Select(x => x.Node + 'a')
    //  .Select(x => x.Node + 1)
    //  .Select(x => (char)x.Node)
    //  .PreOrderTraversal()
    //  .Count();

    //#endregion Select

    //#region Count Nodes

    //[Benchmark]
    //public int CountNodes_DeepTree() =>
    //  Treenumerables
    //  .GetDeepTree(20)
    //  .CountNodes();

    //[Benchmark]
    //public int CountNodes_TriangleTree() =>
    //  new TriangleTree()
    //  .PruneAfter(nodeContext => nodeContext.Position.Depth == 2048)
    //  .CountNodes();

    //[Benchmark]
    //public int CountNodes_WideTree() =>
    //  new CompleteBinaryTree()
    //  .PruneAfter(nodeContext => nodeContext.Position.Depth == 20)
    //  .CountNodes();

    //#endregion

    //#region Enumerable To Tree

    //[Benchmark]
    //public int ToDegenerateTree() =>
    //  Enumerable.Range(0, 1 << 20)
    //  .ToDegenerateTree()
    //  .CountNodes();

    //[Benchmark]
    //public int ToDegenerateTreeUsingToTree() =>
    //  Enumerable.Range(0, 1 << 20)
    //  .ToTree(_ => true)
    //  .CountNodes();

    //[Benchmark]
    //public int ToTrivialForest() =>
    //  Enumerable.Range(0, 1 << 20)
    //  .ToTrivialForest()
    //  .CountNodes();

    //[Benchmark]
    //public int ToTrivialForestUsingToTree() =>
    //  Enumerable.Range(0, 1 << 20)
    //  .ToTree(_ => false)
    //  .CountNodes();

    //#endregion Enumerable To Tree
  }
}
