using Arborist.Treenumerables;

namespace Arborist.Trees
{
  public class CollatzTree : IndexableTreenumerable<ulong, CollatzTreeNode>
  {
    public CollatzTree() : base(_Roots)
    {
    }

    private static IEnumerable<CollatzTreeNode> _Roots =
      new CollatzTreeNode[] { new CollatzTreeNode(2) };
  }
}
