using System;
using System.Collections.Generic;

namespace Arborist.Treenumerables
{
  public class IndexableTreenumerable<TIndexableNode, TIndexableNodeValue>
    : ITreenumerable<TIndexableNodeValue>
    where TIndexableNode : INodeWithIndexableChildren<TIndexableNode, TIndexableNodeValue>
  {
    public IndexableTreenumerable(IEnumerable<TIndexableNode> roots)
    {
      _Roots = roots;
    }

    private readonly IEnumerable<TIndexableNode> _Roots;

    public ITreenumerator<TIndexableNodeValue> GetBreadthFirstTreenumerator()
      => throw new NotImplementedException();

    public ITreenumerator<TIndexableNodeValue> GetDepthFirstTreenumerator()
      => throw new NotImplementedException();
  }
}
