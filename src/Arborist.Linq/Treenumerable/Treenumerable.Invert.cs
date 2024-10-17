using Arborist.Core;
using Arborist.Treenumerables;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<TNode> Invert<TNode>(
      this ITreenumerable<TNode> source)
      => new SimpleNodeTreenumerable<TNode>(TreeInverter.Invert(source));
  }
}
