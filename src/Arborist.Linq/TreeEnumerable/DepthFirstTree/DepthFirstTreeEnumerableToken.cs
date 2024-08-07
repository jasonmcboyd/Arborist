namespace Arborist.Linq.TreeEnumerable.DepthFirstTree
{
  public class DepthFirstTreeEnumerableToken<TNode> : ITreeEnumerableToken<TNode, DepthFirstTreeEnumerableTokenType>
  {
    public DepthFirstTreeEnumerableToken(DepthFirstTreeEnumerableTokenType type)
    {
      Type = type;
      Node = default;
    }

    public DepthFirstTreeEnumerableToken(TNode node)
    {
      Type = DepthFirstTreeEnumerableTokenType.Node;
      Node = node;
    }

    public DepthFirstTreeEnumerableTokenType Type { get; }
    public TNode Node { get; }

    public override string ToString()
    {
      switch (Type)
      {
        case DepthFirstTreeEnumerableTokenType.StartChildGroup:
          return "(";
        case DepthFirstTreeEnumerableTokenType.EndChildGroup:
          return ")";
        default:
          return Node.ToString();
      }
    }
  }
}
