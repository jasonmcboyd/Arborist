using System.Collections.Generic;

namespace Arborist.Treenumerables
{
  public interface INodeWithIndexableChildren<out TNode, out TValue>
    where TNode : INodeWithIndexableChildren<TNode, TValue>
  {
    int ChildCount { get; }
    // TODO: Can I just return an INodeWithIndexableChildren here?
    // I think that would simplify some other stuff if I can.
    TNode this[int index] { get; }
    TValue Value { get; }
  }
}
