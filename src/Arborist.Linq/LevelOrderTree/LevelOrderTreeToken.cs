namespace Arborist.Linq.LevelOrderTree
{
  public class LevelOrderTreeToken<T>
  {
    public LevelOrderTreeToken(LevelOrderTreeTokenType type)
    {
      Type = type;
      Value = default;
    }

    public LevelOrderTreeToken(T value)
    {
      Type = LevelOrderTreeTokenType.Node;
      Value = value;
    }

    public LevelOrderTreeTokenType Type { get; }
    public T Value { get; }

    public override string ToString()
    {
      switch (Type)
      {
        case LevelOrderTreeTokenType.GenerationSeparator:
          return "|";
        case LevelOrderTreeTokenType.FamilySeparator:
          return "_";
        default:
          return Value.ToString();
      }
    }
  }
}
