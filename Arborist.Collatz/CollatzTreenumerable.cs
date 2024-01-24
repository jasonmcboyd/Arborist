using Arborist.Treenumerables;

namespace Arborist.Collatz
{
  public class CollatzTreenumerable : IndexableTreenumerable<CollatzNode, ulong>
  {
    public CollatzTreenumerable() : base(_Roots)
    {
    }

    private static CollatzNode[] _Roots = new CollatzNode[] { new CollatzNode(2) };
  }
}
