namespace Arborist.Treenumerables
{
  public interface INodeWithIndexableChildren<out TNode, out TValue>
    where TNode : INodeWithIndexableChildren<TNode, TValue>
  {
    int ChildCount { get; }
    TNode this[int index] { get; }
    TValue Value { get; }
  }
}
