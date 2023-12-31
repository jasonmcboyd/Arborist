﻿namespace Arborist.Linq.Treenumerators
{
  internal class RepeatDepthFirstTreenumerator<TNode> : TreenumeratorBase<TNode>
  {
    public RepeatDepthFirstTreenumerator(
      ITreenumerable<TNode> treenumerable)
    {
      _Treenumerable = treenumerable;
    }

    public RepeatDepthFirstTreenumerator(
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

    protected override bool OnMoveNext(bool skipChildren)
    {
      if (Treenumerator == null)
      {
        if (skipChildren)
          return false;

        Treenumerator = _Treenumerable.GetDepthFirstTreenumerator();

        _TreenumeratorCount++;

        if (_Count != null && (_TreenumeratorCount - 1) > _Count)
          return false;
      }

      while (!Treenumerator.MoveNext(skipChildren))
      {
        Treenumerator = _Treenumerable.GetDepthFirstTreenumerator();

        _TreenumeratorCount++;

        if (_Count != null && (_TreenumeratorCount - 1) > _Count)
          return false;

        skipChildren = false;
      }

      Current = Treenumerator.Current;

      if (Current.Depth == 0)
      {
        if (Current.VisitCount == 1)
          _RootSiblingCount++;

        Current = Current.WithSiblingIndex(_RootSiblingCount);
      }

      return true;
    }

    public override void Dispose()
    {
      Treenumerator?.Dispose();
    }
  }
}
