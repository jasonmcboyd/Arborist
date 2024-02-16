using Arborist.Treenumerables.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Treenumerables.SimpleSerializer
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

    public static string Serialize(ITreenumerable<string> treenumerable)
      => Serialize(treenumerable, node => node);

    public static string Serialize<TNode>(
      ITreenumerable<TNode> treenumerable,
      Func<TNode, string> map)
    {
      throw new NotImplementedException();
    }
  }
}
