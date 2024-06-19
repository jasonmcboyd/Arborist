namespace Arborist.Nodes
{
  public interface INode<out TNode>
  {
    TNode Value { get; }
  }
}
