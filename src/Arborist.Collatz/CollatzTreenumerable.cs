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

    private static IEnumerable<INodeContainerWithIndexableChildren<ulong>> _Roots =
      new INodeContainerWithIndexableChildren<ulong>[] { new CollatzNode(2) };
  }
}
