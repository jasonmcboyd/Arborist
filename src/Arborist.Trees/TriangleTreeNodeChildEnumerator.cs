namespace Arborist.Trees
{
  public struct TriangleTreeNodeChildEnumerator
    : IChildEnumerator<int>
  {
    public TriangleTreeNodeChildEnumerator(int childCount)
    {
      _ChildCount = childCount;
      _ChildIndex = 0;
      _Disposed = false;
    }

    private readonly int _ChildCount;
    private int _ChildIndex;

    public bool MoveNext(out NodeAndSiblingIndex<int> childNodeAndSiblingIndex)
    {
      if (_Disposed || _ChildIndex == _ChildCount)
      {
        childNodeAndSiblingIndex = default;
        return false;
      }

      childNodeAndSiblingIndex = new NodeAndSiblingIndex<int>(_ChildIndex, _ChildIndex);
      _ChildIndex++;
      return true;
    }

    private bool _Disposed;

    public void Dispose()
    {
      _Disposed = true;
    }
  }
}
