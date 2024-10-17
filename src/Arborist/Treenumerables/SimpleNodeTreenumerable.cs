using System.Collections.Generic;

namespace Arborist.Treenumerables
{
  public class SimpleNodeTreenumerable<TNode> : Treenumerable<TNode, SimpleNode<TNode>, SimpleNodeChildEnumerator<TNode>>
  {
    public SimpleNodeTreenumerable(IEnumerable<SimpleNode<TNode>> roots)
      : base(
        node => node.GetChildEnumerator(),
        SimpleNodeDelegates.MoveNextChild,
        SimpleNodeDelegates.DisposeChildEnumerator,
        node => node.Value,
        roots)
    {
    }
  }
}
