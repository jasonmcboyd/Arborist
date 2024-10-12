using Arborist.Core;
using Arborist.Treenumerators;
using System;
using System.Collections.Generic;

namespace Arborist.Treenumerables
{
  public class Treenumerable<TValue, TNode, TChildEnumerator> : ITreenumerable<TValue>
  {
    public Treenumerable(
      Func<TNode, TChildEnumerator> childEnumeratorFactory,
      MoveNextChildDelegate<TChildEnumerator, TNode> tryMoveNextChildDelegate,
      DisposeChildEnumeratorDelegate<TChildEnumerator> disposeChildEnumeratorDelegate,
      Func<TNode, TValue> nodeToValueMap,
      params TNode[] roots)
      : this(
          childEnumeratorFactory,
          tryMoveNextChildDelegate,
          disposeChildEnumeratorDelegate,
          nodeToValueMap,
          roots as IEnumerable<TNode>)
    {
    }

    public Treenumerable(
      Func<TNode, TChildEnumerator> childEnumeratorFactory,
      MoveNextChildDelegate<TChildEnumerator, TNode> tryMoveNextChildDelegate,
      DisposeChildEnumeratorDelegate<TChildEnumerator> disposeChildEnumeratorDelegate,
      Func<TNode, TValue> nodeToValueMap,
      IEnumerable<TNode> roots)
    {
      _ChildEnumeratorFactory = childEnumeratorFactory;
      _MoveNextChildDelegate = tryMoveNextChildDelegate;
      _DisposeChildEnumeratorDelegate = disposeChildEnumeratorDelegate;
      _NodeToValueMap = nodeToValueMap;
      _Roots = roots;
    }

    private readonly IEnumerable<TNode> _Roots;
    private readonly Func<TNode, TChildEnumerator> _ChildEnumeratorFactory;
    private readonly MoveNextChildDelegate<TChildEnumerator, TNode> _MoveNextChildDelegate;
    private readonly DisposeChildEnumeratorDelegate<TChildEnumerator> _DisposeChildEnumeratorDelegate;
    private readonly Func<TNode, TValue> _NodeToValueMap;

    public ITreenumerator<TValue> GetBreadthFirstTreenumerator()
    {
      return
        new BreadthFirstTreenumerator<TValue, TNode, TChildEnumerator>(
          _Roots,
          _ChildEnumeratorFactory,
          _MoveNextChildDelegate,
          _DisposeChildEnumeratorDelegate,
          _NodeToValueMap);
    }

    public ITreenumerator<TValue> GetDepthFirstTreenumerator()
    {
      return
        new DepthFirstTreenumerator<TValue, TNode, TChildEnumerator>(
          _Roots,
          _ChildEnumeratorFactory,
          _MoveNextChildDelegate,
          _DisposeChildEnumeratorDelegate,
          _NodeToValueMap);
    }
  }
}
