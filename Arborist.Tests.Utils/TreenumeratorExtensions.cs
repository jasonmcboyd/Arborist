using System.Collections.Generic;

namespace Arborist.Tests.Utils
{
  public static class TreenumeratorExtensions
  {
    public static IEnumerable<MoveNextResult<TNode>> GetMoveNextResults<TNode>(
      this ITreenumerator<TNode> enumerator,
      params bool[] skipChildren)
      where TNode : struct
    {
      for (int i = 0; i < skipChildren.Length; i++)
      {
        if (enumerator.MoveNext(skipChildren[i]))
        {
          yield return
            new MoveNextResult<TNode>(
              enumerator.Current.Node,
              enumerator.Current.VisitCount,
              enumerator.Current.VisitCount,
              enumerator.Current.Depth);
        }
        else
        {
          yield return new MoveNextResult<TNode>();
          yield break;
        }
      }
    }
  }
}
