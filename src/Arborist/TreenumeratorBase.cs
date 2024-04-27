using Arborist.Core;

namespace Arborist
{
  public abstract class TreenumeratorBase<TNode> : ITreenumerator<TNode>
  {
    public TNode Node { get; protected set; } = default;

    public int VisitCount { get; protected set; } = 0;

    public NodePosition Position { get; protected set; } = (0, -1);

    public TraversalStrategy TraversalStrategy { get; protected set; } = default;

    public TreenumeratorMode Mode { get; protected set; } = default;

    protected bool EnumerationFinished { get; set; } = false;

    public abstract void Dispose();

    public bool MoveNext(TraversalStrategy traversalStrategy)
    {
      if (EnumerationFinished)
        return false;

      if (OnMoveNext(traversalStrategy))
        return true;

      EnumerationFinished = true;

      return false;
    }

    protected abstract bool OnMoveNext(TraversalStrategy traversalStrategy);
  }
}
