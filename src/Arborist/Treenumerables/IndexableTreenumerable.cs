using Arborist.Core;
using Arborist.Nodes;
using Arborist.Treenumerators;
using Arborist.Virtualization;
using System.Collections.Generic;

namespace Arborist.Treenumerables
{
  public class IndexableTreenumerable<TValue, TNode> : ITreenumerable<TValue>
    where TNode : INodeWithIndexableChildren<TValue, TNode>
  {
    public IndexableTreenumerable(params TNode[] roots)
    {
      _Roots = roots;
    }

    public IndexableTreenumerable(IEnumerable<TNode> roots)
    {
      _Roots = roots;
    }

    private readonly IEnumerable<TNode> _Roots;

    public ITreenumerator<TValue> GetBreadthFirstTreenumerator()
    {
      var pool = new VirtualEnumeratorPool<TValue, TNode>();

      return
        new BreadthFirstTreenumerator<TNode, TValue>(
          _Roots,
          node => node.Value,
          node => pool.Lease(node));
    }

    public ITreenumerator<TValue> GetDepthFirstTreenumerator()
    {
      var pool = new VirtualEnumeratorPool<TValue, TNode>();

      return
        new DepthFirstTreenumerator<TNode, TValue>(
          _Roots,
          node => node.Value,
          node => pool.Lease(node));
    }
  }
}
