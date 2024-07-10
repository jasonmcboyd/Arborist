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
 
    internal static IEnumerable<INodeWithIndexableChildren<TAccumulate>> ToLeaffixScanTreeRoots<TSource, TAccumulate>(
      this IEnumerable<NewickToken<NodeAndPosition<TSource>>> source,
      Func<NodeAndPosition<TAccumulate>, NodeAndPosition<TSource>, TAccumulate> seedAccumulator,
      Func<NodeAndPosition<TAccumulate>, NodeAndPosition<TSource>, TAccumulate> initialAccumulator,
      Func<NodeAndPosition<TAccumulate>, NodeAndPosition<TAccumulate>, TAccumulate> accumulator,
      Func<NodeAndPosition<TSource>, TAccumulate> seedGenerator)
    {
      var tokens = source.ToArray();

      var tree = new Stack<Stack<(NodeWithIndexableChildren<TAccumulate> Node, NodeAndPosition<TAccumulate> NodeAndPosition)>>();

      // Traverse the tokens in reverse order and build the tree up to the root nodes while aggregating the values.
      for (var i = tokens.Length - 1; i >= 0; i--)
      {
        var token = tokens[i];

        switch (token.Type)
        {
          case NewickTokenType.StartChildGroup:
            {
              var children = tree.Pop();
              i--;
              token = tokens[i];
              var parent = token.Value;

              var childArray = new NodeWithIndexableChildren<TAccumulate>[children.Count];

              var processedChildrenCount = -1;

              TAccumulate accumulate = default;

              while (children.Count > 0)
              {
                var child = children.Pop();
                processedChildrenCount++;

                if (processedChildrenCount == 0)
                  accumulate = initialAccumulator(child.NodeAndPosition, parent);
                else
                  accumulate = accumulator(child.NodeAndPosition, new NodeAndPosition<TAccumulate>(accumulate, parent.Position));

                childArray[processedChildrenCount] = child.Node;
              }

              tree.Peek().Push((new NodeWithIndexableChildren<TAccumulate>(accumulate, childArray), new NodeAndPosition<TAccumulate>(accumulate, parent.Position)));
              break;
            }

          case NewickTokenType.EndChildGroup:
            {
              tree.Push(new Stack<(NodeWithIndexableChildren<TAccumulate>, NodeAndPosition<TAccumulate>)>());
              if (tree.Count == 1)
                tree.Push(new Stack<(NodeWithIndexableChildren<TAccumulate>, NodeAndPosition<TAccumulate>)>());
              break;
            }

          default:
            {
              var seed = seedGenerator(token.Value);
              var seedNodeAndPosition = new NodeAndPosition<TAccumulate>(seed, token.Value.Position);
              var accumulate = seedAccumulator(seedNodeAndPosition, token.Value);
              var node = new NodeWithIndexableChildren<TAccumulate>(accumulate);
              var nodeAndPosition = new NodeAndPosition<TAccumulate>(accumulate, token.Value.Position);

              if (tree.Count == 0)
                tree.Push(new Stack<(NodeWithIndexableChildren<TAccumulate>, NodeAndPosition<TAccumulate>)>());

              tree.Peek().Push((node, nodeAndPosition));
              break;
            }
        }
      }

      return tree.Pop().Select(x => x.Node);
    }
  }
}
