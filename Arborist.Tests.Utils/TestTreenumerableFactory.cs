using Arborist.Treenumerables;
using System.Collections.Generic;

namespace Arborist.Tests.Utils
{
  public static class TestTreenumerableFactory
  {
    public static ITreenumerable<TValue> Create<TNode, TValue>(params TNode[] roots)
      where TNode : INodeWithIndexableChildren<TNode, TValue>
      => new IndexableTreenumerable<TNode, TValue>(roots);

    public static ITreenumerable<TValue> Create<TNode, TValue>(IEnumerable<TNode> roots)
      where TNode : INodeWithIndexableChildren<TNode, TValue>
      => new IndexableTreenumerable<TNode, TValue>(roots);
  }
}
