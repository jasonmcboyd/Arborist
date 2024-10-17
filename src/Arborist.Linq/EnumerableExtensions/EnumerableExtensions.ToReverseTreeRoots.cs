using Arborist.Linq.TreeEnumerable.DepthFirstTree;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Linq
{
  public static partial class EnumerableExtensions
  {
    internal static IEnumerable<SimpleNode<TValue>> ToReverseTreeRoots<TValue>(
      this IEnumerable<DepthFirstTreeEnumerableToken<TValue>> source)
    {
      var stack = new Stack<List<SimpleNode<TValue>>>();

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
              stack.Peek().Add(new SimpleNode<TValue>(token.Node, children));
              break;

            case DepthFirstTreeEnumerableTokenType.EndChildGroup:
              stack.Push(new List<SimpleNode<TValue>>());
              if (stack.Count == 1)
                stack.Push(new List<SimpleNode<TValue>>());
              break;

            default:
              if (stack.Count == 0)
                stack.Push(new List<SimpleNode<TValue>>());

              stack.Peek().Add(new SimpleNode<TValue>(token.Node));
              break;
          }

          if (stack.Count == 1 && stack.Peek().Count > 0)
            yield return stack.Pop()[0];
        }
      }
    }
  }
}
