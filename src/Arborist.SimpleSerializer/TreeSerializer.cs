using Arborist.Core;
using Arborist.Nodes;
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

    public static ITreenumerable<TResult> Deserialize<TResult>(
      string tree,
      Func<string, TResult> map)
      => DeserializeRoots(tree, map).ToTreenumerable();

    public static IEnumerable<INodeContainerWithIndexableChildren<string>> DeserializeRoots(string tree)
      => DeserializeRoots(tree, value => value);

    public static IEnumerable<INodeContainerWithIndexableChildren<TResult>> DeserializeRoots<TResult>(
      string tree,
      Func<string, TResult> map)
    {
      // TODO:
      // I am reversing because for some reason my lizard brain
      // finds this more intuitive. I want to change this in the future
      // to avoid the performance penalty of reversing two times, but I
      // am not that worried about performance right now so this is
      // good enough.
      var tokens = Tokenizer.Tokenize(tree).Reverse();

      var stack = new Stack<List<IndexableTreeNode<TResult>>>();

      stack.Push(new List<IndexableTreeNode<TResult>>());

      List<IndexableTreeNode<TResult>> children = null;

      var rootNodes = new List<IndexableTreeNode<TResult>>();

      foreach (var token in tokens)
      {
        TResult value;
        IndexableTreeNode<TResult> node;

        switch (token.TokenType)
        {
          case TokenType.Comma:
            if (stack.Count == 1)
            {
              var list = stack.Peek();
              rootNodes.Add(list.Last());
              list.RemoveAt(list.Count - 1);
            }
            break;

          case TokenType.LeftParentheses:
            children = stack.Pop();
            children.Reverse();
            break;

          case TokenType.RightParentheses:
            stack.Push(new List<IndexableTreeNode<TResult>>());
            break;

          default:
            value = map(token.Symbol);
            node = new IndexableTreeNode<TResult>(value, children);
            stack.Peek().Add(node);
            children = null;
            break;
        }
      }

      if (stack.Peek().Count > 0)
        rootNodes.Add(stack.Peek().Last());

      rootNodes.Reverse();

      return rootNodes;
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

        while (treenumerator.MoveNext(TraversalStrategy.TraverseSubtree))
        {
          if (treenumerator.VisitCount != 1)
            continue;

          var depth = treenumerator.OriginalPosition.Depth;

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

          previousDepth = treenumerator.OriginalPosition.Depth;
        }

        while (previousDepth-- > 0)
          builder.Append(')');

        return builder.ToString();
      }
    }
  }
}
