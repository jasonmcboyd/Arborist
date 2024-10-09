namespace Arborist.Treenumerables
{
  public struct TryMoveNextChildResult<TNode>
  {
    public TryMoveNextChildResult(bool hadNextChild, TNode nextChild, int childIndex)
    {
      HadNextChild = hadNextChild;
      NextChild = nextChild;
      ChildIndex = childIndex;
    }

    public bool HadNextChild { get; }
    public TNode NextChild { get; }
    public int ChildIndex { get; }
  }

  public static class TryMoveNextChildResult
  {
    public static TryMoveNextChildResult<TNode> NoNextChild<TNode>()
    {
      return new TryMoveNextChildResult<TNode>(false, default, -1);
    }

    public static TryMoveNextChildResult<TNode> NextChild<TNode>(TNode childNode, int childIndex)
    {
      return new TryMoveNextChildResult<TNode>(true, childNode, childIndex);
    }
  }
}
