namespace Arborist.Treenumerables.Nodes
{
  public interface INodeContainer<out TNode>
  {
    TNode Value { get; }
  }
}
