namespace Arborist.Linq.PreorderTree
{
  public class PreorderTreeToken<T>
  {
    public PreorderTreeToken(PreorderTreeTokenType type)
    {
      Type = type;
      Value = default;
    }

    public PreorderTreeToken(T value)
    {
      Type = PreorderTreeTokenType.Node;
      Value = value;
    }

    public PreorderTreeTokenType Type { get; }
    public T Value { get; }

    public override string ToString()
    {
      switch (Type)
      {
        case PreorderTreeTokenType.StartChildGroup:
          return "(";
        case PreorderTreeTokenType.EndChildGroup:
          return ")";
        default:
          return Value.ToString();
      }
    }
  }
}
