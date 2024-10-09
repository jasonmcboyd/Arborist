using Arborist.Linq;
using Arborist.Linq.TreeEnumerable.DepthFirstTree;
using Arborist.Nodes;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Linq
{
  public static partial class EnumerableExtensions
  {
    internal static IEnumerable<NodeWithIndexableChildren<TValue>> ToReverseTreeRoots<TValue>(
      this IEnumerable<DepthFirstTreeEnumerableToken<TValue>> source)
    {
      var stack = new Stack<List<NodeWithIndexableChildren<TValue>>>();

      using (var enumerator = source.Reverse().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          var token = enumerator.Current;

          switch (token.Type)
          {
            case DepthFirstTreeEnumerableTokenType.StartChildGroup:
              var children = stack.Pop();
              enumerator.MoveNext();
              token = enumerator.Current;
              stack.Peek().Add(new NodeWithIndexableChildren<TValue>(token.Node, children));
              break;

            case DepthFirstTreeEnumerableTokenType.EndChildGroup:
              stack.Push(new List<NodeWithIndexableChildren<TValue>>());
              if (stack.Count == 1)
                stack.Push(new List<NodeWithIndexableChildren<TValue>>());
              break;

            default:
              if (stack.Count == 0)
                stack.Push(new List<NodeWithIndexableChildren<TValue>>());

              stack.Peek().Add(new NodeWithIndexableChildren<TValue>(token.Node));
              break;
          }

          if (stack.Count == 1 && stack.Peek().Count > 0)
            yield return stack.Pop()[0];
        }
      }
    }
  }
}
