using Arborist.Benchmarks.Trees;
using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;
using System.Linq;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [ShortRunJob]
  public partial class TraversalBenchmarks
  {
    //#region RefSemiDeque

    //[Benchmark]
    //public void RefSemiDeque_Add()
    //{
    //  var deque = new RefSemiDeque<int>();

    //  for (int i = 0; i < (1 << 23); i++)
    //    deque.AddLast(i);
    //}

    //[Benchmark]
    //public void RefSemiDeque_RemoveFirst()
    //{
    //  var deque = new RefSemiDeque<int>();

    //  for (int i = 0; i < (1 << 23); i++)
    //    deque.AddLast(i);

    //  while (deque.Count > 0)
    //    deque.RemoveFirst();
    //}

    //[Benchmark]
    //public void RefSemiDeque_RemoveLast()
    //{
    //  var deque = new RefSemiDeque<int>();

    //  for (int i = 0; i < (1 << 23); i++)
    //    deque.AddLast(i);

    //  while (deque.Count > 0)
    //    deque.RemoveLast();
    //}

    //#endregion RefSemiDeque

    //#region Skip All Nodes

    //[Benchmark]
    //public void BreadthFirstTreenumerator_SkipNodes()
    //{
    //  var treenumerable =
    //    new TriangleTree()
    //    .PruneAfter(nodeContext => nodeContext.Position.Depth == (1 << 12));

    //  using (var treenumerator = treenumerable.GetBreadthFirstTreenumerator())
    //  {
    //    while (treenumerator.MoveNext(NodeTraversalStrategies.SkipNode)) ;
    //  }
    //}

    //[Benchmark]
    //public void DepthFirstTreenumerator_SkipNodes()
    //{
    //  var treenumerable =
    //    new TriangleTree()
    //    .PruneAfter(nodeContext => nodeContext.Position.Depth == (1 << 12));

    //  using (var treenumerator = treenumerable.GetDepthFirstTreenumerator())
    //  {
    //    while (treenumerator.MoveNext(NodeTraversalStrategies.SkipNode)) ;
    //  }
    //}

    //#endregion Skip All Nodes

    //#region LevelOrderTraversal

    //[Benchmark]
    //public void LevelOrderTraversal_DeepTree() =>
    //  Treenumerables
    //  .GetDeepTree(20)
    //  .LevelOrderTraversal()
    //  .Consume();

    //[Benchmark]
    //public void LevelOrderTraversal_TriangleTree() =>
    //  new TriangleTree()
    //  .PruneAfter(nodeContext => nodeContext.Position.Depth == 1447)
    //  .LevelOrderTraversal()
    //  .Consume();

    //[Benchmark]
    //public void LevelOrderTraversal_WideTree_PruneBeforeDepth_20() =>
    //  new CompleteBinaryTree()
    //  .PruneBefore(nodeContext => nodeContext.Position.Depth == 20)
    //  .LevelOrderTraversal()
    //  .Consume();

    //[Benchmark]
    //public void LevelOrderTraversal_WideTree_PruneAfterDepth_19() =>
    //  new CompleteBinaryTree()
    //  .PruneAfter(nodeContext => nodeContext.Position.Depth == 19)
    //  .LevelOrderTraversal()
    //  .Consume();

    //#endregion LevelOrderTraversal

    //#region PreOrderTraversal

    //[Benchmark]
    //public void PreOrderTraversal_DeepTree() =>
    //  Treenumerables
    //  .GetDeepTree(20)
    //  .PreOrderTraversal()
    //  .Consume();

    //[Benchmark]
    //public void PreOrderTraversal_TriangleTree() =>
    //  new TriangleTree()
    //  .PruneAfter(nodeContext => nodeContext.Position.Depth == 1447)
    //  .PreOrderTraversal()
    //  .Consume();

    //[Benchmark]
    //public void PreOrderTraversal_WideTree_PruneBeforeDepth_20() =>
    //  new CompleteBinaryTree()
    //  .PruneBefore(nodeContext => nodeContext.Position.Depth == 20)
    //  .PreOrderTraversal()
    //  .Consume();

    //[Benchmark]
    //public void PreOrderTraversal_WideTree_PruneAfterDepth_19() =>
    //  new CompleteBinaryTree()
    //  .PruneAfter(nodeContext => nodeContext.Position.Depth == 19)
    //  .PreOrderTraversal()
    //  .Consume();

    //#endregion PreOrderTraversal

    //#region PostOrderTraversal

    //[Benchmark]
    //public void PostOrderTraversal_DeepTree() =>
    //  Treenumerables
    //  .GetDeepTree(20)
    //  .PostOrderTraversal()
    //  .Consume();

    //[Benchmark]
    //public void PostOrderTraversal_TriangleTree() =>
    //  new TriangleTree()
    //  .PruneAfter(nodeContext => nodeContext.Position.Depth == 1447)
    //  .PostOrderTraversal()
    //  .Consume();

    //[Benchmark]
    //public void PostOrderTraversal_WideTree_PruneBeforeDepth_20() =>
    //  new CompleteBinaryTree()
    //  .PruneBefore(nodeContext => nodeContext.Position.Depth == 20)
    //  .PostOrderTraversal()
    //  .Consume();

    //[Benchmark]
    //public void PostOrderTraversal_WideTree_PruneAfterDepth_19() =>
    //  new CompleteBinaryTree()
    //  .PruneAfter(nodeContext => nodeContext.Position.Depth == 19)
    //  .PostOrderTraversal()
    //  .Consume();

    //#endregion PostOrderTraversal

    #region Where

    private ITreenumerable<int> GetTreeWithDepthWhere(int cutoff, TreeShape treeShape)
    {
      return
        Treenumerables
        .GetTree(cutoff, treeShape)
        .Where(nodeContext => nodeContext.Position.Depth % 2 == 1);
    }

    private void WhereTest(
      TreeTraversalStrategy treeTraversalStrategy,
      TreeShape treeShape,
      int depth)
      => GetTreeWithDepthWhere(depth, treeShape).Consume(treeTraversalStrategy);

    [Benchmark]
    public void Where_Bft_DeepTree()
      => WhereTest(TreeTraversalStrategy.BreadthFirst, TreeShape.Deep, 19);

    [Benchmark]
    public void Where_Bft_WideTree()
      => WhereTest(TreeTraversalStrategy.BreadthFirst, TreeShape.Wide, 19);

    [Benchmark]
    public void Where_Dft_TriangleTree()
      => new TriangleTree()
      .PruneAfter(nodeContext => nodeContext.Position.Depth == 1448)
      .Where(nodeContext => (nodeContext.Position.Depth + nodeContext.Position.SiblingIndex) % 2 == 0 )
      .Consume();

    [Benchmark]
    public void Where_Dft_TrivialForest_WhereAll()
      => Enumerable.Range(0, 1 << 20).ToTrivialForest().Where(_ => true).Consume();

    [Benchmark]
    public void Where_Dft_TrivialForest_WhereNone()
      => Enumerable.Range(0, 1 << 20).ToTrivialForest().Where(_ => false).Consume();

    [Benchmark]
    public void Where_Dft_ToDegenerateTree_WhereAll()
      => Enumerable.Range(0, 1 << 20).ToDegenerateTree().Where(_ => true).Consume();

    [Benchmark]
    public void Where_Dft_ToDegenerateTree_WhereNone()
      => Enumerable.Range(0, 1 << 20).ToDegenerateTree().Where(_ => false).Consume();

    #endregion Where

    //#region GetLeaves

    //[Benchmark]
    //public void GetLeaves_DeepTree() =>
    //  Treenumerables
    //  .GetDeepTree(19)
    //  .GetLeaves()
    //  .Consume();

    //[Benchmark]
    //public void GetLeaves_WideTree() =>
    //  Treenumerables
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
    //  Treenumerables
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
    //  Treenumerables
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
