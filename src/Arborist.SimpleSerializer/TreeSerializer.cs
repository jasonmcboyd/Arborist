using Arborist.Core;
using Arborist.Treenumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arborist.SimpleSerializer
{
  public static class TreeSerializer
  {
    public static ITreenumerable<string> Deserialize(string tree)
      => Deserialize(tree, value => value);

    public static ITreenumerable<TValue> Deserialize<TValue>(
      string tree,
      Func<string, TValue> map)
      => new SimpleNodeTreenumerable<TValue>(DeserializeRoots(tree, map));

    private static IEnumerable<SimpleNode<string>> DeserializeRoots(string tree)
      => DeserializeRoots(tree, value => value);

    private static IEnumerable<SimpleNode<TValue>> DeserializeRoots<TValue>(
      string tree,
      Func<string, TValue> map)
    {
      var tokens = Tokenizer.Tokenize(tree);

      var stack = new Stack<List<SimpleNode<TValue>>>();

      stack.Push(new List<SimpleNode<TValue>>());

      foreach (var token in tokens)
      {
        TValue value;

        switch (token.TokenType)
        {
          case TokenType.Comma:
            // Do nothing
            break;

          case TokenType.LeftParentheses:
            stack.Push(new List<SimpleNode<TValue>>());
            break;

          case TokenType.RightParentheses:
            var children = stack.Pop();
            var nodes = stack.Peek();
            var node = nodes.Last();
            nodes[nodes.Count - 1] = new SimpleNode<TValue>(node.Value, children);
            break;

          default:
            value = map(token.Symbol);
            node = new SimpleNode<TValue>(value);
            stack.Peek().Add(node);
            break;
        }
      }

      return stack.Pop();
    }

    public static string Serialize(this ITreenumerable<string> treenumerable)
      => Serialize(treenumerable, node => node);

    public static string Serialize<TNode>(
      this ITreenumerable<TNode> treenumerable,
      Func<TNode, string> map)
    {
      var builder = new StringBuilder();

      using (var treenumerator = treenumerable.GetDepthFirstTreenumerator())
      {
        int previousDepth = -1;

        while (treenumerator.MoveNext(NodeTraversalStrategies.TraverseAll))
        {
          if (treenumerator.VisitCount != 1)
            continue;

          var depth = treenumerator.Position.Depth;

          if (previousDepth != -1)
          {
            if (depth > previousDepth)
              builder.Append('(');
            else if (depth < previousDepth)
              builder.Append("),");
            else
              builder.Append(',');
          }

          builder.Append(map(treenumerator.Node));

          previousDepth = treenumerator.Position.Depth;
        }

        while (previousDepth-- > 0)
          builder.Append(')');

        return builder.ToString();
      }
    }
  }
}
