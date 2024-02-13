using Arborist.Treenumerables.Treenumerators;
using System;
using System.Collections.Generic;

namespace Arborist.Treenumerables
{
  internal class Treenumerable<TNode>
    : ITreenumerable<TNode>
  {
    public Treenumerable(
      IEnumerable<TNode> roots,
      Func<TNode, IEnumerator<TNode>> childrenGetter)
    {
      _Roots = roots;
      _ChildrenGetter = childrenGetter;
    }

    private readonly IEnumerable<TNode> _Roots;
    private readonly Func<TNode, IEnumerator<TNode>> _ChildrenGetter;

    public ITreenumerator<TNode> GetBreadthFirstTreenumerator()
      => new BreadthFirstTreenumerator<TNode>(_Roots, _ChildrenGetter);

    public ITreenumerator<TNode> GetDepthFirstTreenumerator()
      => new DepthFirstTreenumerator<TNode>(_Roots, _ChildrenGetter);
  }
}
