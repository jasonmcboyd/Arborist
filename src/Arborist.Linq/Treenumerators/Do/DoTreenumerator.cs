using Arborist.Core;
using Arborist.Linq.Extensions;
using System;

namespace Arborist.Linq.Treenumerators
{
  internal class DoTreenumerator<TNode> : ITreenumerator<TNode>
  {
    public DoTreenumerator(
      Func<ITreenumerator<TNode>> innerTreenumeratorFactory,
      Action<NodeVisit<TNode>> onNext)
    {
      _InnerTreenumerator = innerTreenumeratorFactory();
      _OnNext = onNext;
    }

    private readonly ITreenumerator<TNode> _InnerTreenumerator;
    private readonly Action<NodeVisit<TNode>> _OnNext;

    public TNode Node => _InnerTreenumerator.Node;

    public int VisitCount => _InnerTreenumerator.VisitCount;

    public TreenumeratorMode Mode => _InnerTreenumerator.Mode;

    public NodePosition Position => _InnerTreenumerator.Position;

    public bool MoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (!_InnerTreenumerator.MoveNext(nodeTraversalStrategies))
        return false;

      _OnNext?.Invoke(_InnerTreenumerator.ToNodeVisit());

      return true;
    }

    public void Dispose()
    {
      _InnerTreenumerator?.Dispose();
    }
  }
}
