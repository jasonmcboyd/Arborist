using Arborist.Core;
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

    public static implicit operator MoveNextResult<TNode>((TreenumeratorState, TNode, int, NodePosition, NodePosition) tuple)
      => new MoveNextResult<TNode>(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);

    public override string ToString()
    {
      return $"{TreenumeratorStateMap.ToChar(State)} {Node} {VisitCount} {OriginalPosition} {Position}";
    }
  }

  public static class MoveNextResult
  {
    public static MoveNextResult<TNode> Create<TNode>(ITreenumerator<TNode> treenumerator)
      => new MoveNextResult<TNode>(
        treenumerator.State,
        treenumerator.Node,
        treenumerator.VisitCount,
        treenumerator.OriginalPosition,
        treenumerator.Position);

    public static IEnumerable<MoveNextResult<TNode>> ToDepthFirstMoveNext<TNode>(
      this ITreenumerable<TNode> source)
      => source.ToDepthFirstMoveNext(_ => SchedulingStrategy.TraverseSubtree);

    public static IEnumerable<MoveNextResult<TNode>> ToDepthFirstMoveNext<TNode>(
      this ITreenumerable<TNode> source,
      Func<ITreenumerator<TNode>, SchedulingStrategy> schedulingStrategySelector)
    {
      using (var treenumerator = source.GetDepthFirstTreenumerator())
      {
        var schedulingStrategy = SchedulingStrategy.TraverseSubtree; 
        while (treenumerator.MoveNext(schedulingStrategy))
        {
          yield return Create(treenumerator);

          schedulingStrategy = schedulingStrategySelector(treenumerator);
        }

        yield break;
      }
    }

    public static IEnumerable<MoveNextResult<TNode>> ToBreadthFirstMoveNext<TNode>(
      this ITreenumerable<TNode> source)
      => source.ToBreadthFirstMoveNext(_ => SchedulingStrategy.TraverseSubtree);

    public static IEnumerable<MoveNextResult<TNode>> ToBreadthFirstMoveNext<TNode>(
      this ITreenumerable<TNode> source,
      Func<ITreenumerator<TNode>, SchedulingStrategy> schedulingStrategySelector)
    {
      using (var treenumerator = source.GetBreadthFirstTreenumerator())
      {
        var schedulingStrategy = SchedulingStrategy.TraverseSubtree; 

        while (treenumerator.MoveNext(schedulingStrategy))
        {
          yield return Create(treenumerator);

          schedulingStrategy = schedulingStrategySelector(treenumerator);
        }

        yield break;
      }
    }
  }
}
