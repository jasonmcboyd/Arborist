namespace Arborist.Linq.Treenumerators
{
  public struct IsLeafNode<TNode>
  {
    public IsLeafNode(TNode node, bool isLeaf)
    {
      Node = node;
      IsLeaf = isLeaf;
    }

    public TNode Node { get; }
    public bool IsLeaf { get; }
  }
}
