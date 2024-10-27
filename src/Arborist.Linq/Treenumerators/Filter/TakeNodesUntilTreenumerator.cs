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
    private bool _FinalVisitRemaining = false;

    protected override bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (EnumerationFinished)
        return false;

      if (_StopSchedulingNodes)
        return OnSchedulingStopped(nodeTraversalStrategies);

      if (!InnerTreenumerator.MoveNext(nodeTraversalStrategies))
        return false;

      if (InnerTreenumerator.Mode == TreenumeratorMode.VisitingNode)
      {
        UpdateState();
        return true;
      }

      if (_Predicate(InnerTreenumerator.ToNodeContext()))
      {
        _StopSchedulingNodes = true;

        if (_KeepFinalNode)
          _FinalVisitRemaining = true;
        else
          return OnSchedulingStopped(nodeTraversalStrategies);
      }

      UpdateState();

      return true;
    }

    private bool OnSchedulingStopped(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (_FinalVisitRemaining == true)
      {
        _FinalVisitRemaining = false;
        nodeTraversalStrategies |= NodeTraversalStrategies.SkipDescendantsAndSiblings;
      }
      else
      {
        nodeTraversalStrategies = NodeTraversalStrategies.SkipAll;
      }

      while (true)
      {
        var result = InnerTreenumerator.MoveNext(nodeTraversalStrategies);

        if (!result)
          return false;

        if (InnerTreenumerator.Mode == TreenumeratorMode.VisitingNode
          && (InnerTreenumerator.VisitCount < 2
          || InnerTreenumerator.Position != Position
          || InnerTreenumerator.VisitCount != VisitCount + 1))
        {
          UpdateState();
          return true;
        }

        nodeTraversalStrategies = NodeTraversalStrategies.SkipAll;
      }
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
