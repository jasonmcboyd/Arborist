using Arborist.Core;

namespace Arborist.Linq.Treenumerators
{
  internal class EmptyTreenumerator<TNode> : ITreenumerator<TNode>
  {
    public EmptyTreenumerator()
    {
    }

    public TNode Node => default;
    public int VisitCount => default;
    public NodePosition OriginalPosition => default;
    public NodePosition Position => default;
    public TraversalStrategy TraversalStrategy => default;

    public TreenumeratorMode Mode { get; private set; } = default;

    public bool MoveNext(TraversalStrategy traversalStrategy)
    {
      return false;
    }

    public void Dispose()
    {
    }
  }
}
