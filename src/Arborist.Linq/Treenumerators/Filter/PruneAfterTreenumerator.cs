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

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (EnumerationFinished)
        return false;

      if (Mode == TreenumeratorMode.SchedulingNode)
        nodeTraversalStrategies = GetTraversalStrategy(nodeTraversalStrategies);

      var result = InnerTreenumerator.MoveNext(nodeTraversalStrategies);

      UpdateState();

      return result;
    }

    private NodeTraversalStrategies GetTraversalStrategy(NodeTraversalStrategies nodeTraversalStrategies)
    {
      var skippingNode =
        nodeTraversalStrategies == NodeTraversalStrategies.SkipNodeAndDescendants
        || nodeTraversalStrategies == NodeTraversalStrategies.SkipNode;

      var skippingDescendants =
        _Predicate(this.ToNodeContext())
        || nodeTraversalStrategies == NodeTraversalStrategies.SkipDescendants
        || nodeTraversalStrategies == NodeTraversalStrategies.SkipNodeAndDescendants;

      if (skippingNode && skippingDescendants)
        return NodeTraversalStrategies.SkipNodeAndDescendants;
      else if (skippingNode && !skippingDescendants)
        return NodeTraversalStrategies.SkipNode;
      else if (!skippingNode && skippingDescendants)
        return NodeTraversalStrategies.SkipDescendants;
      else
        return NodeTraversalStrategies.TraverseAll;
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
