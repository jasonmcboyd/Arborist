using System.Collections.Generic;

namespace Arborist.Treenumerables
{
  public interface INodeWithEnumerableChildren<out TNode>
  {
    TNode Value { get; }
    IEnumerable<INodeWithEnumerableChildren<TNode>> Children { get; }
  }
}
