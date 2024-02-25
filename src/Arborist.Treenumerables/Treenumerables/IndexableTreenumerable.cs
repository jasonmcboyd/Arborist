﻿using Arborist.Core;
using Arborist.Treenumerables.Nodes;
using Arborist.Treenumerables.Treenumerators;
using Arborist.Treenumerables.Virtualization;
using System.Collections.Generic;

namespace Arborist.Treenumerables.Treenumerables
{
  public class IndexableTreenumerable<TNode> : ITreenumerable<TNode>
  {
    public IndexableTreenumerable(params INodeContainerWithIndexableChildren<TNode>[] roots)
    {
      _Roots = roots;
    }

    public IndexableTreenumerable(IEnumerable<INodeContainerWithIndexableChildren<TNode>> roots)
    {
      _Roots = roots;
    }

    private readonly IEnumerable<INodeContainerWithIndexableChildren<TNode>> _Roots;

    public ITreenumerator<TNode> GetBreadthFirstTreenumerator()
    {
      var pool = new VirtualEnumeratorPool<TNode>();

      return
        new BreadthFirstTreenumerator<INodeContainerWithIndexableChildren<TNode>, TNode>(
          _Roots,
          node => node.Value,
          node => pool.Lease(node));
    }

    public ITreenumerator<TNode> GetDepthFirstTreenumerator()
    {
      var pool = new VirtualEnumeratorPool<TNode>();

      return
        new DepthFirstTreenumerator<INodeContainerWithIndexableChildren<TNode>, TNode>(
          _Roots,
          node => node.Value,
          node => pool.Lease(node));
    }
  }
}
