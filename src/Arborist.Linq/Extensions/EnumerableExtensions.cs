using Arborist.Linq.Newick;
using Arborist.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Linq.Extensions
{
  public static class EnumerableExtensions
  {
    internal static IEnumerable<INodeWithIndexableChildren<TNode>> ToReverseTreeRoots<TNode>(this IEnumerable<NewickToken<TNode>> source)
    {
      var stack = new Stack<List<NodeWithIndexableChildren<TNode>>>();

      using (var enumerator = source.Reverse().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          var token = enumerator.Current;

          switch (token.Type)
          {
            case NewickTokenType.StartChildGroup:
              var children = stack.Pop();
              enumerator.MoveNext();
              token = enumerator.Current;
              stack.Peek().Add(new NodeWithIndexableChildren<TNode>(token.Value, children));
              break;

            case NewickTokenType.EndChildGroup:
              stack.Push(new List<NodeWithIndexableChildren<TNode>>());
              if (stack.Count == 1)
                stack.Push(new List<NodeWithIndexableChildren<TNode>>());
              break;

            default:
              if (stack.Count == 0)
                stack.Push(new List<NodeWithIndexableChildren<TNode>>());

              stack.Peek().Add(new NodeWithIndexableChildren<TNode>(token.Value));
              break;
          }

          if (stack.Count == 1 && stack.Peek().Count > 0)
            yield return stack.Pop()[0];
        }
      }
    }
  }
}
