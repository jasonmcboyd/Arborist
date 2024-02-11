using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Treenumerables.SimpleSerializer
{
  public static class TreeSerializer
  {
    public static IndexableTreenumerable<TreeNode<string>, string> Deserialize(string tree)
      => Deserialize(tree, value => value);

    public static List<TreeNode<string>> DeserializeRoots(string tree)
      => DeserializeRoots(tree, value => value);

    public static IndexableTreenumerable<TreeNode<TResult>, TResult> Deserialize<TResult>(
      string tree,
      Func<string, TResult> map)
    {
      var rootNodes = DeserializeRoots<TResult>(tree, map);

      return new IndexableTreenumerable<TreeNode<TResult>, TResult>(rootNodes);
    }

    public static List<TreeNode<TResult>> DeserializeRoots<TResult>(
      string tree,
      Func<string, TResult> map)
    {
      var tokens = Tokenizer.Tokenize(tree).Reverse();

      var stack = new Stack<List<TreeNode<TResult>>>();

      stack.Push(new List<TreeNode<TResult>>());

      List<TreeNode<TResult>> children = null;

      var rootNodes = new List<TreeNode<TResult>>();

      foreach (var token in tokens)
      {
        TResult value;
        TreeNode<TResult> node;

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
            stack.Push(new List<TreeNode<TResult>>());
            break;

          default:
            value = map(token.Symbol);
            node = new TreeNode<TResult>(value, children);
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
