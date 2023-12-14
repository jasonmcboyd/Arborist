namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<T> TakeTrees<T>(
      this ITreenumerable<T> source,
      int count)
    {
      return
        source
        .Prune(
          step => step.SiblingIndex >= count,
          PruneOptions.PruneBeforeNode);
    }
  }
}
