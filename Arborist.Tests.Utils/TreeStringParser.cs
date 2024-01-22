using Arborist.Treenumerables;
using System.Collections.Generic;
using System.Text;

namespace Arborist.Tests.Utils
{
  public static class TreeStringParser
  {
    public static IndexableTreenumerable<TestNode, string> ParseTreeString(string tree)
    {
      var parensCount = 0;
      var rootNode = new TestNode();
      var stack = new Stack<TestNode>();
      var builder = new StringBuilder();

      stack.Push(rootNode);

      foreach (var c in tree)
      {
        if (char.IsLetter(c))
        {
          builder.Append(c);
          continue;
        }

        AddNodeToStack(builder, stack);

        if (c == '(')
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

      AddNodeToStack(builder, stack);

      return new IndexableTreenumerable<TestNode, string>(rootNode.Children);
    }

    private static void AddNodeToStack(StringBuilder stringBuilder, Stack<TestNode> stack)
    {
      if (stringBuilder.Length == 0)
        return;

      var node = new TestNode { Value = stringBuilder.ToString() };

      stringBuilder.Clear();

      stack.Peek().Children.Add(node);

      stack.Push(node);
    }
  }
}
