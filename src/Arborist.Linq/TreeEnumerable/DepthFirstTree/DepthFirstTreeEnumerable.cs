using Arborist.Core;
using System.Collections;
using System.Collections.Generic;

namespace Arborist.Linq.TreeEnumerable.DepthFirstTree
{
  public sealed class DepthFirstTreeEnumerable<TNode> : IDepthFirstTreeEnumerable<TNode>
  {
    public DepthFirstTreeEnumerable(ITreenumerable<TNode> treenumerable)
    {
      _Treenumerable = treenumerable;
    }

    public DepthFirstTreeEnumerable(IEnumerable<DepthFirstTreeEnumerableToken<TNode>> enumerable)
    {
      _Enumerable = enumerable;
    }

    private readonly ITreenumerable<TNode> _Treenumerable;
    private readonly IEnumerable<DepthFirstTreeEnumerableToken<TNode>> _Enumerable;

    public IEnumerator<DepthFirstTreeEnumerableToken<TNode>> GetEnumerator()
    {
      return
        _Enumerable != null
          ? _Enumerable.GetEnumerator()
          : new DepthFirstTreeEnumerator<TNode>(_Treenumerable.GetDepthFirstTreenumerator());
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
  }
}
