using Arborist.Core;

namespace Arborist.Linq
{
  internal class EmptyTreenumerator<TNode> : ITreenumerator<TNode>
  {
    private EmptyTreenumerator()
    {
    }

    public static EmptyTreenumerator<TNode> Instance { get; } = new EmptyTreenumerator<TNode>();

    public TNode Node => default;
    public int VisitCount => default;
    public NodePosition OriginalPosition => (0, -1);
    public NodePosition Position => (0, -1);
    public TraversalStrategy TraversalStrategy => default;
    public TreenumeratorMode Mode { get; private set; } = default;

    public bool MoveNext(TraversalStrategy traversalStrategy) => false;

    public void Dispose()
    {
    }
  }
}
