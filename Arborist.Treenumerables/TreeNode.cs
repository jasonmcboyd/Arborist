using System;
using System.Linq;

namespace Arborist.Treenumerables
{
  public class TreeNode<TValue>
    : INodeWithIndexableChildren<TreeNode<TValue>, TValue>
  {
    public TreeNode(TValue value)
      : this(value, Array.Empty<TreeNode<TValue>>())
    {
    }

    public TreeNode(
      TValue value,
      params TValue[] children)
      : this(value, children?.Select(TreeNode.Create).ToArray() ?? Array.Empty<TreeNode<TValue>>())
    {
    }

    public TreeNode(
      TValue value,
      params TreeNode<TValue>[] children)
    {
      Value = value;
      _Children = children ?? Array.Empty<TreeNode<TValue>>();
    }

    private readonly TreeNode<TValue>[] _Children;

    public TreeNode<TValue> this[int index] => _Children[index];

    public int ChildCount => _Children.Length;

    public TValue Value { get; }
  }

  public static class TreeNode
  {
    public static TreeNode<T> Create<T>(T value) => new TreeNode<T>(value);

    public static TreeNode<T> Create<T>(T value, params T[] children) => new TreeNode<T>(value, children);

    public static TreeNode<T> Create<T>(T value, params TreeNode<T>[] children) => new TreeNode<T>(value, children);
  }
}
