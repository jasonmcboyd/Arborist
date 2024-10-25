using Arborist.Core;
using Arborist.Linq.Extensions;
using System;
using System.Collections.Generic;

namespace Arborist.Linq
{
  internal static class LeaffixAggregator
  {
    public static IEnumerable<TAccumulate> Aggregate<TSource, TAccumulate>(
      ITreenumerable<TSource> source,
      Func<NodeContext<TSource>, TAccumulate> leafSelector,
      Func<NodeContext<TSource>, TAccumulate[], TAccumulate> accumulator)
    {
      var nodeStack = new RefSemiDeque<RefSemiDeque<NodeContextAndResult<TSource, TAccumulate>>>();

      nodeStack.AddLast(new RefSemiDeque<NodeContextAndResult<TSource, TAccumulate>>());

      var stackPool = new RefSemiDeque<RefSemiDeque<NodeContextAndResult<TSource, TAccumulate>>>();

      using (var treenumerator = source.GetDepthFirstTreenumerator())
      {
        while (treenumerator.MoveNext(NodeTraversalStrategies.TraverseAll))
        {
          if (treenumerator.Mode == TreenumeratorMode.VisitingNode)
            continue;

          if (treenumerator.Position.Depth > nodeStack.Count - 1)
          {
            var childStack =
              stackPool.Count == 0
              ? new RefSemiDeque<NodeContextAndResult<TSource, TAccumulate>>()
              : stackPool.GetLast();

            childStack.AddLast(new NodeContextAndResult<TSource, TAccumulate>(treenumerator.ToNodeContext()));

            nodeStack.AddLast(childStack);
          }
          else if (treenumerator.Position.Depth < nodeStack.Count - 1)
          {
            AggregateChildren(nodeStack, stackPool, leafSelector, accumulator);

            if (DequeueRootNode(nodeStack, leafSelector, out var result))
              yield return result;

            nodeStack.GetLast().AddLast(new NodeContextAndResult<TSource, TAccumulate>(treenumerator.ToNodeContext()));
          }
          else
          {
            if (DequeueRootNode(nodeStack, leafSelector, out var result))
              yield return result;

            nodeStack.GetLast().AddLast(new NodeContextAndResult<TSource, TAccumulate>(treenumerator.ToNodeContext()));
          }
        }
      }

      while (nodeStack.Count > 1)
        AggregateChildren(nodeStack, stackPool, leafSelector, accumulator);

      if (DequeueRootNode(nodeStack, leafSelector, out var rootNode))
        yield return rootNode;
    }

    private static bool DequeueRootNode<TNode, TResult>(
      RefSemiDeque<RefSemiDeque<NodeContextAndResult<TNode, TResult>>> nodeStack,
      Func<NodeContext<TNode>, TResult> leafSelector,
      out TResult result)
    {
      if ((nodeStack.Count != 1 || nodeStack.GetFirst().Count == 0))
      {
        result = default;
        return false;
      }

      var nodeContextAndResult = nodeStack.GetFirst().RemoveFirst();

      if (nodeContextAndResult.HasResult)
        result = nodeContextAndResult.Result;
      else
        result = leafSelector(nodeContextAndResult.NodeContext);

      return true;
    }
 
    private static void AggregateChildren<TNode, TResult>(
      RefSemiDeque<RefSemiDeque<NodeContextAndResult<TNode, TResult>>> nodeStack,
      RefSemiDeque<RefSemiDeque<NodeContextAndResult<TNode, TResult>>> stackPool,
      Func<NodeContext<TNode>, TResult> leafSelector,
      Func<NodeContext<TNode>, TResult[], TResult> accumulator)
    {
      var childrenStack = nodeStack.RemoveLast();

      var children = new TResult[childrenStack.Count];

      for (var i = 0; i < children.Length; i++)
      {
        var child = childrenStack.RemoveFirst();

        children[i] = child.HasResult
          ? child.Result
          : leafSelector(child.NodeContext);
      }

      stackPool.AddLast(childrenStack);

      var stack = nodeStack.GetLast();
      ref var nodeContextAndResult = ref stack.GetLast();

      nodeContextAndResult.SetResult(accumulator(nodeContextAndResult.NodeContext, children));
    }

    private struct NodeContextAndResult<TNode, TResult>
    {
      public NodeContextAndResult(NodeContext<TNode> nodeContext)
      {
        NodeContext = nodeContext;
        Result = default;
        HasResult = false;
      }

      public NodeContextAndResult(
        NodeContext<TNode> nodeContext,
        TResult result)
      {
        NodeContext = nodeContext;
        Result = result;
        HasResult = true;
      }

      public void SetResult(TResult result)
      {
        Result = result;
        HasResult = true;
      }

      public NodeContext<TNode> NodeContext { get; set; }
      public TResult Result { get; set; }
      public bool HasResult { get; set; }
    }
  }
}
