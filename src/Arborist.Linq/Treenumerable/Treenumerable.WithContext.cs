using Arborist.Core;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    // Surfaces each node's NodeContext (value + position) as the node value -- i.e. promotes
    // the structural context into the value stream. (A typed identity Select; the name that
    // used to be "Materialize" is now reserved for snapshotting a tree into a concrete structure.)
    public static ITreenumerable<NodeContext<TNode>> WithContext<TNode>(this ITreenumerable<TNode> source)
      => source.Select(nodeContext => nodeContext);
  }
}
