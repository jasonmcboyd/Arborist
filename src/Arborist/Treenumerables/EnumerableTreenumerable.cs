using Arborist.Core;
using Arborist.Nodes;
using Arborist.Treenumerators;
using System.Collections.Generic;

namespace Arborist.Treenumerables
{
  public class EnumerableTreenumerable<TNode> : ITreenumerable<TNode>
  {
    public EnumerableTreenumerable(params INodeWithEnumerableChildren<TNode>[] roots)
    {
      _Roots = roots;
    }

    public EnumerableTreenumerable(IEnumerable<INodeWithEnumerableChildren<TNode>> roots)
    {
      _Roots = roots;
    }

    private readonly IEnumerable<INodeWithEnumerableChildren<TNode>> _Roots;

    public ITreenumerator<TNode> GetBreadthFirstTreenumerator()
    {
      return
        new BreadthFirstTreenumerator<INodeWithEnumerableChildren<TNode>, TNode>(
          _Roots,
          node => node.Value,
          node => node.Children.GetEnumerator());
    }

    public ITreenumerator<TNode> GetDepthFirstTreenumerator()
    {
      return
        new DepthFirstTreenumerator<INodeWithEnumerableChildren<TNode>, TNode>(
          _Roots,
          node => node.Value,
          node => node.Children.GetEnumerator());
    }
  }
}
