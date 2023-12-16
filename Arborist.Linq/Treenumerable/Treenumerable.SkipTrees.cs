namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<T> SkipTrees<T>(
      this ITreenumerable<T> source,
      int count)
    {
      return
        source
        .Prune(
          step => step.SiblingIndex < count,
          PruneOption.PruneBeforeNode);
    }
  }
}
