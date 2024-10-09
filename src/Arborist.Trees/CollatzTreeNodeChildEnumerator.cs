using Arborist.Treenumerables;

namespace Arborist.Trees
{
  public struct CollatzTreeNodeChildEnumerator
  {
    public CollatzTreeNodeChildEnumerator(ulong value)
    {
      Value = value;
      _HasSecondChild = value != 4 && (value - 1) % 3 == 0;
    }

    public ulong Value { get; }
    private bool _HasSecondChild;
    private byte _CurrentIndexByte = 0;
    private int ChildCount => _HasSecondChild ? 2 : 1;
    private int CurrentIndex => _CurrentIndexByte - 1;

    private CollatzTreeNode GetFirstChild() => new CollatzTreeNode(checked(Value * 2));
    private CollatzTreeNode GetSecondChild() => new CollatzTreeNode((Value - 1) / 3);

    public TryMoveNextChildResult<CollatzTreeNode> TryMoveNext()
    {
      if (CurrentIndex == ChildCount)
        return TryMoveNextChildResult.NoNextChild<CollatzTreeNode>();

      _CurrentIndexByte++;

      return TryMoveNextChildResult.NextChild(CurrentIndex == 0 ? GetFirstChild() : GetSecondChild(), CurrentIndex);
    }
  }
}
