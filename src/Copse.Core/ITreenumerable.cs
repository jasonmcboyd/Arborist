namespace Copse.Core
{
  public interface ITreenumerable<TNode>
  {
    ITreenumerator<TNode> GetBreadthFirstTreenumerator();
    ITreenumerator<TNode> GetDepthFirstTreenumerator();
  }
}
