using Arborist.Core;
using Arborist.Linq.Extensions;
using System;

namespace Arborist.Linq.Treenumerators
{
  internal class SelectTreenumerator<TInner, TNode> : ITreenumerator<TNode>
  {
    public SelectTreenumerator(
      Func<ITreenumerator<TInner>> innerTreenumeratorFactory,
      Func<NodeContext<TInner>, TNode> selector)
    {
      _InnerTreenumerator = innerTreenumeratorFactory();
      _Selector = selector;
    }

    private readonly ITreenumerator<TInner> _InnerTreenumerator;
    private readonly Func<NodeContext<TInner>, TNode> _Selector;

    public TNode Node { get; private set; } = default;

    public int VisitCount => _InnerTreenumerator.VisitCount;

    public TreenumeratorMode Mode => _InnerTreenumerator.Mode;

    public NodePosition Position => _InnerTreenumerator.Position;

    public bool MoveNext(NodeTraversalStrategy nodeTraversalStrategy)
    {
      var hasNext = _InnerTreenumerator.MoveNext(nodeTraversalStrategy);

      if (!hasNext)
        return false;

      var nodeContext = _InnerTreenumerator.ToNodeContext();

      Node = _Selector(nodeContext);

      return true;
    }

    public void Dispose()
    {
      _InnerTreenumerator?.Dispose();
    }
  }
}
