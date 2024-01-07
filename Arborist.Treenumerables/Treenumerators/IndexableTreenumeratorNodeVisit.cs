namespace Arborist.Treenumerables.Treenumerators
{
  internal struct IndexableTreenumeratorNodeVisit<TNode, TValue>
    where TNode : INodeWithIndexableChildren<TNode, TValue>
  {
    public IndexableTreenumeratorNodeVisit(
      TNode node,
      int visitCount,
      int siblingIndex,
      int depth,
      int visitedChildrenCount,
      bool skipped)
    {
      Node = node;
      VisitCount = visitCount;
      SiblingIndex = siblingIndex;
      Depth = depth;
      VisitedChildrenCount = visitedChildrenCount;
      Skipped = skipped;
    }

    public TNode Node { get; }
    public int VisitCount { get; }
    public int SiblingIndex { get; }
    public int Depth { get; }
    public int VisitedChildrenCount { get; }
    public bool Skipped { get; }
  }

  internal static class IndexableTreenumeratorNodeVisit
  {
    public static IndexableTreenumeratorNodeVisit<TNode, TValue> Create<TNode, TValue>(
      TNode node,
      int visitCount,
      int siblingIndex,
      int depth,
      int visitedChildrenCount,
      bool skipped)
      where TNode : INodeWithIndexableChildren<TNode, TValue>
      => new IndexableTreenumeratorNodeVisit<TNode, TValue>(
        node,
        visitCount,
        siblingIndex,
        depth,
        visitedChildrenCount,
        skipped);

    public static IndexableTreenumeratorNodeVisit<TNode, TValue> With<TNode, TValue>(
      this IndexableTreenumeratorNodeVisit<TNode, TValue> depthFirstNodeVisit,
      int? visitCount = null,
      int? siblingIndex = null,
      int? depth = null,
      int? visitedChildrenCount = null,
      bool? skipped = null)
      where TNode : INodeWithIndexableChildren<TNode, TValue>
      => new IndexableTreenumeratorNodeVisit<TNode, TValue>(
        depthFirstNodeVisit.Node,
        visitCount == null ? depthFirstNodeVisit.VisitCount : visitCount.Value,
        siblingIndex == null ? depthFirstNodeVisit.SiblingIndex : siblingIndex.Value,
        depth == null ? depthFirstNodeVisit.Depth : depth.Value,
        visitedChildrenCount == null ? depthFirstNodeVisit.VisitedChildrenCount : visitedChildrenCount.Value,
        skipped == null ? depthFirstNodeVisit.Skipped : skipped.Value);

    public static IndexableTreenumeratorNodeVisit<TNode, TValue> IncrementVisitedChildrenCount<TNode, TValue>(
      this IndexableTreenumeratorNodeVisit<TNode, TValue> depthFirstNodeVisit)
      where TNode : INodeWithIndexableChildren<TNode, TValue>
      => new IndexableTreenumeratorNodeVisit<TNode, TValue>(
        depthFirstNodeVisit.Node,
        depthFirstNodeVisit.VisitCount,
        depthFirstNodeVisit.SiblingIndex,
        depthFirstNodeVisit.Depth,
        depthFirstNodeVisit.VisitedChildrenCount + 1,
        depthFirstNodeVisit.Skipped);

    public static IndexableTreenumeratorNodeVisit<TNode, TValue> IncrementVisitCount<TNode, TValue>(
      this IndexableTreenumeratorNodeVisit<TNode, TValue> depthFirstNodeVisit)
      where TNode : INodeWithIndexableChildren<TNode, TValue>
      => new IndexableTreenumeratorNodeVisit<TNode, TValue>(
        depthFirstNodeVisit.Node,
        depthFirstNodeVisit.VisitCount + 1,
        depthFirstNodeVisit.SiblingIndex,
        depthFirstNodeVisit.Depth,
        depthFirstNodeVisit.VisitedChildrenCount,
        depthFirstNodeVisit.Skipped);

    public static IndexableTreenumeratorNodeVisit<TNode, TValue> Skip<TNode, TValue>(
      this IndexableTreenumeratorNodeVisit<TNode, TValue> depthFirstNodeVisit)
      where TNode : INodeWithIndexableChildren<TNode, TValue>
      => new IndexableTreenumeratorNodeVisit<TNode, TValue>(
        depthFirstNodeVisit.Node,
        depthFirstNodeVisit.VisitCount,
        depthFirstNodeVisit.SiblingIndex,
        depthFirstNodeVisit.Depth,
        depthFirstNodeVisit.VisitedChildrenCount,
        true);

    public static IndexableTreenumeratorNodeVisit<TNode, TValue> Create<TNode, TValue>(TNode node)
      where TNode : INodeWithIndexableChildren<TNode, TValue>
      => new IndexableTreenumeratorNodeVisit<TNode, TValue>(node, 1, 0, 0, 0, false);

    public static NodeVisit<TValue> ToNodeVisit<TNode, TValue>(
      this IndexableTreenumeratorNodeVisit<TNode, TValue> depthFirstNodeVisit)
      where TNode : INodeWithIndexableChildren<TNode, TValue>
      => NodeVisit.Create(
        depthFirstNodeVisit.Node.Value,
        depthFirstNodeVisit.VisitCount,
        depthFirstNodeVisit.SiblingIndex,
        depthFirstNodeVisit.Depth);

    public static bool HasNextChild<TNode, TValue>(
      this IndexableTreenumeratorNodeVisit<TNode, TValue> depthFirstNode)
      where TNode : INodeWithIndexableChildren<TNode, TValue>
      => depthFirstNode.Node.ChildCount > depthFirstNode.VisitedChildrenCount;

    public static TNode GetNextChildNode<TNode, TValue>(
      this IndexableTreenumeratorNodeVisit<TNode, TValue> depthFirstNode)
      where TNode : INodeWithIndexableChildren<TNode, TValue>
      => depthFirstNode.Node[depthFirstNode.VisitedChildrenCount];

    public static IndexableTreenumeratorNodeVisit<TNode, TValue> GetNextChildVisit<TNode, TValue>(
      this IndexableTreenumeratorNodeVisit<TNode, TValue> depthFirstNode)
      where TNode : INodeWithIndexableChildren<TNode, TValue>
      => IndexableTreenumeratorNodeVisit.Create<TNode, TValue>(
        depthFirstNode.GetNextChildNode(),
        0,
        depthFirstNode.VisitedChildrenCount,
        depthFirstNode.Depth + 1,
        0,
        false);
  }
}
