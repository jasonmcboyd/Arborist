using Nito.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arborist.Treenumerables.Treenumerators
{
  internal sealed class IndexableBreadthFirstTreenumerator<TIndexableNode, TIndexableNodeValue>
    : TreenumeratorBase<TIndexableNodeValue>
    where TIndexableNode : INodeWithIndexableChildren<TIndexableNode, TIndexableNodeValue>
  {
    public IndexableBreadthFirstTreenumerator(IEnumerable<TIndexableNode> roots)
    {
      var enumerator = roots.GetEnumerator();
    }

    private readonly IEnumerator<TIndexableNode> _RootsEnumerator;
    private int _RootsEnumeratorIndex = -1;

    private Deque<IndexableTreenumeratorNodeVisit<TIndexableNode, TIndexableNodeValue>> _CurrentLevel = new Deque<IndexableTreenumeratorNodeVisit<TIndexableNode, TIndexableNodeValue>>();
    private Deque<IndexableTreenumeratorNodeVisit<TIndexableNode, TIndexableNodeValue>> _NextLevel = new Deque<IndexableTreenumeratorNodeVisit<TIndexableNode, TIndexableNodeValue>>();

    private int _Depth = 0;

    protected override bool OnMoveNext(ChildStrategy childStrategy)
    {
      if (_CurrentLevel.Count == 0)
        return false;
    }

    public override void Dispose()
    {
      _RootsEnumerator?.Dispose();
    }
  }
}
