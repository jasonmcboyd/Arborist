using System;

namespace Arborist.Linq.Treenumerators
{
  internal class SelectTreenumerator<TInner, TNode> : TreenumeratorWrapper<TInner, TNode>
  {
    public SelectTreenumerator(
      ITreenumerator<TInner> InnerTreenumerator,
      Func<NodeVisit<TInner>, TNode> selector) : base(InnerTreenumerator)
    {
      _Selector = selector;
    }

    private readonly Func<NodeVisit<TInner>, TNode> _Selector;

    protected override bool OnMoveNext(ChildStrategy childStrategy)
    {
      var hasNext = InnerTreenumerator.MoveNext(childStrategy);

      if (hasNext)
      {
        var node = _Selector(InnerTreenumerator.Current);

        State = InnerTreenumerator.State;

        Current =
          NodeVisit
          .Create(
            node,
            InnerTreenumerator.Current.VisitCount,
            InnerTreenumerator.Current.SiblingIndex,
            InnerTreenumerator.Current.Depth);
      }

      return hasNext;
    }
  }
}
