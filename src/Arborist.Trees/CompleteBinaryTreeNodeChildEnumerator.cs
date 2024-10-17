namespace Arborist.Trees
{
  public struct CompleteBinaryTreeNodeChildEnumerator
  {
    public CompleteBinaryTreeNodeChildEnumerator(ulong parentValue)
    {
      TrySetFirstChildValue(parentValue);
    }

    private void TrySetFirstChildValue(ulong parentValue)
    {
      try
      {
        _ChildValue = checked(parentValue * 2);
      }
      catch
      {
        _ChildValue = ulong.MaxValue;
      }
    }

    private void TryIncrementChildValue()
    {
      if (_ChildValue == ulong.MaxValue)
        return;

      if ((_ChildValue & 1) == 1)
      {
        _ChildValue = ulong.MaxValue;
        return;
      }

      try
      {
        _ChildValue = checked(_ChildValue + 1);
      }
      catch
      {
        _ChildValue = ulong.MaxValue;
      }
    }

    private ulong _ChildValue;

    public bool TryMoveNext(out NodeAndSiblingIndex<ulong> childNodeAndSiblingIndex)
    {
      if (_ChildValue == ulong.MaxValue)
      {
        childNodeAndSiblingIndex = default;
        return false;
      }

      childNodeAndSiblingIndex = new NodeAndSiblingIndex<ulong>(_ChildValue, (int)(_ChildValue % 2));

      TryIncrementChildValue();

      return true;
    }
  }
}
