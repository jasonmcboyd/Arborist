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

    private bool OnInnerEnumerationMoveNext(bool skipChildren)
    {
      if (InnerTreenumerator.MoveNext(skipChildren))
        return true;

      if (_CachedVisit != null && _CachedVisit.Value.VisitCount == 2)
      {
        Branch.ReplaceLast(_CachedVisit.Value);

        _CachedVisit = null;
      }
      else
        Branch.Clear();

      return false;
    }

    private void YieldCachedVisit()
    {
      Branch.ReplaceLast(_CachedVisit.Value);

      _CachedVisit = null;

      _DoNotEnumerateInner = true;

    }

    protected override void OnMoveNext(bool skipChildren)
    {
      do
      {
        if (!_DoNotEnumerateInner)
        {
          if (!OnInnerEnumerationMoveNext(skipChildren))
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

          Branch.Add(visit);

          return;
        }

        if (InnerTreenumerator.Current.Depth < Branch.Last().Node.Depth)
        {
          if (_CachedVisit != null)
          {
            YieldCachedVisit();

            return;
          }

          Branch.RemoveLast();

          var visit =
            NodeVisit.Create(
              InnerTreenumerator.Current,
              Branch.Last().VisitCount + 1,
              Branch.Last().SiblingIndex,
              Branch.Last().Depth);

          Branch.ReplaceLast(visit);
        }
        else if (InnerTreenumerator.Current.Depth == Branch.Last().Node.Depth)
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
                Branch.Last().VisitCount + 1,
                Branch.Last().SiblingIndex,
                Branch.Last().Depth);

          if (InnerTreenumerator.Current.SiblingIndex == Branch.Last().Node.SiblingIndex)
          {
            _CachedVisit = visit;

            continue;
          }

          visit = NodeVisit.Create(visit.Node, 1, visit.SiblingIndex + 1, visit.Depth);

          Branch.ReplaceLast(visit);
        }
        else
        {
          var visit =
            NodeVisit.Create(
              InnerTreenumerator.Current,
              1,
              Branch.Last().VisitCount - 1,
              Branch.Last().Depth + 1);

          Branch.Add(visit);

          _CachedVisit = null;
        }
      }
      while (_CachedVisit != null);

    }
  }
}
