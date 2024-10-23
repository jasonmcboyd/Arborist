namespace Arborist.Benchmarks.Trees
{
  public struct DeepTreeNodeChildEnumerator
    : IChildEnumerator<int>
  {
    public DeepTreeNodeChildEnumerator(int ancestorCount)
    {
      _AncestorCount = ancestorCount;
    }

    private int _AncestorCount;

    public bool MoveNext(out NodeAndSiblingIndex<int> childNodeAndSiblingIndex)
    {
      if (_AncestorCount == 0)
      {
        childNodeAndSiblingIndex = default;
        return false;
      }

      childNodeAndSiblingIndex = new NodeAndSiblingIndex<int>(_AncestorCount, 0);
      _AncestorCount = 0;
      return true;
    }

    public void Dispose()
    {
      // Do nothing.
    }
  }
}
