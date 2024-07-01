using Arborist.Linq.Newick;
using System.Collections.Generic;

namespace Arborist.Linq.Extensions
{
  public static class EnumerableExtensions
  {
    public static IEnumerable<NewickToken<TNode>> ReverseNewickEnumerable<TNode>(this IEnumerable<NewickToken<TNode>> source)
    {
      var levelIndex = 0;

      var levels = new List<Stack<NewickToken<TNode>>>();

      var tokenCount = 0;

      foreach (var token in source)
      {
        tokenCount++;

        var level = levels.GetAtOrAdd(levelIndex);

        switch (token.Type)
        {
          case NewickTokenType.StartChildGroup:
            // Put the StartChildGroup token before the previous token.
            var previousToken = level.Pop();
            level.Push(token);
            level.Push(previousToken);

            // Add the EndChildGroup token to the next level.
            var nextLevel = levels.GetAtOrAdd(levelIndex + 1);
            nextLevel.Push(new NewickToken<TNode>(NewickTokenType.EndChildGroup));

            levelIndex++;
            break;

          case NewickTokenType.EndChildGroup:
            // Not adding this token because it was added when the StartChildGroup was encountered.
            levelIndex--;
            break;

          default:
            level.Push(token);
            break;
        }
      }

      while (tokenCount > 0)
      {
        tokenCount--;

        var level = levels[levelIndex];

        var token = level.Pop();

        if (token.Type == NewickTokenType.StartChildGroup)
          levelIndex++;
        else if (token.Type == NewickTokenType.EndChildGroup)
          levelIndex--;

        yield return token;
      }
    }
  }
}
