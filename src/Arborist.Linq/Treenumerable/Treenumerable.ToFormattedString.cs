using Arborist.Core;
using System;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static string ToFormattedString<TNode>(this ITreenumerable<TNode> source)
    {
      return source.ToFormattedString(node => node.ToString(), 0);
    }

    public static string ToFormattedString<TNode>(
      this ITreenumerable<TNode> source,
      int paddingSize)
    {
      return source.ToFormattedString(node => node.ToString(), paddingSize);
    }

    public static string ToFormattedString<TNode>(
      this ITreenumerable<TNode> source,
      Func<TNode, string> stringFormatter,
      int paddingSize)
    {
      return string.Join(Environment.NewLine, source.ToFormattedLines(stringFormatter, paddingSize));
    }
  }
}
