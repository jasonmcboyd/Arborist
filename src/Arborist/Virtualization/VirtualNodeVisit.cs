using Arborist.Core;

namespace Arborist.Virtualization
{
  // TODO: I am not sure how much value this provides.
  internal class VirtualNodeVisit<TNode>
  {
    public TreenumeratorMode Mode { get; set; }
    public TNode Node { get; set; }
    public int VisitCount { get; set; }
    public NodePosition OriginalPosition { get; set; }
    public NodePosition Position { get; set; }
    public TraversalStrategy TraversalStrategy { get; set; }

    public bool SkippingNode =>
      TraversalStrategy == TraversalStrategy.SkipSubtree;

    public bool SkippingDescendants =>
      TraversalStrategy == TraversalStrategy.SkipDescendants
      || TraversalStrategy == TraversalStrategy.SkipSubtree;
  }
}
