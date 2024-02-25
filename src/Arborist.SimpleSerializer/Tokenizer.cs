using System.Collections.Generic;
using System.Text;

namespace Arborist.SimpleSerializer
{
  public static class Tokenizer
  {
    public static IEnumerable<Token> Tokenize(string tree)
    {
      if (string.IsNullOrEmpty(tree))
        yield break;

      var builder = new StringBuilder();

      for (int i = 0; i < tree.Length; i++)
      {
        var character = tree[i];

        if (IsSpecialSymbol(character))
        {
          if (builder.Length > 0)
          {
            yield return new Token(builder.ToString());
            builder.Clear();
          }

          yield return new Token(character);

          continue;
        }

        builder.Append(character);
      }

      if (builder.Length > 0)
        yield return new Token(builder.ToString());
    }
  
    private static bool IsSpecialSymbol(char character)
    {
      var symbols = ",()";

      for (int i = 0; i < symbols.Length; i++)
        if (symbols[i] == character)
          return true;

      return false;
    }
  }
}
