using Arborist.Core;

namespace Arborist.Treenumerators
{
  struct InternalNodeVisitState<TNode>
  {
    public InternalNodeVisitState(
      TNode node,
      NodePosition position)
    {
      Node = node;
      VisitCount = 0;
      Position = position;
      OwesPromotedParentVisit = false;
    }

    public TNode Node;
    public int VisitCount;
    public NodePosition Position;

    // True when this queued node (an effective parent) is owed an interleaved visit that
    // was swallowed because one of its children was SkipNode'd and a promoted grandchild
    // took the slot. Stored per-entry rather than as an engine-wide flag so it is paid
    // only to THIS parent and discarded when the parent retires -- never leaked to a
    // later, unrelated queue front (the 3-concurrent-skip over-count).
    public bool OwesPromotedParentVisit;
  }
}
