using System.Collections.Generic;

namespace Arborist.Core
{
  public struct NodePosition : IEqualityComparer<NodePosition>
  {
    public NodePosition(int siblingIndex, int depth)
    {
      SiblingIndex = siblingIndex;
      Depth = depth;
    }

    public int SiblingIndex { get; }
    public int Depth { get; }


    public static implicit operator NodePosition((int, int) tuple)
      => new NodePosition(tuple.Item1, tuple.Item2);

    public static implicit operator (int, int)(NodePosition nodePosition)
      => (nodePosition.SiblingIndex, nodePosition.Depth);

    public static bool operator ==(NodePosition left, NodePosition right)
      => left.Equals(left, right);

    public static bool operator !=(NodePosition left, NodePosition right)
      => !left.Equals(left, right);

    public static NodePosition operator +(NodePosition left, NodePosition right)
      => new NodePosition(left.SiblingIndex + right.SiblingIndex, left.Depth + right.Depth);

    public static NodePosition operator +(NodePosition left, (int, int) right)
      => left + (NodePosition)right;

    public static NodePosition operator -(NodePosition left, NodePosition right)
      => new NodePosition(left.SiblingIndex - right.SiblingIndex, left.Depth - right.Depth);

    public static NodePosition operator -(NodePosition left, (int, int) right)
      => left - (NodePosition)right;

    public NodePosition AddToDepth(int value) => new NodePosition(SiblingIndex, Depth + value);
    public NodePosition AddToSiblingIndex(int value) => new NodePosition(SiblingIndex + value, Depth);

    public bool Equals(NodePosition left, NodePosition right)
      => left.SiblingIndex == right.SiblingIndex && left.Depth == right.Depth;

    public int GetHashCode(NodePosition nodePosition)
      => (nodePosition.SiblingIndex, nodePosition.Depth).GetHashCode();

    public override bool Equals(object obj)
    {
      if (!(obj is NodePosition nodePosition))
        return false;

      return Equals(this, nodePosition);
    }

    public override int GetHashCode()
      => GetHashCode(this);

    public override string ToString()
      => $"({SiblingIndex}, {Depth})";
  }
}
