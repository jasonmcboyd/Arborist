namespace Arborist.Trees
{
  public struct CollatzTreeNode
  {
    public CollatzTreeNode(ulong value)
    {
      if (value < 2)
        throw new ArgumentOutOfRangeException($"The 'value' must be greater than 1.");

      Value = value;

    }

    public ulong Value { get; }

    public CollatzTreeNodeChildEnumerator GetChildEnumerator() => new CollatzTreeNodeChildEnumerator(Value);
  }
}
