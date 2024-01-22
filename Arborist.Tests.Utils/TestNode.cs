using Arborist.Treenumerables;
using System.Collections.Generic;

namespace Arborist.Tests.Utils
{
  public class TestNode : INodeWithIndexableChildren<TestNode, string>
  {
    public TestNode this[int index] => Children[index];

    public int ChildCount => Children.Count;

    public readonly List<TestNode> Children = new List<TestNode>();

    public string Value { get; set; }
  }
}
