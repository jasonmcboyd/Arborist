using Arborist.Core;
using Arborist.Linq.TreeEnumerable.BreadthFirstTree;
using Arborist.Nodes;
using Arborist.Treenumerables;
using Nito.Collections;
using System.Collections.Generic;

namespace Arborist.Linq.Extensions
{
  public static class BreadthFirstTreeEnumerableExtensions
  {
    public static ITreenumerable<TNode> ToTreenumerable<TNode>(this IBreadthFirstTreeEnumerable<TNode> source)
    {
      var stack = new Stack<BreadthFirstTreeEnumerableToken<TNode>>();

      foreach (var token in source)
        stack.Push(token);

      var deque = new Deque<Stack<NodeWithIndexableChildren<TNode>>>();

      var levelCount = 0;
      var bottomLevelFilled = false;

      while (stack.Count > 0)
      {
        var token = stack.Pop();

        if (token.Type == BreadthFirstTreeEnumerableTokenType.GenerationSeparator)
        {
          levelCount = deque.Count;
          bottomLevelFilled = true;
          deque.AddToFront(null);
          continue;
        }

        if (token.Type == BreadthFirstTreeEnumerableTokenType.FamilySeparator)
        {
          deque.AddToFront(null);
          continue;
        }

        if (deque[0] == null)
          deque[0] = new Stack<NodeWithIndexableChildren<TNode>>();

        var children =
          bottomLevelFilled
            ? deque.RemoveFromBack()
            : null;

        deque[0].Push(new NodeWithIndexableChildren<TNode>(token.Node, children));
      }

      return new IndexableTreenumerable<TNode>(deque[0]);
    }

    public static IBreadthFirstTreeEnumerable<TNode> ToReverseLevelOrderTreeEnumerable<TNode>(this IBreadthFirstTreeEnumerable<TNode> source)
    {
      return new BreadthFirstTreeEnumerable<TNode>(source.ReverseLevelOrderTreeEnumerable());
    }

    private static IEnumerable<BreadthFirstTreeEnumerableToken<TNode>> ReverseLevelOrderTreeEnumerable<TNode>(this IEnumerable<BreadthFirstTreeEnumerableToken<TNode>> source)
    {
      if (source == null)
        yield break;

      var nodes = new Stack<BreadthFirstTreeEnumerableToken<TNode>>();

      foreach (var token in source)
      {
        if (token.Type == BreadthFirstTreeEnumerableTokenType.GenerationSeparator)
        {
          while (nodes.Count > 0)
            yield return nodes.Pop();

          yield return token;
        }
        else
        {
          nodes.Push(token);
        }
      }

      while (nodes.Count > 0)
        yield return nodes.Pop();
    }
  }
}
