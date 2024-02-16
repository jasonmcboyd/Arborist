using System.Collections.Generic;

namespace Arborist.Treenumerables.Nodes
{
  public interface INodeContainerWithEnumerableChildren<out TValue>
    : INodeContainer<TValue>
  {
    IEnumerable<INodeContainerWithEnumerableChildren<TValue>> Children { get; }
  }
}
