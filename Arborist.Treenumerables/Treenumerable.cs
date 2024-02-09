using Arborist.Treenumerables.Treenumerators;
using System;
using System.Collections.Generic;

namespace Arborist.Treenumerables
{
  public class Treenumerable<TNode>
    : ITreenumerable<TNode>
  {
    public Treenumerable(IEnumerable<INodeWithEnumerableChildren<TNode>> roots)
    {
      _Roots = roots;
    }

    private readonly IEnumerable<INodeWithEnumerableChildren<TNode>> _Roots;

    public ITreenumerator<TNode> GetBreadthFirstTreenumerator()
      => new BreadthFirstTreenumerator<TNode>(_Roots);

    public ITreenumerator<TNode> GetDepthFirstTreenumerator()
      => new DepthFirstTreenumerator<TNode>(_Roots);
  }
}
