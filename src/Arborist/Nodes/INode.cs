namespace Arborist.Nodes
{
  public interface INode<out TValue>
  {
    TValue Value { get; }
  }
}
