using Arborist.Core;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    // TODO:
    // This will still enumerate all the tree roots despite skipping the trees.
    // It is entirely possible for the list to be algorithmically generated and be infinite;
    // That would prevent this from ever completing. I Need to think of a solution for
    // this.
    public static ITreenumerable<T> TakeTrees<T>(
      this ITreenumerable<T> source,
      int count)
      => source.PruneBefore(visit => visit.Position.Depth == 0 && visit.Position.SiblingIndex >= count);
  }
}
