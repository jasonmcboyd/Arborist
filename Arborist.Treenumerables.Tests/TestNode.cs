using System.Collections.Generic;

namespace Arborist.Treenumerables.Tests
{
  public class TestNode : INodeWithIndexableChildren<TestNode, string>
  {
    public TestNode this[int index] => Children[index];

    public int ChildCount => Children.Count;

    public readonly List<TestNode> Children = new List<TestNode>();

    public string Value { get; set; }
  }
}
