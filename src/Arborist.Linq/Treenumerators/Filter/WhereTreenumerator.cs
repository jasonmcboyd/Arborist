using Arborist.Core;
using Arborist.Linq.Extensions;
using System;

namespace Arborist.Linq.Treenumerators
{
  internal class WhereTreenumerator<TNode>
    : TreenumeratorWrapper<TNode>
  {
    public WhereTreenumerator(
      ITreenumerator<TNode> innerTreenumerator,
      Func<NodeVisit<TNode>, bool> predicate)
      : base(innerTreenumerator)
    {
      _Predicate = predicate;
    }

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (Mode == TreenumeratorMode.EnumerationFinished)
        return false;

      if (Mode != TreenumeratorMode.SchedulingNode)
        return OnVisitingNode();

      return OnSchedulingNode(schedulingStrategy);
    }

    private bool OnVisitingNode()
    {
      var schedulingStrategy = SchedulingStrategy.SkipNode;

      while (InnerTreenumerator.MoveNext(schedulingStrategy))
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

    private bool OnSchedulingNode(SchedulingStrategy schedulingStrategy)
    {
      var result = InnerTreenumerator.MoveNext(schedulingStrategy);

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
        OriginalPosition = InnerTreenumerator.Position;
        Position = InnerTreenumerator.Position;
        SchedulingStrategy = InnerTreenumerator.SchedulingStrategy;
      }
    }
  }
}


