namespace Arborist.Nodes
{
  public interface INodeContainer<out TNode>
  {
    TNode Value { get; }
  }
}
