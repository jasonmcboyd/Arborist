using Arborist.Common;

namespace Arborist.SimpleSerializer
{
  public static class SimpleNodeDelegates
  {
    public static bool MoveNextChild<TValue>(
      ref SimpleNodeChildEnumerator<TValue> childEnumerator,
      out NodeAndSiblingIndex<SimpleNode<TValue>> childNodeAndSiblingIndex)
    {
      if (childEnumerator.MoveNext())
      {
        childNodeAndSiblingIndex = childEnumerator.CurrentChild;
        return true;
      }
      else
      {
        childNodeAndSiblingIndex = default;
        return false;
      }
    }

    public static void DisposeChildEnumerator<TValue>(ref SimpleNodeChildEnumerator<TValue> childEnumerator)
    {
      // Do nothing.
    }
  }
}
