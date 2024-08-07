using Arborist.Core;
using System.Collections;
using System.Collections.Generic;

namespace Arborist.Linq.TreeEnumerable.BreadthFirstTree
{
  public sealed class BreadthFirstTreeEnumerable<TNode> : IBreadthFirstTreeEnumerable<TNode>
  {
    public BreadthFirstTreeEnumerable(ITreenumerable<TNode> treenumerable)
    {
      _Treenumerable = treenumerable;
    }

    public BreadthFirstTreeEnumerable(IEnumerable<BreadthFirstTreeEnumerableToken<TNode>> enumerable)
    {
      _Enumerable = enumerable;
    }

    private readonly ITreenumerable<TNode> _Treenumerable;
    private readonly IEnumerable<BreadthFirstTreeEnumerableToken<TNode>> _Enumerable;

    public IEnumerator<BreadthFirstTreeEnumerableToken<TNode>> GetEnumerator()
    {
      return
        _Enumerable != null
          ? _Enumerable.GetEnumerator()
          : new BreadthFirstTreeEnumerator<TNode>(_Treenumerable.GetBreadthFirstTreenumerator());
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
  }
}
