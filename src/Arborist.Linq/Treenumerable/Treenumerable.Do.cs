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
      => TreenumerableFactory.Create(
          source,
          breadthFirstEnumerator => new DoTreenumerator<TNode>(breadthFirstEnumerator, onNext),
          depthFirstEnumerator => new DoTreenumerator<TNode>(depthFirstEnumerator, onNext));
  }
}
