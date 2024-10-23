namespace Arborist.Trees
{
  public struct TriangleTreeNodeChildEnumerator
    : IChildEnumerator<int>
  {
    public TriangleTreeNodeChildEnumerator(int childCount)
    {
      _ChildCount = childCount;
      _ChildIndex = 0;
    }

    private readonly int _ChildCount;
    private int _ChildIndex;

    public bool MoveNext(out NodeAndSiblingIndex<int> childNodeAndSiblingIndex)
    {
      if (_ChildIndex == _ChildCount)
      {
        childNodeAndSiblingIndex = default;
        return false;
      }

      childNodeAndSiblingIndex = new NodeAndSiblingIndex<int>(_ChildIndex, 0);
      _ChildIndex++;
      return true;
    }

    public void Dispose()
    {
      // Do nothing.
    }
  }
}
