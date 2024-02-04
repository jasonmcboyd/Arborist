using System.Collections.Generic;
using System.Linq;

namespace Arborist.Treenumerables.Treenumerators
{
  internal sealed class IndexableDepthFirstTreenumerator<TNode, TValue>
    : TreenumeratorBase<TValue>
    where TNode : INodeWithIndexableChildren<TNode, TValue>
  {
    public IndexableDepthFirstTreenumerator(IEnumerable<TNode> roots)
    {
      var innerRoots = roots.Select(_Pool.Get);
      _InnerTreenumerator = new DepthFirstTreenumerator<TValue>(innerRoots);
    }

    private readonly PooledNodeWithIndexableChildrenWrapperPool<TNode, TValue> _Pool =
      new PooledNodeWithIndexableChildrenWrapperPool<TNode, TValue>();

    private readonly DepthFirstTreenumerator<TValue> _InnerTreenumerator;

    protected override bool OnMoveNext(SchedulingStrategy schedulingStrategy)
    {
      if (!_InnerTreenumerator.MoveNext(schedulingStrategy))
        return false;

      Current = _InnerTreenumerator.Current;
      State = _InnerTreenumerator.State;

      return true;
    }

    public override void Dispose()
    {
      _InnerTreenumerator.Dispose();
    }
  }
}
