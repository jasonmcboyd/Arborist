using System.Collections.Generic;

namespace Arborist.Nodes
{
  public interface INodeContainerWithEnumerableChildren<out TValue>
    : INodeContainer<TValue>
  {
    IEnumerable<INodeContainerWithEnumerableChildren<TValue>> Children { get; }
  }
}
