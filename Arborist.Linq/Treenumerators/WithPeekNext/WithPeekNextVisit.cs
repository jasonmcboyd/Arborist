namespace Arborist.Linq.Treenumerators
{
  public struct WithPeekNextVisit<TNode>
  {
    public WithPeekNextVisit(TNode node, NodeVisit<TNode> nextVisit)
    {
      Node = node;
      _NextVisit = nextVisit;
      HasNextVisit = true;
    }

    public WithPeekNextVisit(TNode node)
    {
      Node = node;
      _NextVisit = default;
      HasNextVisit = false;
    }

    public TNode Node { get; }

    public bool HasNextVisit { get; }

    private readonly NodeVisit<TNode> _NextVisit;
    public NodeVisit<TNode> NextVisit => HasNextVisit ? _NextVisit : throw new System.InvalidOperationException($"{nameof(NextVisit)} is not available.");
  }
}
