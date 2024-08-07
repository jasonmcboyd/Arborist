using Arborist.Core;
using Arborist.Linq.TreeEnumerable.BreadthFirstTree;
using Arborist.Nodes;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Extensions
{
  public static class BreadthFirstTreeEnumerableExtensions
  {
    public static ITreenumerable<TNode> ToTreenumerable<TNode>(this IBreadthFirstTreeEnumerable<TNode> source)
    {
      var rootNodes = new List<NodeWithIndexableChildren<TNode>>();

      throw new NotImplementedException();
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
