using Arborist.Core;
using Arborist.Linq.Extensions;
using System;

namespace Arborist.Linq.Treenumerators
{
  internal class PruneAfterTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public PruneAfterTreenumerator(
      Func<ITreenumerator<TNode>> innerTreenumeratorFactory,
      Func<NodeContext<TNode>, bool> predicate)
      : base(innerTreenumeratorFactory)
    {
      _Predicate = predicate;
    }

    private readonly Func<NodeContext<TNode>, bool> _Predicate;

    protected override bool OnMoveNext(NodeTraversalStrategy nodeTraversalStrategy)
    {
      if (EnumerationFinished)
        return false;

      if (Mode == TreenumeratorMode.SchedulingNode)
        nodeTraversalStrategy = GetTraversalStrategy(nodeTraversalStrategy);

      var result = InnerTreenumerator.MoveNext(nodeTraversalStrategy);

      UpdateState();

      return result;
    }

    private NodeTraversalStrategy GetTraversalStrategy(NodeTraversalStrategy nodeTraversalStrategy)
    {
      var skippingNode =
        nodeTraversalStrategy == NodeTraversalStrategy.SkipSubtree
        || nodeTraversalStrategy == NodeTraversalStrategy.SkipNode;

      var skippingDescendants =
        _Predicate(this.ToNodeContext())
        || nodeTraversalStrategy == NodeTraversalStrategy.SkipDescendants
        || nodeTraversalStrategy == NodeTraversalStrategy.SkipSubtree;

      if (skippingNode && skippingDescendants)
        return NodeTraversalStrategy.SkipSubtree;
      else if (skippingNode && !skippingDescendants)
        return NodeTraversalStrategy.SkipNode;
      else if (!skippingNode && skippingDescendants)
        return NodeTraversalStrategy.SkipDescendants;
      else
        return NodeTraversalStrategy.TraverseSubtree;
    }

    private void UpdateState()
    {
      Mode = InnerTreenumerator.Mode;

      if (!EnumerationFinished)
      {
        Node = InnerTreenumerator.Node;
        VisitCount = InnerTreenumerator.VisitCount;
        Position = InnerTreenumerator.Position;
      }
    }
  }
}
