using Arborist.Core;
using Arborist.Treenumerables;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arborist.SimpleSerializer
{
  public static class TreeSerializer
  {
    public static ITreenumerable<string> Deserialize(string tree)
      => Deserialize(tree, value => value);

    public static ITreenumerable<TValue> Deserialize<TValue>(
      string tree,
      Func<string, TValue> map)
    {
      // Adapt the string map to a span map: each value is materialized once (chars.ToString()),
      // exactly as before. Callers that don't need a string should use the SpanMap overload below.
      SpanMap<TValue> spanMap = chars => map(chars.ToString());
      return new SimpleNodeTreenumerable<TValue>(DeserializeRoots(tree, spanMap));
    }

    // Span overload: the map receives each value as a slice of the source text (no intermediate
    // string), so deserializing into non-string values (e.g. int.Parse(chars)) allocates no value
    // strings at all.
    public static ITreenumerable<TValue> Deserialize<TValue>(
      string tree,
      SpanMap<TValue> map)
      => new SimpleNodeTreenumerable<TValue>(DeserializeRoots(tree, map));

    // Fused parse: tokenize and build in one char pass, slicing each value straight off the source as
    // a ReadOnlySpan<char> -- no StringBuilder, no intermediate Token stream.
    private static IEnumerable<SimpleNode<TValue>> DeserializeRoots<TValue>(
      string tree,
      SpanMap<TValue> map)
    {
      var stack = new Stack<List<SimpleNode<TValue>>>();
      stack.Push(new List<SimpleNode<TValue>>());

      var valueStart = -1; // start index of the current value run; -1 = no value pending

      for (int i = 0; i < tree.Length; i++)
      {
        var character = tree[i];

        switch (character)
        {
          case '(':
          case ')':
          case ',':
            if (valueStart >= 0)
            {
              stack.Peek().Add(new SimpleNode<TValue>(map(tree.AsSpan(valueStart, i - valueStart))));
              valueStart = -1;
            }

            if (character == '(')
            {
              stack.Push(new List<SimpleNode<TValue>>());
            }
            else if (character == ')')
            {
              var children = stack.Pop();
              var nodes = stack.Peek();
              nodes[nodes.Count - 1] = new SimpleNode<TValue>(nodes[nodes.Count - 1].Value, children);
            }
            // ',' just ends the pending value.
            break;

          default:
            if (valueStart < 0)
              valueStart = i;
            break;
        }
      }

      if (valueStart >= 0)
        stack.Peek().Add(new SimpleNode<TValue>(map(tree.AsSpan(valueStart, tree.Length - valueStart))));

      return stack.Pop();
    }

    public static string Serialize(this ITreenumerable<string> treenumerable)
      => Serialize(treenumerable, node => node);

    public static string Serialize<TNode>(
      this ITreenumerable<TNode> treenumerable,
      Func<TNode, string> map)
    {
      var builder = new StringBuilder();

      using (var treenumerator = treenumerable.GetDepthFirstTreenumerator())
      {
        int previousDepth = -1;

        while (treenumerator.MoveNext(NodeTraversalStrategies.TraverseAll))
        {
          if (treenumerator.VisitCount != 1)
            continue;

          var depth = treenumerator.Position.Depth;

          if (previousDepth != -1)
          {
            if (depth > previousDepth)
            {
              builder.Append('(');
            }
            else if (depth < previousDepth)
            {
              for (int i = 0; i < previousDepth - depth; i++)
                builder.Append(')');

              builder.Append(',');
            }
            else
            {
              builder.Append(',');
            }
          }

          builder.Append(map(treenumerator.Node));

          previousDepth = treenumerator.Position.Depth;
        }

        while (previousDepth-- > 0)
          builder.Append(')');

        return builder.ToString();
      }
    }
  }
}
