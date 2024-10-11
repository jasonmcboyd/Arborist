using Arborist.Nodes;

namespace Arborist.Trees
{
  public struct CompleteBinaryTreeNode
  {
    public CompleteBinaryTreeNode(ulong value)
    {
      Value = value;
    }

    public CompleteBinaryTreeNode()
    {
      Value = 0;
    }

    public ulong Value { get; }

    public CompleteBinaryTreeNodeChildEnumerator GetChildEnumerator()
      => new CompleteBinaryTreeNodeChildEnumerator(Value);
  }
}
