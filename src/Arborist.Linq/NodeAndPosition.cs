using Arborist.Core;

namespace Arborist.Linq
{
  public readonly struct NodeAndPosition<TNode>
  {
    public NodeAndPosition(
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
