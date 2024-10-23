using Arborist.Linq;
using System.Linq;

namespace Arborist.Benchmarks
{
  public class Profiler
  {
    public int LevelOrderTraversal_DeepTree()
    {
      var tree = Treenumerables.GetDeepTree(19);

      return
        tree
        .LevelOrderTraversal()
        .Count();
    }
  }
}
