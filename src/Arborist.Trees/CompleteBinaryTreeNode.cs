using Arborist.Nodes;

namespace Arborist.Trees
{
  public struct CompleteBinaryTreeNode : INodeWithIndexableChildren<ulong>
  {
    public CompleteBinaryTreeNode(ulong index)
    {
      Value = index;
    }

    public CompleteBinaryTreeNode()
    {
      Value = 0;
    }

    public INodeWithIndexableChildren<ulong> this[int index]
    {
      get
      {
        if (index > 1)
          throw new IndexOutOfRangeException();

        return
          index == 0
            ? new CompleteBinaryTreeNode(checked(Value * 2))
            : new CompleteBinaryTreeNode(checked(Value * 2 + 1));
      }
    }

    public int ChildCount { get; } = 2;

    public ulong Value { get; }
  }
}
