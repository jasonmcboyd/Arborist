using Arborist.Core;
using Arborist.Linq.Enumerables;
using Arborist.Treenumerators;
using System;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerables
{
  internal class EnumerableTreenumerable<TNode> : ITreenumerable<TNode>
  {
    public EnumerableTreenumerable(
      IEnumerable<TNode> source, 
      Func<NodeContext<TNode>, bool> childAdderStrategy)
    {
      _Source = source;
      _ChildAdderStrategy = childAdderStrategy;
    }

    private readonly IEnumerable<TNode> _Source;
    private readonly Func<NodeContext<TNode>, bool> _ChildAdderStrategy;

    public ITreenumerator<TNode> GetBreadthFirstTreenumerator()
    {
      var sharedEnumerable = new LookAheadSharedEnumerable<TNode>(_Source);

      return
        new BreadthFirstTreenumerator<TNode, TNode, SharedEnumerableChildEnumerator>(
          sharedEnumerable,
          nodeContext => new SharedEnumerableChildEnumerator(nodeContext.Position.Depth, sharedEnumerable, _ChildAdderStrategy),
          node => node);
    }

    public ITreenumerator<TNode> GetDepthFirstTreenumerator()
    {
      var sharedEnumerable = new LookAheadSharedEnumerable<TNode>(_Source);

      return
        new DepthFirstTreenumerator<TNode, TNode, SharedEnumerableChildEnumerator>(
          sharedEnumerable,
          nodeContext => new SharedEnumerableChildEnumerator(nodeContext.Position.Depth, sharedEnumerable, _ChildAdderStrategy),
          node => node);
    }

    internal struct SharedEnumerableChildEnumerator: IChildEnumerator<TNode>
    {
      public SharedEnumerableChildEnumerator(
        int parentNodeDepth,
        LookAheadSharedEnumerable<TNode> source,
        Func<NodeContext<TNode>, bool> childAdderStrategy)
      {
        _ParentNodeDepth = parentNodeDepth;
        _SharedEnumerator = source.GetSharedEnumerator();
        _ChildAdderStrategy = childAdderStrategy;
        _CurrentSiblingIndex = 0;
      }

      private static readonly Random _Random = new Random();

      private readonly int _ParentNodeDepth;
      private readonly LookAheadSharedEnumerator<TNode> _SharedEnumerator;
      private readonly Func<NodeContext<TNode>, bool> _ChildAdderStrategy;
      private int _CurrentSiblingIndex;

      public bool MoveNext(out NodeAndSiblingIndex<TNode> childNodeAndSiblingIndex)
      {
        if (!_SharedEnumerator.PeekNext(out var childNode))
        {
          childNodeAndSiblingIndex = default;
          return false;
        }

        var position = new NodePosition(_CurrentSiblingIndex, _ParentNodeDepth + 1);

        var hasNext = _ChildAdderStrategy(new NodeContext<TNode>(childNode, position));

        if (!hasNext || !_SharedEnumerator.MoveNext())
        {
          childNodeAndSiblingIndex = default;
          return false;
        }

        childNodeAndSiblingIndex = new NodeAndSiblingIndex<TNode>(_SharedEnumerator.Current, _CurrentSiblingIndex);

        _CurrentSiblingIndex++;

        return true;
      }

      public void Dispose() => _SharedEnumerator.Dispose();
    }
  }
}
