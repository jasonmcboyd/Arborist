using System;

namespace Arborist.Linq.Treenumerators
{
  public struct WithNextChildVisit<TNode>
  {
    public WithNextChildVisit(TNode node, TNode nextChild)
    {
      Node = node;
      _NextChild = nextChild;
      HasNextChild = true;
    }

    public WithNextChildVisit(TNode node)
    {
      Node = node;
      _NextChild = default;
      HasNextChild = false;
    }

    public TNode Node { get; }

    public bool HasNextChild { get; }

    private readonly TNode _NextChild;
    public TNode NextChild => HasNextChild ? _NextChild : throw new InvalidOperationException($"{nameof(NextChild)} is not available.");
  }
}
