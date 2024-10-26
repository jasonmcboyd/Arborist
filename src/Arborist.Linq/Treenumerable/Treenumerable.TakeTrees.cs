using Arborist.Core;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TNode> TakeTrees<TNode>(
      this ITreenumerable<TNode> source,
      int count)
      => source.TakeNodesUntil(
        visit => visit.Position.Depth == 0 && visit.Position.SiblingIndex == count,
        false);
  }
}
