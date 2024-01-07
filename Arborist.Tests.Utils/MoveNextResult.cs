using System;
using System.Collections.Generic;

namespace Arborist.Tests.Utils
{
  public readonly struct MoveNextResult<TNode>
  {
    public MoveNextResult(
      TreenumeratorState state,
      TNode node,
      int visitCount,
      int siblingIndex,
      int depth)
    {
      State = state;
      Node = node;
      VisitCount = visitCount;
      SiblingIndex = siblingIndex;
      Depth = depth;
    }

    public TreenumeratorState State { get; }
    public TNode Node { get; }
    public int VisitCount { get; }
    public int SiblingIndex { get; }
    public int Depth { get; }

    public override bool Equals(object obj)
    {
      if (!(obj is MoveNextResult<TNode> other))
        return false;

      return
        State == other.State
        && Equals(other.Node, Node)
        && other.VisitCount == VisitCount
        && other.SiblingIndex == SiblingIndex
        && other.Depth == Depth;
    }

    public override int GetHashCode() => (State, Node, VisitCount, SiblingIndex, Depth).GetHashCode();

    public static implicit operator MoveNextResult<TNode>((TreenumeratorState, TNode, int, int, int) tuple)
      => new MoveNextResult<TNode>(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);

    public override string ToString()
    {
      return $"{State}, {Node}, {VisitCount}, {SiblingIndex}, {Depth}";
    }
  }

  public static class MoveNextResult
  {
    public static MoveNextResult<TNode> Create<TNode>(TreenumeratorState state, NodeVisit<TNode> visit)
      => new MoveNextResult<TNode>(state, visit.Node, visit.VisitCount, visit.SiblingIndex, visit.Depth);

    public static IEnumerable<MoveNextResult<TNode>> ToDepthFirstMoveNext<TNode>(
      this ITreenumerable<TNode> source)
      => source.ToDepthFirstMoveNext(_ => ChildStrategy.ScheduleForTraversal);

    public static IEnumerable<MoveNextResult<TNode>> ToDepthFirstMoveNext<TNode>(
      this ITreenumerable<TNode> source,
      Func<NodeVisit<TNode>, ChildStrategy> childStrategySelector)
    {
      using (var enumerator = source.GetDepthFirstTreenumerator())
      {
        NodeVisit<TNode>? previous = null;

        var childStrategy = ChildStrategy.ScheduleForTraversal;

        while (enumerator.MoveNext(childStrategy))
        {
          var visit =
            enumerator.State == TreenumeratorState.EnumerationFinished
            || enumerator.State == TreenumeratorState.EnumerationNotStarted
            ? default
            : enumerator.Current;

          yield return Create(enumerator.State, visit);

          previous = enumerator.Current;

          childStrategy = childStrategySelector(previous.Value);
        }
      }
    }

    public static IEnumerable<MoveNextResult<TNode>> ToBreadthFirstMoveNext<TNode>(
      this ITreenumerable<TNode> source)
      => source.ToBreadthFirstMoveNext(_ => ChildStrategy.ScheduleForTraversal);

    public static IEnumerable<MoveNextResult<TNode>> ToBreadthFirstMoveNext<TNode>(
      this ITreenumerable<TNode> source,
      Func<NodeVisit<TNode>, ChildStrategy> childStrategySelector)
    {
      using (var enumerator = source.GetBreadthFirstTreenumerator())
      {
        NodeVisit<TNode>? previous = null;

        var childStrategy = ChildStrategy.ScheduleForTraversal;

        while (enumerator.MoveNext(childStrategy))
        {
          var visit =
            enumerator.State == TreenumeratorState.EnumerationFinished
            || enumerator.State == TreenumeratorState.EnumerationNotStarted
            ? default
            : enumerator.Current;

          yield return Create(enumerator.State, visit);

          previous = enumerator.Current;

          childStrategy = childStrategySelector(previous.Value);
        }
      }
    }
  }
}
