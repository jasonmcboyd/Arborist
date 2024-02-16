namespace Arborist.Treenumerables.Virtualization
{
  internal class VirtualNodeVisit<TNode>
  {
    public TreenumeratorState TreenumeratorState { get; set; }
    public TNode Node { get; set; }
    public int VisitCount { get; set; }
    public NodePosition OriginalPosition { get; set; }
    public NodePosition Position { get; set; }
    public SchedulingStrategy SchedulingStrategy { get; set; }
  }
}
