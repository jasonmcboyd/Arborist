using Arborist.Core;

namespace Arborist.Linq
{
  public static partial class Treenumerable
  {
    public static ITreenumerable<T> SkipTrees<T>(
      this ITreenumerable<T> source,
      int count)
      => source.PruneBefore(step => step.Position.Depth == 0 && step.Position.SiblingIndex < count);
  }
}
