using Arborist.Treenumerables;
using System;

namespace Arborist.Collatz
{
  public struct CollatzNode : INodeWithIndexableChildren<CollatzNode, ulong>
  {
    public CollatzNode(ulong value)
    {
      if (value < 2)
        throw new ArgumentOutOfRangeException($"The 'value' must be greater than 1.");

      Value = value;

      _FirstChild = checked(value * 2);

      if (value != 4 && (value - 1) % 3 == 0)
      {
        _SecondChild = (value - 1) / 3;
        ChildCount = 2;
      }
      else
      {
        _SecondChild = 0;
        ChildCount = 1;
      }
    }

    public CollatzNode this[int index]
    {
      get
      {
        if (index == 0)
          return new CollatzNode(_FirstChild);
        else if (index == 1 && ChildCount == 2)
          return new CollatzNode(_SecondChild);
        else
          throw new IndexOutOfRangeException();
      }
    }

    public int ChildCount { get; private set; }

    private ulong _FirstChild;
    private ulong _SecondChild;

    public ulong Value { get; }
  }
}
