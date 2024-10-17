using Arborist.Core;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    // TODO: I don't think I like the name of this method.
    public static ITreenumerable<NodeContext<TNode>> Materialize<TNode>(this ITreenumerable<TNode> source)
      => source.Select(nodeContext => nodeContext);
  }
}
