using System;
using System.Collections.Generic;

namespace Arborist.Tests.Utils
{
  public class MoveNextResult<TNode>
  {
    public MoveNextResult(
      TreenumeratorState state,
      TNode node,
      int visitCount,
      NodePosition originalPosition,
      NodePosition position)
    {
      State = state;
      Node = node;
      VisitCount = visitCount;
      OriginalPosition = originalPosition;
      Position = position;
    }

    public TreenumeratorState State { get; }
    public TNode Node { get; }
    public int VisitCount { get; }
    public NodePosition OriginalPosition { get; }
    public NodePosition Position { get; }

    public override bool Equals(object obj)
    {
      if (!(obj is MoveNextResult<TNode> other))
        return false;

      return
        State == other.State
        && Equals(other.Node, Node)
        && other.VisitCount == VisitCount
        && other.OriginalPosition == OriginalPosition
        && other.Position == Position;
    }

    public override int GetHashCode() => (State, Node, VisitCount, OriginalPosition).GetHashCode();

    public static implicit operator MoveNextResult<TNode>((TreenumeratorState, TNode, int, NodePosition) tuple)
      => new MoveNextResult<TNode>(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, default);

    public override string ToString()
    {
      return $"{TreenumeratorStateMap.ToChar(State)}, {Node}, {VisitCount}, ({OriginalPosition.SiblingIndex}, {OriginalPosition.Depth})";
    }
  }

  public static class MoveNextResult
  {
    public static MoveNextResult<TNode> Create<TNode>(TreenumeratorState state, NodeVisit<TNode> visit)
      => new MoveNextResult<TNode>(state, visit.Node, visit.VisitCount, visit.OriginalPosition, default);

    public static IEnumerable<MoveNextResult<TNode>> ToDepthFirstMoveNext<TNode>(
      this ITreenumerable<TNode> source)
      => source.ToDepthFirstMoveNext(_ => SchedulingStrategy.ScheduleForTraversal);

    public static IEnumerable<MoveNextResult<TNode>> ToDepthFirstMoveNext<TNode>(
      this ITreenumerable<TNode> source,
      Func<NodeVisit<TNode>, SchedulingStrategy> schedulingStrategySelector)
    {
      using (var enumerator = source.GetDepthFirstTreenumerator())
      {
        NodeVisit<TNode>? previous = null;

        var schedulingStrategy = SchedulingStrategy.ScheduleForTraversal;

        while (enumerator.MoveNext(schedulingStrategy))
        {
          var visit =
            enumerator.State == TreenumeratorState.EnumerationFinished
            || enumerator.State == TreenumeratorState.EnumerationNotStarted
            ? default
            : enumerator.Current;

          yield return Create(enumerator.State, visit);

          previous = enumerator.Current;

          schedulingStrategy = schedulingStrategySelector(previous.Value);
        }
      }
    }

    public static IEnumerable<MoveNextResult<TNode>> ToBreadthFirstMoveNext<TNode>(
      this ITreenumerable<TNode> source)
      => source.ToBreadthFirstMoveNext(_ => SchedulingStrategy.ScheduleForTraversal);

    public static IEnumerable<MoveNextResult<TNode>> ToBreadthFirstMoveNext<TNode>(
      this ITreenumerable<TNode> source,
      Func<NodeVisit<TNode>, SchedulingStrategy> schedulingStrategySelector)
    {
      using (var enumerator = source.GetBreadthFirstTreenumerator())
      {
        NodeVisit<TNode>? previous = null;

        var schedulingStrategy = SchedulingStrategy.ScheduleForTraversal;

        while (enumerator.MoveNext(schedulingStrategy))
        {
          var visit =
            enumerator.State == TreenumeratorState.EnumerationFinished
            || enumerator.State == TreenumeratorState.EnumerationNotStarted
            ? default
            : enumerator.Current;

          yield return Create(enumerator.State, visit);

          previous = enumerator.Current;

          schedulingStrategy = schedulingStrategySelector(previous.Value);
        }
      }
    }
  }
}
