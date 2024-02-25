namespace Arborist.Nodes
{
  public interface INodeContainerWithIndexableChildren<out TValue>
    : INodeContainer<TValue>
  {
    int ChildCount { get; }
    INodeContainerWithIndexableChildren<TValue> this[int index] { get; }
  }
}
