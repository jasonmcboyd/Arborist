namespace Arborist.Treenumerables.SimpleSerializer
{
  public struct Token
  {
    public Token(char character) : this(character.ToString())
    {
    }

    public Token(string value)
    {
      Symbol = value;

      switch (value)
      {
        case ",":
          TokenType = TokenType.Comma;
          break;

        case "(":
          TokenType = TokenType.LeftParentheses;
          break;

        case ")":
          TokenType = TokenType.RightParentheses;
          break;

        default:
          TokenType = TokenType.String;
          break;
      }
    }

    public string Symbol { get; }
    public TokenType TokenType { get; }

    public override string ToString() => Symbol;
  }
}
