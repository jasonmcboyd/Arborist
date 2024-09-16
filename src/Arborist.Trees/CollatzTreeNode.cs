using Arborist.Nodes;

namespace Arborist.Trees
{
  struct CollatzTreeNode : INodeWithIndexableChildren<ulong>
  {
    public CollatzTreeNode(ulong value)
    {
      if (value < 2)
        throw new ArgumentOutOfRangeException($"The 'value' must be greater than 1.");

      Value = value;

      var hasSecondChild = value != 4 && (value - 1) % 3 == 0;
        
      ChildCount = hasSecondChild ? 2 : 1;
    }

    public INodeWithIndexableChildren<ulong> this[int index]
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

    public int ChildCount { get; private set; }
    public ulong Value { get; }

    private ulong GetFirstChild() => checked(Value * 2);

    private ulong GetSecondChild() => (Value - 1) / 3;
  }
}
