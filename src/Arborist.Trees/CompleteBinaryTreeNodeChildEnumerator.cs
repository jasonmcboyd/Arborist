namespace Arborist.Trees
{
  public struct CompleteBinaryTreeNodeChildEnumerator
    : IChildEnumerator<int>
  {
    public CompleteBinaryTreeNodeChildEnumerator(int parentValue)
    {
      try
      {
        _ChildValue = checked(parentValue * 2);
      }
      catch
      {
        _ChildValue = int.MaxValue;
      }
    }

    private void TryIncrementChildValue()
    {
      if (_ChildValue == int.MaxValue)
        return;

      if ((_ChildValue & 1) == 1)
      {
        _ChildValue = int.MaxValue;
        return;
      }

      try
      {
        _ChildValue = checked(_ChildValue + 1);
      }
      catch
      {
        _ChildValue = int.MaxValue;
      }
    }

    private int _ChildValue;

    public bool MoveNext(out NodeAndSiblingIndex<int> childNodeAndSiblingIndex)
    {
      if (_ChildValue == int.MaxValue)
      {
        childNodeAndSiblingIndex = default;
        return false;
      }

      childNodeAndSiblingIndex = new NodeAndSiblingIndex<int>(_ChildValue, (int)(_ChildValue % 2));

      TryIncrementChildValue();

      return true;
    }

    public void Dispose()
    {
      // Do nothing.
    }
  }
}
