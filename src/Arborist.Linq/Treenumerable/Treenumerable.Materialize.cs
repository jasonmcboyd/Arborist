using Arborist.Core;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<NodeContext<TNode>> Materialize<TNode>(this ITreenumerable<TNode> source)
      => source.Select(nodeContext => nodeContext);
  }
}
