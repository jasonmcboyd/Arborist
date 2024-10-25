using Arborist.Core;
using System;

namespace Arborist.Linq.Treenumerators
{
  internal class HideTreenumerator<TNode> : ITreenumerator<TNode>
  {
    public HideTreenumerator(
      Func<ITreenumerator<TNode>> innerTreenumeratorFactory)
    {
      _InnerTreenumerator = innerTreenumeratorFactory();
    }

    private readonly ITreenumerator<TNode> _InnerTreenumerator;

    public TNode Node => _InnerTreenumerator.Node;

    public int VisitCount => _InnerTreenumerator.VisitCount;

    public TreenumeratorMode Mode => _InnerTreenumerator.Mode;

    public NodePosition Position => _InnerTreenumerator.Position;

    public bool MoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      return _InnerTreenumerator.MoveNext(nodeTraversalStrategies);
    }

    public void Dispose()
    {
      _InnerTreenumerator?.Dispose();
    }
  }
}
