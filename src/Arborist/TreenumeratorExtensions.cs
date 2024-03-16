using Arborist.Core;

namespace Arborist
{
  public static class TreenumeratorExtensions
  {
    public static bool MoveNext<TNode>(this ITreenumerator<TNode> treenumerator)
      => treenumerator.MoveNext(SchedulingStrategy.TraverseSubtree);
  }
}
