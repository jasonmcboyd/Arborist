namespace Arborist
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
  }
}
