namespace Arborist.Linq.Treenumerators
{
  public struct WithParentNode<TNode>
  {
    public WithParentNode(TNode node, TNode parentNode)
    {
      Node = node;
      _ParentNode = parentNode;
      HasParentNode = true;
    }

    public WithParentNode(TNode node)
    {
      Node = node;
      _ParentNode = default;
      HasParentNode = false;
    }

    public TNode Node { get; }

    public bool HasParentNode { get; }

    private readonly TNode _ParentNode;
    public TNode ParentNode => HasParentNode ? _ParentNode : throw new System.InvalidOperationException($"{nameof(ParentNode)} is not available.");
  }
}
