namespace Arborist.Benchmarks.Trees
{
  public struct DeepTreeNodeChildEnumerator
  {
    public DeepTreeNodeChildEnumerator(int ancestorCount)
    {
      _AncestorCount = ancestorCount;
    }

    private int _AncestorCount;

    public bool TryMoveNext(out NodeAndSiblingIndex<int> childNodeAndSiblingIndex)
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
  }
}
