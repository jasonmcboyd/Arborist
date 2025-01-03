﻿namespace Arborist.Trees
{
  public struct CollatzTreeNodeChildEnumerator
    : IChildEnumerator<ulong>
  {
    public CollatzTreeNodeChildEnumerator(ulong value)
    {
      Value = value;
      _HasSecondChild = value != 4 && (value - 1) % 3 == 0;
      _CurrentIndexByte = 0;
      _Disposed = true;
    }

    public ulong Value { get; }
    private bool _HasSecondChild;
    private byte _CurrentIndexByte;
    private int ChildCount => _HasSecondChild ? 2 : 1;
    private int CurrentIndex => _CurrentIndexByte - 1;

    private ulong GetFirstChild() => checked(Value * 2);
    private ulong GetSecondChild() => (Value - 1) / 3;

    public bool MoveNext(out NodeAndSiblingIndex<ulong> childNodeAndSiblingIndex)
    {
      if (_Disposed || CurrentIndex == ChildCount)
      {
        childNodeAndSiblingIndex = default;
        return false;
      }

      _CurrentIndexByte++;

      childNodeAndSiblingIndex =
        new NodeAndSiblingIndex<ulong>(
          CurrentIndex == 0 ? GetFirstChild() : GetSecondChild(),
          CurrentIndex);

      return true;
    }

    private bool _Disposed;

    public void Dispose()
    {
      _Disposed = true;
    }
  }
}
