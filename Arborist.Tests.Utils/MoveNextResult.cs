using System;
using System.Collections.Generic;

namespace Arborist.Tests.Utils
{
  public readonly struct MoveNextResult<TNode>
    where TNode : struct
  {
    public MoveNextResult(
      TNode node,
      int visitCount,
      int siblingIndex,
      int depth)
    {
      Node = node;
      VisitCount = visitCount;
      SiblingIndex = siblingIndex;
      Depth = depth;
    }

    public TNode Node { get; }
    public int VisitCount { get; }
    public int SiblingIndex { get; }
    public int Depth { get; }

    public override bool Equals(object obj)
    {
      if (!(obj is MoveNextResult<TNode> other))
        return false;

      return
        Equals(other.Node, Node)
        && other.VisitCount == VisitCount
        && other.SiblingIndex == SiblingIndex
        && other.Depth == Depth;
    }

    public override int GetHashCode() => (Node, VisitCount, SiblingIndex, Depth).GetHashCode();

    public static implicit operator MoveNextResult<TNode>((TNode, int, int, int) tuple)
      => new MoveNextResult<TNode>(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);

    public override string ToString()
    {
      return $"{Node}, {VisitCount}, {SiblingIndex}, {Depth}";
    }
  }

  public static class MoveNextResult
  {
    public static MoveNextResult<TNode> Create<TNode>(NodeVisit<TNode> visit)
      where TNode : struct
      => new MoveNextResult<TNode>(visit.Node, visit.VisitCount, visit.SiblingIndex, visit.Depth);

    public static IEnumerable<MoveNextResult<TNode>> ToDepthFirstMoveNext<TNode>(
      this ITreenumerable<TNode> source)
      where TNode : struct
      => source.ToDepthFirstMoveNext(_ => false);

    public static IEnumerable<MoveNextResult<TNode>> ToDepthFirstMoveNext<TNode>(
      this ITreenumerable<TNode> source,
      Func<NodeVisit<TNode>, bool> skipChildrenPredicate)
      where TNode : struct
    {
      using (var enumerator = source.GetDepthFirstTreenumerator())
      {
        NodeVisit<TNode>? previous = null;

        while (enumerator.MoveNext(previous == null ? false : skipChildrenPredicate(previous.Value)))
        {
          yield return Create(enumerator.Current);

          previous = enumerator.Current;
        }
      }
    }

    public static IEnumerable<MoveNextResult<TNode>> ToBreadthFirstMoveNext<TNode>(
      this ITreenumerable<TNode> source)
      where TNode : struct
      => source.ToBreadthFirstMoveNext(_ => false);

    public static IEnumerable<MoveNextResult<TNode>> ToBreadthFirstMoveNext<TNode>(
      this ITreenumerable<TNode> source,
      Func<NodeVisit<TNode>, bool> skipChildrenPredicate)
      where TNode : struct
    {
      using (var enumerator = source.GetBreadthFirstTreenumerator())
      {
        NodeVisit<TNode>? previous = null;

        while (enumerator.MoveNext(previous == null ? false : skipChildrenPredicate(previous.Value)))
        {
          yield return Create(enumerator.Current);

          previous = enumerator.Current;
        }
      }
    }
  }
}
