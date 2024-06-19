using System.Collections.Generic;

namespace Arborist.Nodes
{
  public interface INodeWithEnumerableChildren<out TValue>
    : INode<TValue>
  {
    IEnumerable<INodeWithEnumerableChildren<TValue>> Children { get; }
  }
}
