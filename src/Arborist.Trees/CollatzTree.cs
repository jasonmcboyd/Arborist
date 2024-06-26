using Arborist.Nodes;
using Arborist.Treenumerables;

namespace Arborist.Trees
{
  public class CollatzTree : IndexableTreenumerable<ulong>
  {
    public CollatzTree() : base(_Roots)
    {
    }

    private static IEnumerable<INodeWithIndexableChildren<ulong>> _Roots =
      new INodeWithIndexableChildren<ulong>[] { new CollatzTreeNode(2) };
  }
}
