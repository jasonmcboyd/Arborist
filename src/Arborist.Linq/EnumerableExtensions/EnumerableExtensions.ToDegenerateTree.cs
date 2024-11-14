using Arborist.Core;
using Arborist.Linq.Treenumerators.Enumerator;
using System.Collections.Generic;

namespace Arborist.Linq
{
  public static partial class EnumerableExtensions
  {
    public static ITreenumerable<TNode> ToDegenerateTree<TNode>(this IEnumerable<TNode> source)
    {
      return
        TreenumerableFactory
        .Create(
          () => new EnumerableAsTreeBreadthFirstTreenumerator<TNode>(source),
          () => new EnumerableAsTreeDepthFirstTreenumerator<TNode>(source));
    }
  }
}
