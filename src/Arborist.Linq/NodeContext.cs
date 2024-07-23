using Arborist.Core;

namespace Arborist.Linq
{
  public readonly struct NodeContext<TNode>
  {
    public NodeContext(
      TNode node,
      NodePosition position)
    {
      Node = node;
      Position = position;
    }

    public TNode Node { get; }
    public NodePosition Position { get; }

    public override string ToString()
    {
      return $"{Position}  {Node}";
    }
  }
}
