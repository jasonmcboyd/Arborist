namespace Arborist.Nodes
{
  public interface INodeWithIndexableChildren<TValue, out TNode>
    : INode<TValue>
    where TNode : INodeWithIndexableChildren<TValue, TNode>
  {
    int ChildCount { get; }
    TNode this[int index] { get; }
  }
}
