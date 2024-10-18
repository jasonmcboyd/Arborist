using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist
{
  public struct SimpleNode<TValue>
  {
    public SimpleNode(TValue value)
    {
      Value = value;
      _Children = Array.Empty<SimpleNode<TValue>>();
    }

    public SimpleNode(
      TValue value,
      IEnumerable<SimpleNode<TValue>> children)
    {
      Value = value;
      _Children = children?.ToArray() ?? Array.Empty<SimpleNode<TValue>>();
    }

    public SimpleNode(
      TValue value,
      TValue[] children)
    {
      Value = value;

      if (children == null || children.Length == 0)
      {
        _Children = Array.Empty<SimpleNode<TValue>>();
      }
      else
      {
        _Children = new SimpleNode<TValue>[children.Length];
        children.CopyTo(_Children, 0);
      }
    }

    private SimpleNode<TValue>[] _Children;

    public TValue Value { get; }

    public SimpleNodeChildEnumerator<TValue> GetChildEnumerator() => new SimpleNodeChildEnumerator<TValue>(_Children);
  }
}
