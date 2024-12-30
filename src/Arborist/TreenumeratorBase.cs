using Arborist.Core;
using System;

namespace Arborist
{
  public abstract class TreenumeratorBase<TNode> : ITreenumerator<TNode>
  {
    public TNode Node { get; protected set; } = default;

    public int VisitCount { get; protected set; } = 0;

    public NodePosition Position { get; protected set; } = new NodePosition(0, -1);

    public TreenumeratorMode Mode { get; protected set; } = default;

    protected bool EnumerationFinished { get; private set; }


    public bool MoveNext(NodeTraversalStrategies nodeTraversalStrategy)
    {
      if (Disposed || EnumerationFinished)
        return false;

      if (OnMoveNext(nodeTraversalStrategy))
        return true;

      EnumerationFinished = true;

      return false;
    }

    protected abstract bool OnMoveNext(NodeTraversalStrategies nodeTraversalStrategy);

    #region IDisposable

    protected bool Disposed { get; private set; } = false;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
      if (!Disposed)
      {
        if (disposing)
          OnDisposing();

        Disposed = true;
      }
    }

    protected virtual void OnDisposing()
    {
    }

    // Finalizer to ensure resources are released if Dispose is not called.
    ~TreenumeratorBase()
    {
      Dispose(false);
    }

    #endregion IDisposable
  }
}
