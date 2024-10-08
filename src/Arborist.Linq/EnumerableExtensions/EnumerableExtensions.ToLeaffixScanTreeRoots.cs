﻿using Arborist.Core;
using Arborist.Linq;
using Arborist.Linq.TreeEnumerable.DepthFirstTree;
using Arborist.Linq.Treenumerators.Enumerator;
using Arborist.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Linq
{
  public static partial class EnumerableExtensions
  {
    internal static IEnumerable<NodeWithIndexableChildren<TAccumulate>> ToLeaffixScanTreeRoots<TSource, TAccumulate>(
      this IEnumerable<DepthFirstTreeEnumerableToken<NodeContext<TSource>>> source,
      Func<NodeContext<TAccumulate>, NodeContext<TSource>, TAccumulate> seedAccumulator,
      Func<NodeContext<TAccumulate>, NodeContext<TSource>, TAccumulate> initialAccumulator,
      Func<NodeContext<TAccumulate>, NodeContext<TAccumulate>, TAccumulate> accumulator,
      Func<NodeContext<TSource>, TAccumulate> seedGenerator)
    {
      var tokens = source.ToArray();

      var tree = new Stack<Stack<(NodeWithIndexableChildren<TAccumulate> Node, NodeContext<TAccumulate> NodeAndPosition)>>();

      // Traverse the tokens in reverse order and build the tree up to the root nodes while aggregating the values.
      for (var i = tokens.Length - 1; i >= 0; i--)
      {
        var token = tokens[i];

        switch (token.Type)
        {
          case DepthFirstTreeEnumerableTokenType.StartChildGroup:
            {
              var children = tree.Pop();
              i--;
              token = tokens[i];
              var parent = token.Node;

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
                  accumulate = accumulator(child.NodeAndPosition, new NodeContext<TAccumulate>(accumulate, parent.Position));

                childArray[processedChildrenCount] = child.Node;
              }

              tree.Peek().Push((new NodeWithIndexableChildren<TAccumulate>(accumulate, childArray), new NodeContext<TAccumulate>(accumulate, parent.Position)));
              break;
            }

          case DepthFirstTreeEnumerableTokenType.EndChildGroup:
            {
              tree.Push(new Stack<(NodeWithIndexableChildren<TAccumulate>, NodeContext<TAccumulate>)>());
              if (tree.Count == 1)
                tree.Push(new Stack<(NodeWithIndexableChildren<TAccumulate>, NodeContext<TAccumulate>)>());
              break;
            }

          default:
            {
              var seed = seedGenerator(token.Node);
              var seedNodeAndPosition = new NodeContext<TAccumulate>(seed, token.Node.Position);
              var accumulate = seedAccumulator(seedNodeAndPosition, token.Node);
              var node = new NodeWithIndexableChildren<TAccumulate>(accumulate);
              var nodeAndPosition = new NodeContext<TAccumulate>(accumulate, token.Node.Position);

              if (tree.Count == 0)
                tree.Push(new Stack<(NodeWithIndexableChildren<TAccumulate>, NodeContext<TAccumulate>)>());

              tree.Peek().Push((node, nodeAndPosition));
              break;
            }
        }
      }

      return tree.Pop().Select(x => x.Node);
    }
  }
}
