using Arborist.Core;

namespace Arborist.Linq
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
