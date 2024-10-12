namespace Arborist.Common
{
  public readonly struct NodeAndSiblingIndex<TNode>
  {
    public NodeAndSiblingIndex(
      TNode node,
      int siblingIndex)
    {
      Node = node;
      SiblingIndex = siblingIndex;
    }

    public TNode Node { get; }
    public int SiblingIndex { get; }

    public override string ToString()
    {
      return $"{SiblingIndex}  {Node}";
    }

    public static implicit operator NodeAndSiblingIndex<TNode>((TNode, int) tuple)
      => new NodeAndSiblingIndex<TNode>(tuple.Item1, tuple.Item2);
  }
}
