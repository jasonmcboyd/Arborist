using Arborist.Linq.Extensions;
using System;
using System.Linq;

namespace Arborist.Linq.Treenumerators
{
  internal class WhereDepthFirstTreenumerator<TNode>
    : StackTreenumeratorWrapper<TNode, NodeVisit<TNode>, TNode>
  {
    public WhereDepthFirstTreenumerator(
      ITreenumerator<TNode> innerTreenumerator,
      Func<NodeVisit<TNode>, bool> predicate)
      : base(
          innerTreenumerator,
          branchVisit => branchVisit.Node)
    {
      _Predicate = predicate;
    }

    private readonly Func<NodeVisit<TNode>, bool> _Predicate;

    private bool _FirstValueReturned = false;

    private NodeVisit<NodeVisit<TNode>>? _CachedVisit;

    private bool _DoNotEnumerateInner = false;

    private bool OnInnerEnumerationMoveNext(ChildStrategy childStrategy)
    {
      if (InnerTreenumerator.MoveNext(childStrategy))
        return true;

      if (_CachedVisit != null && _CachedVisit.Value.VisitCount == 2)
      {
        Stack.ReplaceLast(_CachedVisit.Value);

        _CachedVisit = null;
      }
      else
        Stack.Clear();

      return false;
    }

    private void YieldCachedVisit()
    {
      Stack.ReplaceLast(_CachedVisit.Value);

      _CachedVisit = null;

      _DoNotEnumerateInner = true;

    }

    protected override void OnMoveNext(ChildStrategy childStrategy)
    {
      do
      {
        if (!_DoNotEnumerateInner)
        {
          if (!OnInnerEnumerationMoveNext(childStrategy))
            return;

          while (!_Predicate(InnerTreenumerator.Current))
            if (!OnInnerEnumerationMoveNext(false))
              return;
        }

        _DoNotEnumerateInner = false;

        if (!_FirstValueReturned)
        {
          _FirstValueReturned = true;

          var visit =
            NodeVisit.Create(
              InnerTreenumerator.Current,
              1,
              0,
              0);

          Stack.Add(visit);

          return;
        }

        if (InnerTreenumerator.Current.Depth < Stack.Last().Node.Depth)
        {
          if (_CachedVisit != null)
          {
            YieldCachedVisit();

            return;
          }

          Stack.RemoveLast();

          var visit =
            NodeVisit.Create(
              InnerTreenumerator.Current,
              Stack.Last().VisitCount + 1,
              Stack.Last().SiblingIndex,
              Stack.Last().Depth);

          Stack.ReplaceLast(visit);
        }
        else if (InnerTreenumerator.Current.Depth == Stack.Last().Node.Depth)
        {
          if (_CachedVisit != null)
          {
            if (InnerTreenumerator.Current.SiblingIndex != _CachedVisit.Value.SiblingIndex)
            {
              YieldCachedVisit();

              return;
            }

            continue;
          }

          var visit =
              NodeVisit.Create(
                InnerTreenumerator.Current,
                Stack.Last().VisitCount + 1,
                Stack.Last().SiblingIndex,
                Stack.Last().Depth);

          if (InnerTreenumerator.Current.SiblingIndex == Stack.Last().Node.SiblingIndex)
          {
            _CachedVisit = visit;

            continue;
          }

          visit = NodeVisit.Create(visit.Node, 1, visit.SiblingIndex + 1, visit.Depth);

          Stack.ReplaceLast(visit);
        }
        else
        {
          var visit =
            NodeVisit.Create(
              InnerTreenumerator.Current,
              1,
              Stack.Last().VisitCount - 1,
              Stack.Last().Depth + 1);

          Stack.Add(visit);

          _CachedVisit = null;
        }
      }
      while (_CachedVisit != null);

    }
  }
}
