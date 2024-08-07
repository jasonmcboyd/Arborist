namespace Arborist.Linq.TreeEnumerable.BreadthFirstTree
{
  public class BreadthFirstTreeEnumerableToken<TNode> : ITreeEnumerableToken<TNode, BreadthFirstTreeEnumerableTokenType>
  {
    public BreadthFirstTreeEnumerableToken(BreadthFirstTreeEnumerableTokenType type)
    {
      Type = type;
      Node = default;
    }

    public BreadthFirstTreeEnumerableToken(TNode node)
    {
      Type = BreadthFirstTreeEnumerableTokenType.Node;
      Node = node;
    }

    public BreadthFirstTreeEnumerableTokenType Type { get; }
    public TNode Node { get; }

    public override string ToString()
    {
      switch (Type)
      {
        case BreadthFirstTreeEnumerableTokenType.GenerationSeparator:
          return "|";
        case BreadthFirstTreeEnumerableTokenType.FamilySeparator:
          return ":";
        default:
          return Node.ToString();
      }
    }
  }
}
