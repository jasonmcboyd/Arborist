namespace Arborist.SimpleSerializer
{
  public struct Token
  {
    public Token(char character)
    {
      switch (character)
      {
        case ',':
          Symbol = ",";                       // string literals are interned -> no allocation
          TokenType = TokenType.Comma;
          break;

        case '(':
          Symbol = "(";
          TokenType = TokenType.LeftParentheses;
          break;

        case ')':
          Symbol = ")";
          TokenType = TokenType.RightParentheses;
          break;

        default:
          Symbol = character.ToString();
          TokenType = TokenType.String;
          break;
      }
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
