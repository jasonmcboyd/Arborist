using Copse.Core;

namespace Copse.Linq
{
  public static partial class Treenumerable
  {
    public static void Consume<TNode>(
      this ITreenumerable<TNode> source,
      TreeTraversalStrategy treeTraversalStrategy = default)
    {
      using (var treenumerator = source.GetTreenumerator(treeTraversalStrategy))
        while (treenumerator.MoveNext(NodeTraversalStrategies.TraverseAll));
    }
  }
}
