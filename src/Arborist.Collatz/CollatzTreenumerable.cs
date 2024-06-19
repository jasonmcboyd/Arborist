using Arborist.Nodes;
using Arborist.Treenumerables;
using System.Collections.Generic;

namespace Arborist.Collatz
{
  public class CollatzTreenumerable : IndexableTreenumerable<ulong>
  {
    public CollatzTreenumerable() : base(_Roots)
    {
    }

    private static IEnumerable<INodeWithIndexableChildren<ulong>> _Roots =
      new INodeWithIndexableChildren<ulong>[] { new CollatzNode(2) };
  }
}
