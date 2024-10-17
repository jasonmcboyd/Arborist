using Arborist.Core;
using System.Collections.Generic;

namespace Arborist.Linq
{
  internal static class TreeInverter
  {
    public static IEnumerable<SimpleNode<TNode>> Invert<TNode>(ITreenumerable<TNode> source)
    {
      var nodeStack = new Stack<Stack<SimpleNode<TNode>>>();

      nodeStack.Push(new Stack<SimpleNode<TNode>>());

      var stacks = new Stack<Stack<SimpleNode<TNode>>>();

      using (var treenumerator = source.GetDepthFirstTreenumerator())
      {
        while (treenumerator.MoveNext(NodeTraversalStrategy.TraverseSubtree))
        {
          if (treenumerator.Mode == TreenumeratorMode.VisitingNode)
            continue;

          if (treenumerator.Position.Depth > nodeStack.Count - 1)
          {
            var childStack = stacks.Count > 0 ? stacks.Pop() : new Stack<SimpleNode<TNode>>();

            childStack.Push(new SimpleNode<TNode>(treenumerator.Node));

            nodeStack.Push(childStack);
          }
          else if (treenumerator.Position.Depth < nodeStack.Count - 1)
          {
            var childrenStack = nodeStack.Pop();

            var children = new SimpleNode<TNode>[childrenStack.Count];

            for (var i = 0; i < children.Length; i++)
              children[i] = childrenStack.Pop();

            stacks.Push(childrenStack);

            var node = nodeStack.Peek().Pop();
            nodeStack.Peek().Push(new SimpleNode<TNode>(node.Value, children));

            nodeStack.Peek().Push(new SimpleNode<TNode>(treenumerator.Node));
          }
          else
          {
            nodeStack.Peek().Push(new SimpleNode<TNode>(treenumerator.Node));
          }
        }
      }

      while (nodeStack.Count > 1)
      {
        var childrenStack = nodeStack.Pop();

        var children = new SimpleNode<TNode>[childrenStack.Count];

        for (var i = 0; i < children.Length; i++)
          children[i] = childrenStack.Pop();

        stacks.Push(childrenStack);

        var node = new SimpleNode<TNode>(nodeStack.Peek().Pop().Value, children);

        nodeStack.Peek().Push(node);
      }

      while (nodeStack.Peek().Count > 0)
        yield return nodeStack.Peek().Pop();
    }
  }
}
