using Arborist.Core;
using Arborist.Linq.Extensions;
using Arborist.Linq.Newick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static IEnumerable<string> ToFormattedLines<TNode>(this ITreenumerable<TNode> source)
    {
      return source.ToFormattedLines(node => node.ToString(), 0);
    }

    public static IEnumerable<string> ToFormattedLines<TNode>(
      this ITreenumerable<TNode> source,
      int paddingSize)
    {
      return source.ToFormattedLines(node => node.ToString(), paddingSize);
    }

    public static IEnumerable<string> ToFormattedLines<TNode>(
      this ITreenumerable<TNode> source,
      Func<TNode, string> stringFormatter,
      int paddingSize)
    {
      var reverseNewickEnumerable = source.ToNewickEnumerable().Reverse();

      const char BAR_NODE = '│';
      const char INTERIOR_BRANCH_NODE = '├';
      const char EXTERIOR_BRANCH_NODE = '└';
      const char WHITESPACE_NODE = ' ';
      const char BRANCH_PADDING = '─';

      var branchPadding = new string(BRANCH_PADDING, paddingSize);
      var whitespacePadding = new string(WHITESPACE_NODE, paddingSize);

      var nodes = new List<char>();
      var depth = 0;

      var results = new Stack<string>();

      var builder = new StringBuilder();

      // TODO: I think I can process one tree at a time, instead of all trees at once.
      foreach (var token in reverseNewickEnumerable)
      {
        switch (token.Type)
        {
          case NewickTokenType.EndChildGroup:
            depth++;

            if (nodes.Count > 0 && (nodes.Last() == INTERIOR_BRANCH_NODE || nodes.Last() == EXTERIOR_BRANCH_NODE))
              nodes.ReplaceLast(BAR_NODE);

            nodes.Add(WHITESPACE_NODE);
            break;

          case NewickTokenType.StartChildGroup:
            depth--;

            builder.Remove(builder.Length - (paddingSize + 1), paddingSize + 1);

            nodes.RemoveLast();
            break;

          default:
            if (nodes.Count == 0)
            {
              results.Push(stringFormatter(token.Value));
              continue;
            }
            var node = nodes.Last();

            if (node == WHITESPACE_NODE)
              nodes.ReplaceLast(EXTERIOR_BRANCH_NODE);
            else if (node == EXTERIOR_BRANCH_NODE || node == BAR_NODE)
              nodes.ReplaceLast(INTERIOR_BRANCH_NODE);

            builder.Clear();

            for (int i = 0; i < nodes.Count; i++)
            {
              node = nodes[i];

              builder.Append(node);

              if (node == BAR_NODE || node == WHITESPACE_NODE)
                builder.Append(whitespacePadding);
              else
                builder.Append(branchPadding);
            }

            builder.Append(stringFormatter(token.Value));

            results.Push(builder.ToString());

            break;
        }
      }

      while (results.Count > 0)
        yield return results.Pop();
    }
  }
}
