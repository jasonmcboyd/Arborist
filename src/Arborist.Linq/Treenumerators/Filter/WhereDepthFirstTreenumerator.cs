using Arborist.Core;
using Arborist.Linq.Extensions;
using System;

namespace Arborist.Linq.Treenumerators
{
  internal class WhereDepthFirstTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public WhereDepthFirstTreenumerator(
      ITreenumerator<TNode> innerTreenumerator,
      Func<NodeVisit<TNode>, bool> predicate)
      : base(innerTreenumerator)
    {
      _Predicate = predicate;
    }

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;

    protected override bool OnMoveNext(TraversalStrategy traversalStrategy)
    {
      if (Mode == TreenumeratorMode.EnumerationFinished)
        return false;

      if (Mode != TreenumeratorMode.SchedulingNode)
        return OnVisitingNode();

      return OnSchedulingNode(traversalStrategy);
    }

    private bool OnVisitingNode()
    {
      var traversalStrategy = TraversalStrategy.SkipNode;

      while (InnerTreenumerator.MoveNext(traversalStrategy))
      {
        if (InnerTreenumerator.Mode == TreenumeratorMode.VisitingNode
          || _Predicate(InnerTreenumerator.ToNodeVisit()))
        {
          UpdateState();

          return true;
        }
      }

      UpdateState();

      return false;
    }

    private bool OnSchedulingNode(TraversalStrategy traversalStrategy)
    {
      var result = InnerTreenumerator.MoveNext(traversalStrategy);

      UpdateState();

      return result;
    }

    private void UpdateState()
    {
      Mode = InnerTreenumerator.Mode;

      if (Mode != TreenumeratorMode.EnumerationFinished)
      {
        Node = InnerTreenumerator.Node;
        VisitCount = InnerTreenumerator.VisitCount;
        OriginalPosition = InnerTreenumerator.OriginalPosition;
        Position = InnerTreenumerator.Position;
      }
    }
  }
}
