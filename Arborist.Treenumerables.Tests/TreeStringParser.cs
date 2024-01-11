using System.Collections.Generic;

namespace Arborist.Treenumerables.Tests
{
  internal static class TreeStringParser
  {
    public static IndexableTreenumerable<TestNode, char> ParseTreeString(string tree)
    {
      var parensCount = 0;

      var rootNode = new TestNode();

      var stack = new Stack<TestNode>();

      stack.Push(rootNode);

      foreach (var c in tree)
      {
        if (char.IsLetter(c))
        {
          var node = new TestNode { Value = c };

          stack.Peek().Children.Add(node);

          stack.Push(node);
        }
        else if (c == '(')
        {
          parensCount++;
        }
        else if (c == ')')
        {
          parensCount--;

          stack.Pop();
        }
        else if (c == ',')
        {
          stack.Pop();
        }
      }

      return new IndexableTreenumerable<TestNode, char>(rootNode.Children);
    }
  }
}
