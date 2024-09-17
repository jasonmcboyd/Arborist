using Arborist.Nodes;

namespace Arborist.Trees
{
  public struct CollatzTreeNode : INodeWithIndexableChildren<ulong, CollatzTreeNode>
  {
    public CollatzTreeNode(ulong value)
    {
      if (value < 2)
        throw new ArgumentOutOfRangeException($"The 'value' must be greater than 1.");

      Value = value;

      var hasSecondChild = value != 4 && (value - 1) % 3 == 0;
        
      ChildCount = hasSecondChild ? 2 : 1;
    }

    public CollatzTreeNode this[int index]
    {
      get
      {
        if (index == 0)
          return new CollatzTreeNode(GetFirstChild());
        else if (index == 1 && ChildCount == 2)
          return new CollatzTreeNode(GetSecondChild());
        else
          throw new IndexOutOfRangeException();
      }
    }

    public ulong Value { get; }
    public int ChildCount { get; private set; }

    private ulong GetFirstChild() => checked(Value * 2);
    private ulong GetSecondChild() => (Value - 1) / 3;
  }
}
