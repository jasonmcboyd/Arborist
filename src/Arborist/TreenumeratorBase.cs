using Arborist.Core;

namespace Arborist
{
  public abstract class TreenumeratorBase<TNode> : ITreenumerator<TNode>
  {
    public TNode Node { get; protected set; } = default;

    public int VisitCount { get; protected set; } = 0;

    public NodePosition Position { get; protected set; } = new NodePosition(0, -1);

    public TreenumeratorMode Mode { get; protected set; } = default;

    protected bool EnumerationFinished { get; private set; }

    public abstract void Dispose();

    public bool MoveNext(NodeTraversalStrategy nodeTraversalStrategy)
    {
      if (EnumerationFinished)
        return false;

      if (OnMoveNext(nodeTraversalStrategy))
        return true;

      EnumerationFinished = true;

      return false;
    }

    protected abstract bool OnMoveNext(NodeTraversalStrategy nodeTraversalStrategy);
  }
}
