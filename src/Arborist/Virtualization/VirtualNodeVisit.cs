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
    public SchedulingStrategy SchedulingStrategy { get; set; }

    public bool SkippingNode =>
      SchedulingStrategy == SchedulingStrategy.SkipNode
      || SchedulingStrategy == SchedulingStrategy.SkipSubtree;

    public bool SkippingDescendants =>
      SchedulingStrategy == SchedulingStrategy.SkipDescendants
      || SchedulingStrategy == SchedulingStrategy.SkipSubtree;
  }
}
