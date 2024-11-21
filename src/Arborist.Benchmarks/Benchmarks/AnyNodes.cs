using Arborist.Core;
using Arborist.Linq;
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
    public void AnyNodes_Bft_DeepTree()
      => AnyNodesTest(TreeTraversalStrategy.BreadthFirst, TreeShape.Deep, 19);

    [Benchmark]
    public void AnyNodes_Bft_WideTree()
      => AnyNodesTest(TreeTraversalStrategy.BreadthFirst, TreeShape.Wide, 19);

    [Benchmark]
    public void AnyNodes_Dft_DeepTree()
      => AnyNodesTest(TreeTraversalStrategy.DepthFirst, TreeShape.Deep, 19);

    [Benchmark]
    public void AnyNodes_Dft_WideTree()
      => AnyNodesTest(TreeTraversalStrategy.DepthFirst, TreeShape.Wide, 19);
  }
}
