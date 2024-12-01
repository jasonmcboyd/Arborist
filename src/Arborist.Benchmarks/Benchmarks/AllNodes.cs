using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;
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
      .AllNodes(nodeContext => nodeContext.Node == -1, treeTraversalStrategy);
    }

    [Benchmark]
    public void Bft_DeepTree() =>
      AllNodesTest(TreeTraversalStrategy.BreadthFirst, TreeShape.Deep, 19);

    [Benchmark]
    public void Bft_CompleteBinaryTree_19() =>
      new CompleteBinaryTree()
      .PruneBefore(nodeContext => nodeContext.Position.Depth == 19)
      .AllNodes(nodeContext => nodeContext.Node == -1, TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void Dft_DeepTree() =>
      AllNodesTest(TreeTraversalStrategy.DepthFirst, TreeShape.Deep, 19);

    [Benchmark]
    public void Dft_WideTree() =>
      new CompleteBinaryTree()
      .PruneBefore(nodeContext => nodeContext.Position.Depth == 19)
      .AllNodes(nodeContext => nodeContext.Node == -1, TreeTraversalStrategy.DepthFirst);

    #endregion AllNodes
  }
}
