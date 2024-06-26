using Arborist.Core;
using System.Collections.Generic;

namespace Arborist.Linq.Experimental
{
  public static partial class Treenumerable
  {
    public static IEnumerable<string> PrettyPrint<TNode>(
      this ITreenumerable<TNode> source)
    {
      if (source == null)
      {
        yield return "";
        yield break;
      };

      foreach (var nodeVisit in source.Materialize().PreOrderTraversal())
        yield return nodeVisit.Node.ToString().PadLeft(nodeVisit.Position.Depth * 2 + 1, '-');
    }
  }
}
