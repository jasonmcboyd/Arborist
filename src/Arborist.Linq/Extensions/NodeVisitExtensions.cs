﻿using Arborist.Core;

namespace Arborist.Linq.Extensions
{
  public static class NodeVisitExtensions
  {
    public static NodeVisit<TNode> IncrementVisitCount<TNode>(this NodeVisit<TNode> visit)
    {
      return
        new NodeVisit<TNode>(
          visit.Mode,
          visit.Node,
          visit.VisitCount + 1,
          visit.Position);
    }

    public static NodeVisit<TResult> WithNode<TSource, TResult>(
      this NodeVisit<TSource> visit,
      TResult node)
    {
      return
        new NodeVisit<TResult>(
          visit.Mode,
          node,
          visit.VisitCount,
          visit.Position);
    }

    public static NodeContext<TNode> ToNodeContext<TNode>(this NodeVisit<TNode> visit)
    {
      return
        new NodeContext<TNode>(
          visit.Node,
          visit.Position);
    }
  }
}
