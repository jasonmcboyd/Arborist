using Arborist.Core;
using Arborist.Linq.Treenumerators;

namespace Arborist.Linq
{
  internal class HideTreenumerable<TNode> : ITreenumerable<TNode>
  {
    public HideTreenumerable(ITreenumerable<TNode> innerTreenumerable)
    {
      _InnerTreenumerable = innerTreenumerable;
    }

    private readonly ITreenumerable<TNode> _InnerTreenumerable;

    public ITreenumerator<TNode> GetBreadthFirstTreenumerator()
      => new HideTreenumerator<TNode>(_InnerTreenumerable.GetBreadthFirstTreenumerator);

    public ITreenumerator<TNode> GetDepthFirstTreenumerator()
      => new HideTreenumerator<TNode>(_InnerTreenumerable.GetDepthFirstTreenumerator);
  }
}
