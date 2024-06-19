namespace Arborist.Nodes
{
  public interface INodeWithIndexableChildren<out TValue>
    : INode<TValue>
  {
    int ChildCount { get; }
    INodeWithIndexableChildren<TValue> this[int index] { get; }
  }
}
