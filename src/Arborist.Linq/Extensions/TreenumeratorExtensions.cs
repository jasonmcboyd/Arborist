using Arborist.Core;

namespace Arborist.Linq.Extensions
{
  internal static class TreenumeratorExtensions
  {
    public static NodeVisit<TNode> ToNodeVisit<TNode>(this ITreenumerator<TNode> treenumerator)
    {
      return
        new NodeVisit<TNode>(
          treenumerator.Mode,
          treenumerator.Node,
          treenumerator.VisitCount,
          treenumerator.Position);
    }

    public static NodeContext<TNode> ToNodeContext<TNode>(this ITreenumerator<TNode> treenumerator)
    {
      return
        new NodeContext<TNode>(
          treenumerator.Node,
          treenumerator.Position);
    }
  }
}
