using Arborist.Core;
using Arborist.Linq.Extensions;
using System;

namespace Arborist.Linq.Treenumerators
{
  internal class TakeNodesUntilTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public TakeNodesUntilTreenumerator(
      Func<ITreenumerator<TNode>> innerTreenumeratorFactory,
      Func<NodeContext<TNode>, bool> predicate,
      bool keepFinalNode)
      : base(innerTreenumeratorFactory)
    {
      _Predicate = predicate;
      _KeepFinalNode = keepFinalNode;
    }

    private readonly Func<NodeContext<TNode>, bool> _Predicate;
    private bool _KeepFinalNode;
    private bool _StopSchedulingNodes;
    private bool _EnumerationStarted = false;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (EnumerationFinished)
        return false;

      if (!_EnumerationStarted)
      {
        _EnumerationStarted = true;
      }
      else if (Mode == TreenumeratorMode.SchedulingNode)
      {
        if (_StopSchedulingNodes)
        {
          nodeTraversalStrategies = NodeTraversalStrategies.SkipAll;
        }
        else if (_Predicate(this.ToNodeContext()))
        {
          _StopSchedulingNodes = true;

          nodeTraversalStrategies =
            _KeepFinalNode
            ? NodeTraversalStrategies.SkipDescendantsAndSiblings
            : NodeTraversalStrategies.SkipAll;
        }
      }

      var result = InnerTreenumerator.MoveNext(nodeTraversalStrategies);

      UpdateState();

      return result;
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
