using Arborist.Core;
using Arborist.Linq.TreeEnumerable.DepthFirstTree;
using Arborist.Nodes;
using Arborist.Treenumerables;
using Nito.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Linq.Extensions
{
  public static class DepthFirstTreeEnumerableExtensions
  {
    public static ITreenumerable<TValue> ToTreenumerable<TValue>(this IDepthFirstTreeEnumerable<TValue> source)
    {
      var nodes = new Stack<List<NodeWithIndexableChildren<TValue>>>();

      nodes.Push(new List<NodeWithIndexableChildren<TValue>>());

      var tokens = new Stack<DepthFirstTreeEnumerableToken<TValue>>();

      using (var enumerator = source.GetEnumerator())
      {
        DepthFirstTreeEnumerableToken<TValue> previousToken = default;

        while (enumerator.MoveNext())
        {
          var token = enumerator.Current;

          switch (token.Type)
          {
            case DepthFirstTreeEnumerableTokenType.StartChildGroup:
              nodes.Push(new List<NodeWithIndexableChildren<TValue>>());
              break;

            case DepthFirstTreeEnumerableTokenType.EndChildGroup:
              if (previousToken.Type == DepthFirstTreeEnumerableTokenType.EndChildGroup)
              {
                var node = new NodeWithIndexableChildren<TValue>(tokens.Pop().Node, nodes.Pop());
                nodes.Peek().Add(node);
              }
              else
              {
                nodes.Peek().Add(new NodeWithIndexableChildren<TValue>(tokens.Pop().Node));
                var node = new NodeWithIndexableChildren<TValue>(tokens.Pop().Node, nodes.Pop());
                nodes.Peek().Add(node);
              }
              break;

            default:
              if (tokens.Count == nodes.Count)
                nodes.Peek().Add(new NodeWithIndexableChildren<TValue>(tokens.Pop().Node));

              tokens.Push(token);

            break;
          }

          previousToken = enumerator.Current;
        }

        if (tokens.Count > 0)
          nodes.Peek().Add(new NodeWithIndexableChildren<TValue>(tokens.Pop().Node));

        return nodes.Pop().ToTreenumerable();
      }
    }

    public static IDepthFirstTreeEnumerable<TNode> ToReversePreorderTreeEnumerable<TNode>(this IDepthFirstTreeEnumerable<TNode> source)
    {
      return new DepthFirstTreeEnumerable<TNode>(source.ReversePreorderTreeEnumerable());
    }

    private static IEnumerable<DepthFirstTreeEnumerableToken<TNode>> ReversePreorderTreeEnumerable<TNode>(this IEnumerable<DepthFirstTreeEnumerableToken<TNode>> source)
    {
      if (source == null)
        yield break;

      var stack = new List<Deque<DepthFirstTreeEnumerableToken<TNode>>>()
      {
        new Deque<DepthFirstTreeEnumerableToken<TNode>>()
      };

      var depth = 0;

      using (var enumerator = source.Reverse().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          var token = enumerator.Current;

          switch (token.Type)
          {
            case DepthFirstTreeEnumerableTokenType.StartChildGroup:
              stack[depth].AddToBack(new DepthFirstTreeEnumerableToken<TNode>(DepthFirstTreeEnumerableTokenType.EndChildGroup));
              depth--;
              break;

            case DepthFirstTreeEnumerableTokenType.EndChildGroup:
              stack[depth].AddToBack(new DepthFirstTreeEnumerableToken<TNode>(DepthFirstTreeEnumerableTokenType.StartChildGroup));
              depth++;
              if (depth > stack.Count - 1)
                stack.Add(new Deque<DepthFirstTreeEnumerableToken<TNode>>());
              break;

            default:
              var currentLevel = stack[depth];
              var precededByChildGroupToken =
                currentLevel.Count > 0
                && currentLevel.Last().Type != DepthFirstTreeEnumerableTokenType.Node;

              currentLevel.AddToBack(token);
              break;
          }

          if (stack[0].Count > 0 && stack[0].Last().Type == DepthFirstTreeEnumerableTokenType.Node)
          {
            depth = 0;

            while (stack[depth].Count > 0)
            {
              var node = stack[depth].RemoveFromFront();

              if (node.Type == DepthFirstTreeEnumerableTokenType.StartChildGroup)
              {
                yield return stack[depth].RemoveFromFront();
                depth++;
              }
              else if (node.Type == DepthFirstTreeEnumerableTokenType.EndChildGroup)
              {
                depth--;
              }

              yield return node;
            }
          }
        }
      }
    }
  }
}
