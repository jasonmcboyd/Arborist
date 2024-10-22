namespace Arborist.Trees
{
  public struct TriangleTreeNodeChildEnumerator
  {
    public TriangleTreeNodeChildEnumerator(int childCount)
    {
      _ChildCount = childCount;
      _ChildIndex = 0;
    }

    private readonly int _ChildCount;
    private int _ChildIndex;

    public bool TryMoveNext(out NodeAndSiblingIndex<int> childNodeAndSiblingIndex)
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
  }
}
