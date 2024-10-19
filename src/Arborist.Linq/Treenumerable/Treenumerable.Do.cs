using Arborist.Core;
using Arborist.Linq.Treenumerators;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TNode> Do<TNode>(
      this ITreenumerable<TNode> source,
      Action<NodeVisit<TNode>> onNext)
    {
      if (onNext == null)
        return source;

      return
        TreenumerableFactory
        .Create(
          () => new DoTreenumerator<TNode>(source.GetBreadthFirstTreenumerator, onNext),
          () => new DoTreenumerator<TNode>(source.GetDepthFirstTreenumerator, onNext));
    }
  }
}
