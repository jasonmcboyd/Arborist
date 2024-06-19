using Arborist.Core;
using Arborist.Nodes;
using Arborist.Treenumerators;
using Arborist.Virtualization;
using System.Collections.Generic;

namespace Arborist.Treenumerables
{
  public class IndexableTreenumerable<TNode> : ITreenumerable<TNode>
  {
    public IndexableTreenumerable(params INodeWithIndexableChildren<TNode>[] roots)
    {
      _Roots = roots;
    }

    public IndexableTreenumerable(IEnumerable<INodeWithIndexableChildren<TNode>> roots)
    {
      _Roots = roots;
    }

    private readonly IEnumerable<INodeWithIndexableChildren<TNode>> _Roots;

    public ITreenumerator<TNode> GetBreadthFirstTreenumerator()
    {
      var pool = new VirtualEnumeratorPool<TNode>();

      return
        new BreadthFirstTreenumerator<INodeWithIndexableChildren<TNode>, TNode>(
          _Roots,
          node => node.Value,
          node => pool.Lease(node));
    }

    public ITreenumerator<TNode> GetDepthFirstTreenumerator()
    {
      var pool = new VirtualEnumeratorPool<TNode>();

      return
        new DepthFirstTreenumerator<INodeWithIndexableChildren<TNode>, TNode>(
          _Roots,
          node => node.Value,
          node => pool.Lease(node));
    }
  }
}
