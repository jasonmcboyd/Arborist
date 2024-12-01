using Arborist.Core;
using Arborist.Linq;
using Arborist.Trees;
using BenchmarkDotNet.Attributes;

namespace Arborist.Benchmarks
{
  [MemoryDiagnoser]
  [ShortRunJob]
  public class AnyNodes
  {
    private void AnyNodesTest(
      TreeTraversalStrategy treeTraversalStrategy,
      TreeShape treeShape,
      int cutoff)
    {
      Treenumerables
      .GetTree(cutoff, treeShape)
      .AnyNodes(nodeContext => nodeContext.Node == -1, treeTraversalStrategy);
    }

    [Benchmark]
    public void Bft_DeepTree() =>
      AnyNodesTest(TreeTraversalStrategy.BreadthFirst, TreeShape.Deep, 19);

    [Benchmark]
    public void Bft_CompleteBinaryTree_PruneBefore_19() =>
      new CompleteBinaryTree()
      .PruneBefore(nodeContext => nodeContext.Position.Depth == 19)
      .AnyNodes(nodeContext => nodeContext.Node == -1, TreeTraversalStrategy.BreadthFirst);

    [Benchmark]
    public void Dft_DeepTree() =>
      AnyNodesTest(TreeTraversalStrategy.DepthFirst, TreeShape.Deep, 19);

    [Benchmark]
    public void Dft_WideTree() =>
      AnyNodesTest(TreeTraversalStrategy.DepthFirst, TreeShape.Wide, 19);
  }
}
