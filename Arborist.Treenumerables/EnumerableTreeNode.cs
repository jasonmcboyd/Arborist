using System.Collections.Generic;
using System.Linq;

namespace Arborist.Treenumerables
{
  public class EnumerableTreeNode<TValue>
    : INodeWithEnumerableChildren<TValue>
  {
    public EnumerableTreeNode(TValue value)
      : this(value, Enumerable.Empty<EnumerableTreeNode<TValue>>())
    {
    }

    public EnumerableTreeNode(
      TValue value,
      IEnumerable<EnumerableTreeNode<TValue>> children)
    {
      Value = value;
      Children = children;
    }

    public TValue Value { get; }

    public IEnumerable<INodeWithEnumerableChildren<TValue>> Children { get; private set; }
  }

  public static class EnumerableTreeNode
  {
    private static EnumerableTreeNode<T> Create<T>(TreeNode<T> node)
    {
      var children = new TreeNode<T>[node.ChildCount];

      for (int i = 0; i < node.ChildCount; i++)
        children[i] = node[i];

      return new EnumerableTreeNode<T>(node.Value, children.Select(x => Create(x)));
    }

    public static IEnumerable<EnumerableTreeNode<T>> Create<T>(IEnumerable<TreeNode<T>> roots)
    {
      foreach (var root in roots)
        yield return Create(root);
    }
  }
}
