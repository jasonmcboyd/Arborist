using Arborist.Core;
using System.Linq;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static int CountTrees<TNode>(this ITreenumerable<TNode> source)
      => source.GetRoots().Count();
  }
}
