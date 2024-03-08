using Arborist.Core;
using Arborist.Linq.Treenumerators;

namespace Arborist.Linq.Treenumerables
{
  public class EmptyTreenumerable<TNode> : ITreenumerable<TNode>
  {
    private EmptyTreenumerable()
    {
    }

    private static readonly EmptyTreenumerable<TNode> _Instance = new EmptyTreenumerable<TNode>();
    public static EmptyTreenumerable<TNode> Instance => _Instance;

    public ITreenumerator<TNode> GetBreadthFirstTreenumerator()
      => new EmptyTreenumerator<TNode>();

    public ITreenumerator<TNode> GetDepthFirstTreenumerator()
      => new EmptyTreenumerator<TNode>();
  }
}
