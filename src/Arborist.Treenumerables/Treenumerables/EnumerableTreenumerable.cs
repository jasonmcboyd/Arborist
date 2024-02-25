using Arborist.Core;
using Arborist.Treenumerables.Nodes;
using Arborist.Treenumerables.Treenumerators;
using System.Collections.Generic;

namespace Arborist.Treenumerables.Treenumerables
{
  public class EnumerableTreenumerable<TNode> : ITreenumerable<TNode>
  {
    public EnumerableTreenumerable(params INodeContainerWithEnumerableChildren<TNode>[] roots)
    {
      _Roots = roots;
    }

    public EnumerableTreenumerable(IEnumerable<INodeContainerWithEnumerableChildren<TNode>> roots)
    {
      _Roots = roots;
    }

    private readonly IEnumerable<INodeContainerWithEnumerableChildren<TNode>> _Roots;

    public ITreenumerator<TNode> GetBreadthFirstTreenumerator()
    {
      return
        new BreadthFirstTreenumerator<INodeContainerWithEnumerableChildren<TNode>, TNode>(
          _Roots,
          node => node.Value,
          node => node.Children.GetEnumerator());
    }

    public ITreenumerator<TNode> GetDepthFirstTreenumerator()
    {
      return
        new DepthFirstTreenumerator<INodeContainerWithEnumerableChildren<TNode>, TNode>(
          _Roots,
          node => node.Value,
          node => node.Children.GetEnumerator());
    }
  }
}
