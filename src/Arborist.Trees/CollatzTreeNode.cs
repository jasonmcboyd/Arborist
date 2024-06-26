using Arborist.Nodes;
using System;

namespace Arborist.Trees
{
  internal struct CollatzTreeNode : INodeWithIndexableChildren<ulong>
  {
    public CollatzTreeNode(ulong value)
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

    public INodeWithIndexableChildren<ulong> this[int index]
    {
      get
      {
        if (index == 0)
          return new CollatzTreeNode(_FirstChild);
        else if (index == 1 && ChildCount == 2)
          return new CollatzTreeNode(_SecondChild);
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
