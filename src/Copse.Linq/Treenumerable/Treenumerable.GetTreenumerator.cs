using Copse.Core;

namespace Copse.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerator<TNode> GetTreenumerator<TNode>(
      this ITreenumerable<TNode> source,
      TreeTraversalStrategy treeTraversalStrategy)
    {
      return
        treeTraversalStrategy == TreeTraversalStrategy.BreadthFirst
        ? source.GetBreadthFirstTreenumerator()
        : source.GetDepthFirstTreenumerator();
    }
  }
}
