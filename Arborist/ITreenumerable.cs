namespace Arborist
{
  public interface ITreenumerable<TNode>
  {
    ITreenumerator<TNode> GetBreadthFirstTreenumerator();
    ITreenumerator<TNode> GetDepthFirstTreenumerator();
  }
}