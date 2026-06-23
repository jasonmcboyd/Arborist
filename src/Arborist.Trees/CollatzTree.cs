using Arborist.Treenumerables;

namespace Arborist.Trees
{
  public class CollatzTree : Treenumerable<ulong, CollatzTreeNodeChildEnumerator>
  {
    public CollatzTree()
      : base(
          nodeContext => new CollatzTreeNodeChildEnumerator(nodeContext.Node),
          new ulong[] { 2 })
    {
    }
  }
}
