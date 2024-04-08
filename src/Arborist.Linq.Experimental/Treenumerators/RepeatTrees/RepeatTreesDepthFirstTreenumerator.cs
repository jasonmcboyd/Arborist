using Arborist.Core;
using System;

namespace Arborist.Linq.Experimental.Treenumerators
{
  internal class RepeatTreesDepthFirstTreenumerator<TNode> : TreenumeratorBase<TNode>
  {
    public RepeatTreesDepthFirstTreenumerator(
      ITreenumerable<TNode> treenumerable)
    {
      _Treenumerable = treenumerable;
    }

    public RepeatTreesDepthFirstTreenumerator(
      ITreenumerable<TNode> treenumerable,
      int count)
    {
      _Treenumerable = treenumerable;
      _Count = count;
    }

    private readonly int? _Count;
    private int _TreenumeratorCount = 0;
    private int _RootSiblingCount = -1;

    private readonly ITreenumerable<TNode> _Treenumerable;

    private ITreenumerator<TNode> _Treenumerator;
    private ITreenumerator<TNode> Treenumerator
    {
      get => _Treenumerator;
      set
      {
        _Treenumerator?.Dispose();

        _Treenumerator = value;
      }
    }

    protected override bool OnMoveNext(TraversalStrategy traversalStrategy)
    {
      throw new NotImplementedException();
      //if (Treenumerator == null)
      //{
      //  if (traversalStrategy == TraversalStrategy.SkipSubtree)
      //    return false;

      //  Treenumerator = _Treenumerable.GetDepthFirstTreenumerator();

      //  _TreenumeratorCount++;

      //  if (_Count != null && (_TreenumeratorCount - 1) > _Count)
      //    return false;
      //}

      //while (!Treenumerator.MoveNext(traversalStrategy))
      //{
      //  Treenumerator = _Treenumerable.GetDepthFirstTreenumerator();

      //  _TreenumeratorCount++;

      //  if (_Count != null && (_TreenumeratorCount - 1) > _Count)
      //    return false;

      //  traversalStrategy = TraversalStrategy.ScheduleForTraversal;
      //}

      //Current = Treenumerator.Node;

      //if (Current.OriginalPosition.Depth == 0)
      //{
      //  if (Current.VisitCount == 1)
      //    _RootSiblingCount++;

      //  Current = Current.WithSiblingIndex(_RootSiblingCount);
      //}

      //return true;
    }

    public override void Dispose()
    {
      Treenumerator?.Dispose();
    }
  }
}
