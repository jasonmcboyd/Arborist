using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist
{
  public struct SimpleNode<TValue>
  {
    public SimpleNode(TValue value) : this(value, null) { }

    public SimpleNode(
      TValue value,
      IEnumerable<SimpleNode<TValue>> children)
    {
      Value = value;
      _Children = children?.ToArray() ?? Array.Empty<SimpleNode<TValue>>();
    }

    private SimpleNode<TValue>[] _Children;

    public TValue Value { get; }

    public SimpleNodeChildEnumerator<TValue> GetChildEnumerator() => new SimpleNodeChildEnumerator<TValue>(_Children);
  }
}
