using System.Collections.Generic;

namespace Arborist.Treenumerables
{
  public class SimpleNodeTreenumerable<TNode>
    : Treenumerable<TNode, SimpleNode<TNode>, SimpleNodeChildEnumerator<TNode>>
  {
    private IEnumerable<SimpleNode<object>> _Enumerable;

    public SimpleNodeTreenumerable(IEnumerable<SimpleNode<TNode>> roots)
      : base(
        nodeContext => nodeContext.Node.GetChildEnumerator(),
        node => node.Value,
        roots)
    {
    }
  }
}
