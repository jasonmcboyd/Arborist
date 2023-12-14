using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TNode> Do<TNode>(
      this ITreenumerable<TNode> source,
      Action<NodeVisit<TNode>> onNext)
    {
      return
        source.Select(step =>
        {
          onNext(step);
          return step.Node;
        });
    }
  }
}
