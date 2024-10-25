using Arborist.Core;
using System;

namespace Arborist.Linq.Experimental.Treenumerators.ExpandNodes
{
  internal class ExpandNodesDepthFirstTreenumerator<TSource, TExpandedNode, TResult>
    : TreenumeratorBase<TResult>
  {
    public ExpandNodesDepthFirstTreenumerator(
      Func<ITreenumerator<TSource>> innerTreenumeratorFactory,
      Func<NodeContext<TSource>, bool> predicate,
      Func<NodeContext<TSource>, ITreenumerable<TExpandedNode>> nodeExpander,
      Func<NodeContext<TSource>, NodeContext<TExpandedNode>, TResult> selector)
    {
      _InnerTreenumerator = innerTreenumeratorFactory();
      _Predicate = predicate;
      _NodeExpander = nodeExpander;
      _Selector = selector;
    }

    private readonly ITreenumerator<TSource> _InnerTreenumerator;
    private readonly Func<NodeContext<TSource>, bool> _Predicate;
    private readonly Func<NodeContext<TSource>, ITreenumerable<TExpandedNode>> _NodeExpander;
    private readonly Func<NodeContext<TSource>, NodeContext<TExpandedNode>, TResult> _Selector;

    private ITreenumerable<TExpandedNode> _ExpandedNode;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (!_InnerTreenumerator.MoveNext(nodeTraversalStrategies))
        return false;

      throw new NotImplementedException();
    }

    public override void Dispose()
    {
      _InnerTreenumerator?.Dispose();
    }
  }
}
