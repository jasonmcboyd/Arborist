using Arborist.Treenumerables;

namespace Arborist.Trees
{
  public class CollatzTree : Treenumerable<ulong, ulong, CollatzTreeNodeChildEnumerator>
  {
    public CollatzTree()
      : base(
          nodeContext => new CollatzTreeNodeChildEnumerator(nodeContext.Node),
          node => node,
          new ulong[] { 2 })
    {
    }
  }
}
