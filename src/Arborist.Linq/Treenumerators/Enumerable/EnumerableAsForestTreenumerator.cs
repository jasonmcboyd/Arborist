using Arborist.Core;
using System.Collections.Generic;

namespace Arborist.Linq.Treenumerators.Enumerator
{
  internal class EnumerableAsForestTreenumerator<TNode> : ITreenumerator<TNode>
  {
    public EnumerableAsForestTreenumerator(
      IEnumerable<TNode> enumerable)
    {
      _Enumerator = enumerable.GetEnumerator();
    }

    private readonly IEnumerator<TNode> _Enumerator;
    private int _RootNodesSeen = 0;

    public TNode Node { get; private set; } = default;
    public int VisitCount { get; private set; } = 0;
    public NodePosition Position { get; private set; } = new NodePosition(0, -1);
    public TreenumeratorMode Mode { get; private set; } = default;

    public bool MoveNext(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (Mode == TreenumeratorMode.VisitingNode || Position == new NodePosition(0, -1))
        return TryMoveNext();

      return OnScheduling(nodeTraversalStrategies);
    }

    private bool OnScheduling(NodeTraversalStrategies nodeTraversalStrategies)
    {
      if (nodeTraversalStrategies.HasFlag(NodeTraversalStrategies.SkipSiblings))
        return false;

      if (nodeTraversalStrategies.HasFlag(NodeTraversalStrategies.SkipNode))
        return TryMoveNext();

      Mode = TreenumeratorMode.VisitingNode;
      VisitCount++;

      return true;
    }

    private bool TryMoveNext()
    {
      if (!_Enumerator.MoveNext())
        return false;

      ScheduleNodeFromEnumerator();

      return true;
    }

    private void ScheduleNodeFromEnumerator()
    {
      Node = _Enumerator.Current;
      VisitCount = 0;
      Mode = TreenumeratorMode.SchedulingNode;
      Position = new NodePosition(_RootNodesSeen, 0);

      _RootNodesSeen++;
    }

    public void Dispose()
    {
      _Enumerator?.Dispose();
    }
  }
}
