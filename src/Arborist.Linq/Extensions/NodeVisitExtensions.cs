﻿using System.ComponentModel;

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
          visit.OriginalPosition,
          visit.Position,
          visit.SchedulingStrategy);
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
          visit.OriginalPosition,
          visit.Position,
          visit.SchedulingStrategy);
    }
  }
}
