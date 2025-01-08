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
    }

    public TNode Node;
    public int VisitCount;
    public NodePosition Position;
  }
}
