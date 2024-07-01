namespace Arborist.Linq.Newick
{
  public class NewickToken<T>
  {
    public NewickToken(NewickTokenType type)
    {
      Type = type;
      Value = default;
    }

    public NewickToken(T value)
    {
      Type = NewickTokenType.Node;
      Value = value;
    }

    public NewickTokenType Type { get; }
    public T Value { get; }

    public override string ToString()
    {
      switch (Type)
      {
        case NewickTokenType.StartChildGroup:
          return "(";
        case NewickTokenType.EndChildGroup:
          return ")";
        default:
          return Value.ToString();
      }
    }
  }
}
