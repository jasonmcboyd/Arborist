using Arborist.Core;

namespace Arborist.Linq.Treenumerators
{
  internal class HideTreenumerator<TNode> : ITreenumerator<TNode>
  {
    public HideTreenumerator(ITreenumerator<TNode> innerTreenumerator)
    {
      _InnerTreenumerator = innerTreenumerator;
    }

    private readonly ITreenumerator<TNode> _InnerTreenumerator;

    public TNode Node => _InnerTreenumerator.Node;

    public int VisitCount => _InnerTreenumerator.VisitCount;

    public TreenumeratorMode Mode => _InnerTreenumerator.Mode;

    public NodePosition Position => _InnerTreenumerator.Position;

    public bool MoveNext(NodeTraversalStrategy nodeTraversalStrategy)
    {
      return _InnerTreenumerator.MoveNext(nodeTraversalStrategy);
    }

    public void Dispose()
    {
      _InnerTreenumerator?.Dispose();
    }
  }
}
