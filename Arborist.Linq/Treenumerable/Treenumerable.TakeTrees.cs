namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<T> TakeTrees<T>(
      this ITreenumerable<T> source,
      int count)
      => source.PruneBefore(visit => visit.OriginalPosition.SiblingIndex >= count);
  }
}
