namespace Arborist
{
  public enum SchedulingStrategy
  {
    // Traverse node, traverse descendants
    ScheduleForTraversal,
    // Skip node, traverse descendants
    SkipNode,
    // Traverse node, skip descendants
    SkipDescendantSubtrees,
    // Skip node, skip descendants
    SkipSubtree,
  }
}
