using Arborist.Core;
using Arborist.Treenumerators;
using System;
using System.Collections.Generic;

namespace Arborist.Treenumerables
{
  public class Treenumerable<TValue, TNode, TChildEnumerator> : ITreenumerable<TValue>
    where TChildEnumerator : IChildEnumerator<TNode>
  {
    public Treenumerable(
      Func<NodeContext<TNode>, TChildEnumerator> childEnumeratorFactory,
      Func<TNode, TValue> nodeToValueMap,
      params TNode[] roots)
      : this(
          childEnumeratorFactory,
          nodeToValueMap,
          roots as IEnumerable<TNode>)
    {
    }

    public Treenumerable(
      Func<NodeContext<TNode>, TChildEnumerator> childEnumeratorFactory,
      Func<TNode, TValue> nodeToValueMap,
      IEnumerable<TNode> roots)
    {
      _ChildEnumeratorFactory = childEnumeratorFactory;
      _NodeToValueMap = nodeToValueMap;
      _Roots = roots;
    }

    private readonly IEnumerable<TNode> _Roots;
    private readonly Func<NodeContext<TNode>, TChildEnumerator> _ChildEnumeratorFactory;
    private readonly Func<TNode, TValue> _NodeToValueMap;

    public ITreenumerator<TValue> GetBreadthFirstTreenumerator()
    {
      return
        new BreadthFirstTreenumerator<TValue, TNode, TChildEnumerator>(
          _Roots,
          _ChildEnumeratorFactory,
          _NodeToValueMap);
    }

    public ITreenumerator<TValue> GetDepthFirstTreenumerator()
    {
      return
        new DepthFirstTreenumerator<TValue, TNode, TChildEnumerator>(
          _Roots,
          _ChildEnumeratorFactory,
          _NodeToValueMap);
    }
  }
}
